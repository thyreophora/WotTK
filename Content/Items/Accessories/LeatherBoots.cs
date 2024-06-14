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
    [AutoloadEquip(EquipType.Shoes)]
    public class LeatherBoots : LevelLockedItem
    {
        
        public override int MinimalLevel => 1;

        public int agility = 7;
        public int stamina = 4;
        public int armor = 3;

        public override void SetDefaults()
		{
            base.SetDefaults();

            Item.width = 32;
			Item.height = 32;
            Item.value = Item.sellPrice(0, 0, 0, 30);
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
            player.GetModPlayer<WotTKPlayer>().agility += 2;
            player.GetModPlayer<WotTKPlayer>().stamina += 3;
            player.GetModPlayer<WotTKPlayer>().armor += 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<LinenCloth>(), 3)
                .AddIngredient(ModContent.ItemType<MediumLeather>(), 3)
                .AddIngredient(ItemID.FlinxFur, 2)
                .AddTile<LeatherworkingTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }

	public class LeatherBootsPlayer : ModPlayer
	{
		public bool hasBoots = false;

		public override void ResetEffects() => hasBoots = false;

		public override void PostUpdateRunSpeeds()
		{
			if (hasBoots)
			{
				Player.runAcceleration *= 1.25f;
				Player.maxRunSpeed += 0.1f;
				Player.accRunSpeed += 0.05f;
			}
		}
	}
}
