using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Content.Materials;
using WotTK.Content.Tiles;
using WotTK.Common.Players;

namespace WotTK.Content.Items.Accessories
{
    public class MysticBracelet : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 30;
			Item.value = Item.sellPrice(0, 1, 7, 95);
			Item.rare = ItemRarityID.Orange;
            Item.defense = 3;

            Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<WotTKPlayer>().agility += 10;

            player.GetModPlayer<WotTKPlayer>().strength += 6;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 8)
                .AddIngredient(ItemID.PlatinumBar, 8)
                .AddIngredient(ItemID.Shackle, 1)
                .AddTile<Tiles.BlackAnvil>()
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 8)
                .AddIngredient(ItemID.GoldBar, 8)
                .AddIngredient(ItemID.Shackle, 1)
                .AddTile<Tiles.BlackAnvil>()
                .Register();
        }
    }
}
