using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Common.Players;
using WotTK.Content.Items.Placeble;
using WotTK.Content.Items.Materials;
using System;
using WotTK.Common;

namespace WotTK.Content.Items.Accessories
{
    public class DarkBelt : LevelLockedItem
    {
        public override int MinimalLevel => 10;
        public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 24;
            Item.value = Item.sellPrice(silver: 1);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 2;

            Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<WotTKPlayer>().agility += 6;

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
