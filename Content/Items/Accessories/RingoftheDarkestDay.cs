using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Common.Players;
using WotTK.Content.Items.Placeables;
using WotTK.Content.Items.Materials;
using System;
using WotTK.Common;

namespace WotTK.Content.Items.Accessories
{
    public class RingoftheDarkestDay : LevelLockedItem
    {
        public override int MinimalLevel => 40;
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 36;
			Item.height = 30;
            Item.value = Item.sellPrice(silver: 1);
            Item.rare = ItemRarityID.LightRed;

            Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<WotTKPlayer>().intellect += 10;
            player.GetDamage(DamageClass.Magic) *= 1.05f;
            player.GetCritChance(DamageClass.Magic) += 5;

        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 15)
                .AddIngredient(ItemID.Diamond, 1)
                .AddTile<BlackAnvilTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
}
