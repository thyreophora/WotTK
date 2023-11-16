using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Content.Materials;
using WotTK.Content.Tiles;
using WotTK.Common.Players;

namespace WotTK.Content.Items.Accessories
{
    public class ArchaedicStone : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 30;
			Item.value = Item.sellPrice(0, 0, 40, 0);
			Item.rare = ItemRarityID.LightRed;
            Item.defense = 5;

            Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<WotTKPlayer>().agility += 12;

            player.GetModPlayer<WotTKPlayer>().strength += 10;

            player.GetModPlayer<WotTKPlayer>().intellect += 8;

            player.statManaMax2 += 25;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MysticBracelet>(), 1)
                .AddIngredient(ModContent.ItemType<UnderworldBand>(), 1)
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 14)
                .AddIngredient(ItemID.HellstoneBar, 8)
                .AddTile<Tiles.BlackAnvil>()
                .Register();
        }
    }
}
