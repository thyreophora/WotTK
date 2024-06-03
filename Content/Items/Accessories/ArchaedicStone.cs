using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using WotTK.Common;
using WotTK.Common.Players;
using System.Collections.Generic;
using WotTK.Content.Items.Placeables;
using WotTK.Content.Items.Materials;

namespace WotTK.Content.Items.Accessories
{
    public class ArchaedicStone : LevelLockedItem
    {
        public override int MinimalLevel => 35;

        public int agility = 18;
        public int stamina = 26;
        public int armor = 24;
        public int spirit = 15;
    
        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 30;
			Item.value = Item.sellPrice(0, 0, 40, 0);
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
        if (spirit > 0)
        {
            string spiritText = $"[c/FFFF00:+{spirit}] spirit";
            tooltips.Add(new TooltipLine(Mod, "Spirit", spiritText));
        }
        if (armor > 0)
        {
            string armorText = $"[c/FFFF00:+{armor}] armor";
            tooltips.Add(new TooltipLine(Mod, "Armor", armorText));
        }
    }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<WotTKPlayer>().agility += 18;
            player.GetModPlayer<WotTKPlayer>().stamina += 26;
            player.GetModPlayer<WotTKPlayer>().armor += 24;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MysticBracelet>(), 1)
                .AddIngredient(ModContent.ItemType<UnderworldBand>(), 1)
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 15)
                .AddIngredient(ItemID.HellstoneBar, 8)
                .AddTile<BlackAnvilTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
}
