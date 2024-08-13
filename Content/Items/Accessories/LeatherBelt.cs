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
    public class LeatherBelt : LevelLockedItem
    {
        
        public override int MinimalLevel => 3;

        public int agility = 4;
        public int stamina = 3;

        public override void SetDefaults()
		{
            base.SetDefaults();

			Item.width = 30;
			Item.height = 32;
            Item.value = Item.sellPrice(0, 0, 0, 25);
            Item.rare = ItemRarityID.Blue;

            Item.accessory = true;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (agility > 0)
            {
                string agilityText = $"[c/FFFF00:+{agility}] agility";
                tooltips.Add(new TooltipLine(Mod, "Agility", agilityText));
            }
            if (stamina > 0)
            {
                string staminaText = $"[c/FFFF00:+{stamina}] stamina";
                tooltips.Add(new TooltipLine(Mod, "Stamina", staminaText));
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.autoReuseGlove = true;

            player.GetModPlayer<WotTKPlayer>().agility += 4;
            player.GetModPlayer<WotTKPlayer>().stamina += 3;
        }
        public override void AddRecipes()
        {
            int requiredTool = ModContent.ItemType<Content.Items.Tools.LeatherworkingTool>();

            CreateRecipe()
                .AddIngredient(ModContent.ItemType<LinenCloth>(), 1)
                .AddIngredient(ModContent.ItemType<MediumLeather>(), 3)
                .AddIngredient(ItemID.FlinxFur, 2)
                .AddTile<LeatherworkingTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)

                .AddCondition(LeatherworkingToolCondition.ConstructLeatherworkingToolCondition(requiredTool, out Func<bool> toolCondition), toolCondition)
                .Register();
        }
    }
}
