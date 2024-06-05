using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using WotTK.Common.Players;

namespace WotTK.Common.QuestSystem;

public class SlayTask : QuestTask
{
    public int? MobType { get; }
    public int? MobNetId { get; }
    public int MobCount { get; }
    
    public override string TitleKey => "Quests.Common.SlayTask.Title";
    public override string DescriptionKey => "Quests.Common.SlayTask.Description";

    private readonly HashSet<WotTKPlayer> playersDoing = new();
    
    private SlayTask(int? mobType, int? mobNetId, int mobCount, string id)
        : base(id)
    {
        MobType = mobType;
        MobNetId = mobNetId;
        MobCount = mobCount;
    }

    public static SlayTask FromType(int mobType, int mobCount, string id)
    {
        if (mobType <= 0)
        {
            throw new ArgumentException("Invalid mob type", nameof(mobType));
        }
        
        return new SlayTask(mobType, null, mobCount, id);
    }
    
    public static SlayTask FromNetId(int mobNetId, int mobCount, string id)
    {
        if (mobNetId >= 0)
        {
            throw new ArgumentException("Invalid mob net id", nameof(mobNetId));
        }
        
        return new SlayTask(null, mobNetId, mobCount, id);
    }

    public override void AddPlayer(WotTKPlayer player)
    {
        if (playersDoing.Count == 0)
        {
            On_Player.OnKillNPC += PlayerKilledNPC;
        }

        playersDoing.Add(player);
    }

    public override void RemovePlayer(WotTKPlayer player)
    {
        playersDoing.Remove(player);
        
        if (playersDoing.Count == 0)
        {
            On_Player.OnKillNPC -= PlayerKilledNPC;
        }
    }

    public override bool IsDone(WotTKPlayer player)
    {
        return player.GetTaskProgress<int>(QuestId, Id) >= MobCount;
    }

    public override void Draw(SpriteBatch sb, Vector2 position)
    {
        
    }

    private void PlayerKilledNPC(On_Player.orig_OnKillNPC orig, Player self, ref NPCKillAttempt attempt, object externalKillingBlowSource)
    {
        if (MobType.HasValue ? attempt.npc.type == MobType : attempt.npc.netID == MobNetId && playersDoing.Contains(self.GetModPlayer<WotTKPlayer>()))
        {
            var player = self.GetModPlayer<WotTKPlayer>();
            player.SetTaskProgress(QuestId, Id, player.GetTaskProgress<int>(QuestId, Id) + 1);
        }
        
        orig(self, ref attempt, externalKillingBlowSource);
    }
}