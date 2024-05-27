using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Content.Items.Placeables;
using WotTK.Common;

namespace WotTK.Content.Items.Materials
{
    public class DarkIronBar : LevelLockedItem
    {
        public override int MinimalLevel => 35;
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = -11;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronOre>(), 4)
                .AddIngredient(ItemID.Hellstone, 2)
                .AddTile<BlackAnvilTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
}