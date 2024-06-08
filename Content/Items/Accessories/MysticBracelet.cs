using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Common.Players;
using WotTK.Content.Items.Placeables;
using WotTK.Content.Items.Materials;
using System;
using WotTK.Common;
using System.Collections.Generic;

namespace WotTK.Content.Items.Accessories
{
    public class MysticBracelet : LevelLockedItem
	{
        public override int MinimalLevel => 35;

        public int spirit = 14;
        public int intellect = 18;
        public override void SetDefaults()
		{
            base.SetDefaults();

            Item.width = 32;
			Item.height = 30;
			Item.value = Item.sellPrice(0, 0, 6, 50);
			Item.rare = ItemRarityID.Orange;
            Item.defense = 3;

            Item.accessory = true;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (spirit > 0)
            {
                string spiritText = $"[c/FFFF00:+{spirit}] spirit";
                tooltips.Add(new TooltipLine(Mod, "Spirit", spiritText));
            }
            if (intellect > 0)
            {
                string intellectText = $"[c/FFFF00:+{intellect}] intellect";
                tooltips.Add(new TooltipLine(Mod, "Intellect", intellectText));
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<WotTKPlayer>().spirit += 14;
            player.GetModPlayer<WotTKPlayer>().intellect += 18;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 8)
                .AddIngredient(ItemID.PlatinumBar, 8)
                .AddIngredient(ItemID.Shackle, 1)
                .AddTile<BlackAnvilTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 8)
                .AddIngredient(ItemID.GoldBar, 8)
                .AddIngredient(ItemID.Shackle, 1)
                .AddTile<BlackAnvilTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition2), condition2)
                .Register();
        }
    }
}
