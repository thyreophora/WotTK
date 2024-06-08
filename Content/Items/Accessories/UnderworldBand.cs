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
    public class UnderworldBand : LevelLockedItem
    {
        public override int MinimalLevel => 25;

        public int stamina = 18;
        public int armor = 16;

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 24;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 0, 62, 0);
			Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (stamina > 0)
            {
                string staminaText = $"[c/FFFF00:+{stamina}] stamina";
                tooltips.Add(new TooltipLine(Mod, "Stamina", staminaText));
            }
            if (armor > 0)
            {
                string armorText = $"[c/FFFF00:+{armor}] armor";
                tooltips.Add(new TooltipLine(Mod, "Armor", armorText));
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<WotTKPlayer>().stamina += 18;
            player.GetModPlayer<WotTKPlayer>().armor += 16;
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
