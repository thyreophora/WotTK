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
    public class ArchaedicStone : LevelLockedItem
    {
        public override int MinimalLevel => 27;
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
                .AddTile<BlackAnvilTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
}
