using Microsoft.Xna.Framework;
using System.Collections.Generic;
using WotTK.Content.Items.Materials;
using WotTK.Content.Items.Placeble;
using WotTK.Common.Players;
using WotTK.Common;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Weapons.Melee.Swords
{
    public class DarkIronGreatsword : LevelLockedItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true;
        }

        public override int MinimalLevel => 40;
        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 48;
            Item.height = 48;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.buyPrice(platinum: 5);

            // Use Properties
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.channel = true;

            // Weapon Properties
            Item.damage = 45;
            Item.knockBack = 5;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;

            // Projectile Properties
            Item.shootSpeed = 25f;
            Item.shoot = ModContent.ProjectileType<DarkIronGreatsword_Proj>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<DarkIronGreatsword_Proj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DarkIronBar>(), 18)
                .AddIngredient(ItemID.PlatinumBroadsword, 1)
                .AddTile<BlackAnvilTile>()
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
}
