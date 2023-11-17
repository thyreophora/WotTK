using System;
using System.Collections.Generic;
using WotTK.Common.Players;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Test
{
    public class LevelUp : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = Item.height = 32;
            Item.value = 0;
            Item.rare = -1;

            Item.useTime = Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item1;
            Item.consumable = false;
        }
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<WotTKPlayer>().playerLevel = 80;
            player.GetModPlayer<WotTKPlayer>().playerLevelPoints = 0;
            Main.NewText("Level is maximazed (80 level)");
            return true;
        }
    }
}
