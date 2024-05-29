using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Common.Players;
using WotTK.Content.Items.Placeables;
using WotTK.Content.Items.Materials;
using System;
using WotTK.Common;

namespace WotTK.Content.Items.Accessories
{
    public class DarkBelt : LevelLockedItem
    {
        public override int MinimalLevel => 40;
        public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 24;
            Item.value = Item.sellPrice(silver: 1);
            Item.rare = ItemRarityID.LightRed;

            Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<WotTKPlayer>().stamina += 10;
            player.GetModPlayer<WotTKPlayer>().armor += 10;

        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 2)
                .AddIngredient(ModContent.ItemType<MediumLeather>(), 6)
                .AddTile<BlackAnvilTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
}
