using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace WotTK.Content.Items.Consumables.Abstract
{
    public abstract class Bandage : ModItem
    {
        public int healing_time;
        public int heal_amount;
        public override void SetDefaults()
        {
            DefaultBandage();
        }
        public void DefaultBandage(int time = 60 * 60, int amount = 100)
        {
            Item.consumable = true;
            Item.maxStack = 69;
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Purple;
            healing_time = time;
            heal_amount = amount;
        }

        public override bool CanUseItem(Player player)
        {
            // TODO: replace with buff in the UseItem function
            return !player.HasBuff(BuffID.PotionSickness);
        }

        public override bool? UseItem(Player player)
        {
            // TODO: make a buff that gives healing every X ticks for a total amount
            player.AddBuff(BuffID.PotionSickness, healing_time);
            player.Heal(heal_amount);
            return true;
        }
    }
}
