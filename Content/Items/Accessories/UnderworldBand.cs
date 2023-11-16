using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Content.Materials;
using WotTK.Content.Tiles;
using WotTK.Common.Players;

namespace WotTK.Content.Items.Accessories
{
    public class UnderworldBand : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 0, 62, 0);
			Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<WotTKPlayer>().agility += 4;

            player.GetModPlayer<WotTKPlayer>().intellect += 6;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 6)
                .AddIngredient(ItemID.Obsidian, 8)
                .AddTile<Tiles.BlackAnvil>()
                .Register();
        }
    }
}
