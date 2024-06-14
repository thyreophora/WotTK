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
    [AutoloadEquip(EquipType.Front)]
    public class RogueScarf : LevelLockedItem
    {
        public override int MinimalLevel => 3;

        public int agility = 12;
        public int strength = 8;
        public int armor = 10;

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 30;
            Item.height = 26;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
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
            player.GetModPlayer<WotTKPlayer>().agility += 12;
            player.GetModPlayer<WotTKPlayer>().strength += 8;
            player.GetModPlayer<WotTKPlayer>().armor += 10;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WoolCloth>(), 3)
                .AddIngredient(ModContent.ItemType<MediumLeather>(), 8)
                .AddTile<LeatherworkingTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
}