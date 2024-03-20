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
    public class DarkRuby : LevelLockedItem
    {
        public override int MinimalLevel => 35;
        public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 0, 40, 0);
			Item.rare = ItemRarityID.LightRed;

            Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<WotTKPlayer>().agility += 8;
            player.lifeRegen += 4;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 14)
                .AddIngredient(ItemID.Ruby, 2)
                .AddTile<BlackAnvilTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
}
