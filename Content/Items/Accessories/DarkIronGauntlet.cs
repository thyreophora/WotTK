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
    public class DarkIronGauntlet : LevelLockedItem
    {
        public override int MinimalLevel => 35;

        public int strength = 16;
        public int armor = 14;
        public int stamina = 18;

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 28;
			Item.height = 44;
            Item.value = Item.sellPrice(silver: 1);
            Item.rare = ItemRarityID.LightRed;

            Item.accessory = true;
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (strength > 0)
            {
                string strengthText = $"[c/FFFF00:+{strength}] strength";
                tooltips.Add(new TooltipLine(Mod, "Strength", strengthText));
            }
            if (armor > 0)
            {
                string armorText = $"[c/FFFF00:+{armor}] armor";
                tooltips.Add(new TooltipLine(Mod, "Armor", armorText));
            }
            if (stamina > 0)
            {
                string staminaText = $"[c/FFFF00:+{stamina}] stamina";
                tooltips.Add(new TooltipLine(Mod, "Stamina", staminaText));
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<WotTKPlayer>().strength += 16;
            player.GetModPlayer<WotTKPlayer>().armor += 14;
            player.GetModPlayer<WotTKPlayer>().stamina += 18;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 8)
                .AddIngredient(ModContent.ItemType<MediumLeather>(), 6)
                .AddTile<BlackAnvilTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
}
