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
    public class DarkIronGauntlet : LevelLockedItem
    {
        public override int MinimalLevel => 40;
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 28;
			Item.height = 44;
            Item.value = Item.sellPrice(silver: 1);
            Item.rare = ItemRarityID.LightRed;

            Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<WotTKPlayer>().agility += 15;

            player.GetDamage(DamageClass.Melee) *= 1.07f;
            player.GetCritChance(DamageClass.Melee) += 7;
            player.GetAttackSpeed(DamageClass.Melee) += 0.08f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 8)
                .AddIngredient(ModContent.ItemType<MediumLeather>(), 6)
                .AddTile<BlackAnvilTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
}
