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
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = -11;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.IronBar, 4);
            recipe.AddIngredient(ItemID.DemoniteBar, 4);
            recipe.AddTile(TileID.Anvils);

            recipe.Register();
        }
    }
}