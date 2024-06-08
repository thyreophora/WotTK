using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using WotTK.Common.Players;

namespace WotTK.Common.QuestSystem;

public class ExploreTask : QuestTask
{
    public Vector2 Location { get; }
    public float AreaRadius { get; }
    private float areaRadiusSquared;

    public override string TitleKey => "Quests.Common.ExploreTask.Title";
    public override string DescriptionKey => "Quests.Common.ExploreTask.Description";
    
    private readonly HashSet<WotTKPlayer> playersDoing = new();
    
    public ExploreTask(Vector2 location, float areaRadius, string id)
        : base(id)
    {
        Location = location;
        AreaRadius = areaRadius;
        areaRadiusSquared = AreaRadius * AreaRadius;
    }

    public override void AddPlayer(WotTKPlayer player)
    {
        if (playersDoing.Count == 0)
        {
            On_Player.Update += PlayerTick;
        }

        playersDoing.Add(player);
    }

    public override void RemovePlayer(WotTKPlayer player)
    {
        playersDoing.Remove(player);
        
        if (playersDoing.Count == 0)
        {
            On_Player.Update -= PlayerTick;
        }
    }

    public override bool IsDone(WotTKPlayer player)
    {
        return player.GetTaskProgress<bool>(QuestId, Id);
    }

    public override void Draw(SpriteBatch sb, Vector2 position)
    {
    }

    private void PlayerTick(On_Player.orig_Update orig, Player self, int i)
    {
        if (playersDoing.Contains(self.GetModPlayer<WotTKPlayer>()))
        {
            if ((Location - self.Center).LengthSquared() <= areaRadiusSquared)
            {
                self.GetModPlayer<WotTKPlayer>().SetTaskProgress(QuestId, Id, true);
            }
        }
    }
}