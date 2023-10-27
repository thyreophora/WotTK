using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Test
{
    public class LevelShow : ModItem
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
            Main.NewText("Level: " + player.GetModPlayer<WotTKPlayer>().playerLevel + "\n Points: " + player.GetModPlayer<WotTKPlayer>().playerLevelPoints + "/" + player.GetModPlayer<WotTKPlayer>().playerLevelPointsNeed);
            return true;
        }
    }
}
