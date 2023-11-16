using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using WotTK.Content.Materials;

namespace WotTK.Content.Tiles
{
    public class BlackAnvilItem : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 24;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.rare = -11;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.createTile = ModContent.TileType<BlackAnvil>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 8)
                .AddIngredient(ItemID.IronAnvil, 1)

                .Register();

        }
    }
}