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
    public class LevelReset : ModItem
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
            //Main.NewText("Level: " + player.GetModPlayer<WotTKPlayer>().playerLevel + "\n Points: " + player.GetModPlayer<WotTKPlayer>().playerLevelPoints + "/" + player.GetModPlayer<WotTKPlayer>().playerLevelPointsNeed);
            //player.GetModPlayer<WotTKPlayer>().playerLevel = 1;
            //player.GetModPlayer<WotTKPlayer>().playerLevelPoints = 0;
            //Main.NewText("Progress is reseted");

            if (player.altFunctionUse == 2)
            {
                player.GetModPlayer<WotTKPlayer>().playerLevel -= 1;
                player.GetModPlayer<WotTKPlayer>().playerLevelPoints = 0;
                Main.NewText("Level decreased by 1 level");

            }
            else
            {
                player.GetModPlayer<WotTKPlayer>().playerLevel = 1;
                player.GetModPlayer<WotTKPlayer>().playerLevelPoints = 0;
                Main.NewText("Progress is reseted");
            }

            return true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}
