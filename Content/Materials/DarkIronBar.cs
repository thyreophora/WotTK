using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Materials
{
    public class DarkIronBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
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
                .AddIngredient(ItemID.IronBar, 2)
                .AddIngredient(ItemID.DemoniteBar, 2)
                .AddTile(TileID.Anvils)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 2)
                .AddIngredient(ItemID.CrimtaneBar, 2)
                .AddTile(TileID.Anvils)
                .Register();

        }
    }
}