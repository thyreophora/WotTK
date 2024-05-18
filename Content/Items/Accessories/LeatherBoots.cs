using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Common.Players;
using WotTK.Content.Items.Placeble;
using WotTK.Content.Items.Materials;
using System;
using WotTK.Common;

namespace WotTK.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Shoes)]
    public class LeatherBoots : LevelLockedItem
    {
        
        public override int MinimalLevel => 3;
        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
            Item.value = Item.sellPrice(silver: 1);
            Item.rare = ItemRarityID.LightRed;

            Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<LeatherBootsPlayer>().hasBoots = true;

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<LinenCloth>(), 3)
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
