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
    public abstract class AbstractPill : ModItem
    {
        public int toxic_index;
        public int effect_index;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Floral Macirsum Bag");
            // Tooltip.SetDefault("May contain seeds of other types\n{$CommonItem// Tooltip.RightClickToOpen}");
        }
        public override void SetDefaults()
        {
            DefaultPill();
        }
        public void DefaultPill()
        {
            Item.consumable = true;
            Item.maxStack = 999;
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Purple;
            toxic_index = 0;
            effect_index = 0;
        }
        public override void RightClick(Player player)
        {
            //player.GetModPlayer<PlayerPills>().pill[effect_index] += 60;
            //player.GetModPlayer<PlayerPills>().toxic[toxic_index] += 90;
        }
        public override bool CanRightClick()
        {
            return true;
        }
    }
}
