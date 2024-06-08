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
    public abstract class NightstalkerArmor : LevelLockedItem
    {
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<WotTKPlayer>().armor += armor;
            player.GetModPlayer<WotTKPlayer>().strength += strength;
            player.GetModPlayer<WotTKPlayer>().agility += agility;

            if (player.head == ModContent.ItemType<NightstalkerHoodie>() &&
                player.body == ModContent.ItemType<NightstalkerCloak>() &&
                player.legs == ModContent.ItemType<NightstalkerPants>())
            {
                player.GetModPlayer<WotTKPlayer>().strength += 50;
            }
        }

        public int armor;
        public int strength;
        public int agility;
    }

    [AutoloadEquip(EquipType.Head)]
    public class NightstalkerHoodie : LevelLockedItem
    {
        public override int MinimalLevel => 15;
        
        public int armor = 9;
        public int strength = 6;
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
            player.GetModPlayer<WotTKPlayer>().armor += 9;
            player.GetModPlayer<WotTKPlayer>().strength += 6;
            player.GetModPlayer<WotTKPlayer>().agility += 14;
        }
    }
    
    [AutoloadEquip(EquipType.Body)]
    public class NightstalkerCloak : LevelLockedItem
    {
        public override int MinimalLevel => 15;

        public int armor = 14;
        public int stamina = 12;
        public int agility = 16;

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
        if (stamina > 0)
        {
            string staminaText = $"[c/FFFF00:+{stamina}] stamina";
            tooltips.Add(new TooltipLine(Mod, "Stamina", staminaText));
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
            player.GetModPlayer<WotTKPlayer>().armor += 14;
            player.GetModPlayer<WotTKPlayer>().strength += 12;
            player.GetModPlayer<WotTKPlayer>().agility += 16;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class NightstalkerPants : LevelLockedItem
    {
        public override int MinimalLevel => 15;

        public int armor = 12;
        public int agility = 14;

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
            player.GetModPlayer<WotTKPlayer>().armor += 12;
            player.GetModPlayer<WotTKPlayer>().agility += 14;
        }
    }
}
