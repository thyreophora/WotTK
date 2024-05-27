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
    public class UnderworldBand : LevelLockedItem
    {
        public override int MinimalLevel => 25;

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

            player.GetModPlayer<WotTKPlayer>().intellect += 3;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 8)
                .AddIngredient(ItemID.Obsidian, 8)
                .AddTile<BlackAnvilTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
}
