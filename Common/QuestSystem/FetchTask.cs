using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using WotTK.Common.Players;

namespace WotTK.Common.QuestSystem;

public class FetchTask : QuestTask
{
    public int ItemType { get; }
    public int ItemCount { get; }

    public override string TitleKey => "Quests.Common.FetchTask.Title";
    public override string DescriptionKey => "Quests.Common.FetchTask.Description";

    public FetchTask(int itemType, int itemCount, string id)
        : base(id)
    {
        ItemType = itemType;
        ItemCount = itemCount;
    }

    public override bool IsDone(WotTKPlayer player)
    {
        return player.Player.inventory.Where(item => item.type == ItemType).Sum(item => item.stack) >= ItemCount;
    }

    public override bool Apply(WotTKPlayer player)
    {
        if (!IsDone(player))
        {
            return false;
        }
        
        int toSub = ItemCount;
        foreach (Item item in player.Player.inventory)
        {
            if (item.type == ItemType)
            {
                int subCount = Math.Min(item.stack, toSub);
                item.stack -= subCount;
                toSub -= subCount;

                if (toSub == 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public override void Draw(SpriteBatch sb, Vector2 position)
    {
        if (ContentSamples.ItemsByType.TryGetValue(ItemType, out Item item))
        {
            ItemSlot.Draw(sb, ref item, 0, position, Color.White);
        }
    }
}