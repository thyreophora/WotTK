using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using WotTK.Common;
using WotTK.Common.Players;
using System.Collections.Generic;
using WotTK.Content.Items.Placeables;
using WotTK.Content.Items.Materials;

namespace WotTK.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class NightstalkerHoodie : LevelLockedItem
    {
        public override int MinimalLevel => 15;
        
        public int armor = 16;
        public int strength = 12;
        public int agility = 14;

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 18;
            Item.height = 22;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Green;
        }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        if (armor > 0)
        {
            string armorText = $"[c/FFFF00:+{armor}] armor";
            tooltips.Add(new TooltipLine(Mod, "Armor", armorText));
        }
        if (strength > 0)
        {
            string strengthText = $"[c/FFFF00:+{strength}] strength";
            tooltips.Add(new TooltipLine(Mod, "Strength", strengthText));
        }
        if (agility > 0)
        {
            string agilityText = $"[c/FFFF00:+{agility}] agility";
            tooltips.Add(new TooltipLine(Mod, "Agility", agilityText));
        }
    }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MediumLeather>(), 6)
                .AddTile<LeatherworkingTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<WotTKPlayer>().armor += 16;
            player.GetModPlayer<WotTKPlayer>().strength += 12;
            player.GetModPlayer<WotTKPlayer>().agility += 14;
        }
    }
    
    [AutoloadEquip(EquipType.Body)]
    public class NightstalkerCloak : LevelLockedItem
    {
        public override int MinimalLevel => 15;

        public int armor = 24;
        public int strength = 18;
        public int agility = 22;

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 30;
            Item.height = 24;
            Item.value = Item.sellPrice(silver: 9);
            Item.rare = ItemRarityID.Green;
        }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        if (armor > 0)
        {
            string armorText = $"[c/FFFF00:+{armor}] armor";
            tooltips.Add(new TooltipLine(Mod, "Armor", armorText));
        }
        if (strength > 0)
        {
            string strengthText = $"[c/FFFF00:+{strength}] strength";
            tooltips.Add(new TooltipLine(Mod, "Strength", strengthText));
        }
        if (agility > 0)
        {
            string agilityText = $"[c/FFFF00:+{agility}] agility";
            tooltips.Add(new TooltipLine(Mod, "Agility", agilityText));
        }
    }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MediumLeather>(), 6)
                .AddTile<LeatherworkingTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<WotTKPlayer>().armor += 24;
            player.GetModPlayer<WotTKPlayer>().strength += 18;
            player.GetModPlayer<WotTKPlayer>().agility += 22;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class NightstalkerPants : LevelLockedItem
    {
        public override int MinimalLevel => 15;

        public int armor = 18;
        public int strength = 15;
        public int agility = 18;

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 24;
            Item.height = 14;
            Item.value = Item.sellPrice(silver: 7);
            Item.rare = ItemRarityID.Green;
        }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        if (armor > 0)
        {
            string armorText = $"[c/FFFF00:+{armor}] armor";
            tooltips.Add(new TooltipLine(Mod, "Armor", armorText));
        }
        if (strength > 0)
        {
            string strengthText = $"[c/FFFF00:+{strength}] strength";
            tooltips.Add(new TooltipLine(Mod, "Strength", strengthText));
        }
        if (agility > 0)
        {
            string agilityText = $"[c/FFFF00:+{agility}] agility";
            tooltips.Add(new TooltipLine(Mod, "Agility", agilityText));
        }
    }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MediumLeather>(), 6)
                .AddTile<LeatherworkingTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<WotTKPlayer>().armor += 18;
            player.GetModPlayer<WotTKPlayer>().strength += 15;
            player.GetModPlayer<WotTKPlayer>().agility += 18;
        }
    }
}
