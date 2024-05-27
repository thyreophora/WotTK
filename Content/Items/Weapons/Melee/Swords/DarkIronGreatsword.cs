using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Content.Items.Materials;
using WotTK.Content.Items.Placeables;
using WotTK.Common.Players;
using WotTK.Common;
using System;

namespace WotTK.Content.Items.Weapons.Melee.Swords
{
    public class DarkIronGreatsword : LevelLockedItem
    {
        public static readonly SoundStyle swordSwing = new("WotTK/Sounds/Swings/SwordSwing", 2);
        public override int MinimalLevel => 15;
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 45;
            Item.useAnimation = 20;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 20;
            Item.useTurn = true;

            Item.knockBack = 3f;
            Item.UseSound = swordSwing;
            Item.autoReuse = true;
            Item.shootSpeed = 10f;

            Item.value = 500;
            Item.rare = ItemRarityID.Orange;
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
