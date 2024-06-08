using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Common.Players;
using System.Collections.Generic;
using WotTK.Content.Items.Placeables;
using WotTK.Content.Items.Materials;
using System;
using WotTK.Common;

namespace WotTK.Content.Items.Accessories
{
    public class DarkRuby : LevelLockedItem
    {
        public override int MinimalLevel => 35;

        public int spirit = 18;
        public int intellect = 15;

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 28;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 0, 15, 50);
			Item.rare = ItemRarityID.LightRed;

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
            player.GetModPlayer<WotTKPlayer>().spirit += 18;
            player.GetModPlayer<WotTKPlayer>().intellect += 15;
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
