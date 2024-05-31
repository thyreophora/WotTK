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

        public int agility = 15;
        public int spirit = 22;
        public int armor = 15;

        public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 24;
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
			player.GetModPlayer<WotTKPlayer>().agility += 15;
            player.GetModPlayer<WotTKPlayer>().spirit += 22;
            player.GetModPlayer<WotTKPlayer>().armor += 15;
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
