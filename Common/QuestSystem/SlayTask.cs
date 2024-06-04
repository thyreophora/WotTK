using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using WotTK.Common.Players;

namespace WotTK.Common.QuestSystem;

public class SlayTask : QuestTask
{
    public int MobType { get; }
    public int MobCount { get; }
    
    public override string TitleKey => "Quests.Common.SlayTask.Title";
    public override string DescriptionKey => "Quests.Common.SlayTask.Description";

    private int playersDoing;
    
    public SlayTask(int mobType, int mobCount, string id)
        : base(id)
    {
        MobType = mobType;
        MobCount = mobCount;
    }

    public override void AddPlayer(WotTKPlayer player)
    {
        playersDoing++;
        
        On_Player.OnKillNPC += PlayerKilledNPC;
    }

    public override void RemovePlayer(WotTKPlayer player)
    {
        playersDoing--;

        if (playersDoing == 0)
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
        if (attempt.npc.type == MobType)
        {
            var player = self.GetModPlayer<WotTKPlayer>();
            player.SetTaskProgress(QuestId, Id, player.GetTaskProgress<int>(QuestId, Id) + 1);
        }
        
        orig(self, ref attempt, externalKillingBlowSource);
    }
}