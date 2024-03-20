using Microsoft.Xna.Framework;
using WotTK.Content.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WotTK.Content.Items.Materials;

namespace WotTK.Content.Items.Weapons.Magic.Staffs
{
    public class BlackholeWand : LevelLockedItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
        public override int MinimalLevel => 32;
        public override bool IsWeapon => true;
        public override void SetDefaults()
        {
            Item.width = 86;
            Item.height = 44;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;

            Item.useTime = Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = new SoundStyle("WotTK/Sounds/Custom/WandBaseSound");
            Item.autoReuse = true;

            Item.mana = 10;
            Item.damage = 50;
            Item.knockBack = 2f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 20;
            Item.width = 28;
            Item.height = 28;
            Item.useTime = 6;
            Item.useAnimation = 24;
            Item.reuseDelay = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            //Item.shoot = ModContent.ProjectileType<DigestiveVat_Proj>();
            Item.UseSound = SoundID.NPCHit20;

            Item.shoot = ModContent.ProjectileType<BlackholeWand_Proj>();
            Item.shootSpeed = 20;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));
                float scale = 1f - (Main.rand.NextFloat() * 0.4f);
                perturbedSpeed *= scale;
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<OrdilWood>(), 6)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}