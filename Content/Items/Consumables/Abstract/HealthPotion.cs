using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Consumables.Abstract
{
    public abstract class HealthPotion : ModItem
    {
        public int sickness_time;
        public int heal_amount;
        public override void SetDefaults()
        {
            DefaultHealthPotion();
        }
        public void DefaultHealthPotion(int sickness = 60 * 60, int amount = 100)
        {
            Item.consumable = true;
            Item.maxStack = 69;
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Purple;
            sickness_time = sickness;
            heal_amount = amount;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(BuffID.PotionSickness);
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(BuffID.PotionSickness, sickness_time);
            player.Heal(heal_amount);
            return true;
        }
    }
}
