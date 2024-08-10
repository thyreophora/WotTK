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
    [AutoloadEquip(new EquipType[] { EquipType.HandsOn, EquipType.HandsOff })]
    public class LeatherGloves : LevelLockedItem
    {
        
        public override int MinimalLevel => 3;

        public int agility = 3;
        public int stamina = 4;
        public int armor = 2;

        public override void SetDefaults()
		{
			Item.width = 44;
			Item.height = 34;
            Item.value = Item.sellPrice(0, 0, 0, 25);
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
            player.autoReuseGlove = true;

            player.GetModPlayer<WotTKPlayer>().agility += 3;
            player.GetModPlayer<WotTKPlayer>().stamina += 4;
            player.GetModPlayer<WotTKPlayer>().armor += 2;
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
