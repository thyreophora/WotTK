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
    public class LeatherSheath : LevelLockedItem
    {
        
        public override int MinimalLevel => 5;

        public int agility = 4;
        public int strength = 6;
        public int armor = 8;

        public override void SetDefaults()
		{
			Item.width = 42;
			Item.height = 50;
            Item.value = Item.sellPrice(0, 0, 0, 45);
            Item.rare = ItemRarityID.LightRed;

            Item.accessory = true;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (agility > 0)
            {
                string agilityText = $"[c/FFFF00:+{agility}] agility";
                tooltips.Add(new TooltipLine(Mod, "Agility", agilityText));
            }
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
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.autoReuseGlove = true;

            player.GetModPlayer<WotTKPlayer>().agility += 4;
            player.GetModPlayer<WotTKPlayer>().strength += 6;
            player.GetModPlayer<WotTKPlayer>().armor += 8;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MediumLeather>(), 6)
                .AddTile<LeatherworkingTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
}
