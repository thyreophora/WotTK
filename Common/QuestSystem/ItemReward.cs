using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using WotTK.Common.Players;

namespace WotTK.Common.QuestSystem;

public class ItemReward : QuestReward
{
    public int ItemType { get; }
    public int ItemCount { get; }
    
    public override string TitleKey => "Quests.Common.ItemReward.Title";
    public override string DescriptionKey => "Quests.Common.ItemReward.Description";
    
    public ItemReward(int itemType, int itemCount, string id)
        : base(id)
    {
        ItemType = itemType;
        ItemCount = itemCount;
    }

    public override void Grant(WotTKPlayer player)
    {
        player.Player.QuickSpawnItem(null, ContentSamples.ItemsByType[ItemType], ItemCount);
    }

    public override void Draw(SpriteBatch sb, Vector2 position)
    {
        
    }
}