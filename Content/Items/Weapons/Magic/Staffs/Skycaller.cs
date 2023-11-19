using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using WotTK.Utilities;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Audio;
using WotTK.Common;

namespace WotTK.Content.Items.Weapons.Magic.Staffs
{
    public class Skycaller : LevelLockedItem
    {
        public override int MinimalLevel => 7;
        public override bool IsWeapon => true;
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 42;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;

            Item.useTime = Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = new SoundStyle("WotTK/Sounds/Custom/WandBaseSound");
            Item.autoReuse = true;

            Item.mana = 10;
            Item.damage = 25;
            Item.knockBack = 2f;
            Item.DamageType = DamageClass.Magic;

            Item.shoot = ModContent.ProjectileType<SkycallerProj>();
            Item.shootSpeed = 20;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);

            //velocity = new Vector2(0, Item.shootSpeed).RotatedByRandom(0.33f);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 vel = new Vector2(0, Item.shootSpeed).RotatedByRandom(0.33f);
            Projectile.NewProjectile(source, Main.MouseWorld - vel * 40f, vel, type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FallenStar, 5)
                .AddRecipeGroup("Wood", 6)
                .AddTile(TileID.Anvils)
                .AddCondition(LevelLockedRecipe.ConstructRecipeCondition(MinimalLevel, out Func<bool> condition), condition)
                .Register();
        }
    }
    public class SkycallerProj : ModProjectile 
    {
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 22;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;

            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.ai[0] = Main.MouseWorld.Y + 2;
        }
        public int TargetIndex = -1;
        public override void AI()
        {
            Projectile.rotation += 0.2f;
            Projectile.velocity.Y += 0.1f;
            if (Projectile.Center.Y > Projectile.ai[0])
                Projectile.tileCollide = true;
            if (Projectile.velocity.Y > 0)
            {
                if (TargetIndex >= 0)
                {
                    if (!Main.npc[TargetIndex].active || !Main.npc[TargetIndex].CanBeChasedBy())
                    {
                        TargetIndex = -1;
                    }
                    else
                    {
                        Vector2 value = Projectile.SafeDirectionTo(Main.npc[TargetIndex].Center) * (Projectile.velocity.Length() + 1.5f);
                        Projectile.velocity = Vector2.Lerp(Projectile.velocity, value, 0.05f);
                    }
                }

                if (TargetIndex == -1)
                {
                    NPC nPC = Projectile.Center.ClosestNPCAt(1600f);
                    if (nPC != null)
                    {
                        TargetIndex = nPC.whoAmI;
                    }
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("WotTK/Textures/_Glow");
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            SpriteEffects effects = (Projectile.spriteDirection == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None; 
            Color color = new Color(255, 255, 0) * 0.1f; // R, G, B 
            for (int k = 0; k < Projectile.oldPos.Length - 1; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                spriteBatch.Draw(texture, drawPos, null, color * 0.45f, Projectile.oldRot[k] + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
                spriteBatch.Draw(texture, drawPos - Projectile.oldPos[k] * 0.5f + Projectile.oldPos[k + 1] * 0.5f, null, color * 0.45f, Projectile.oldRot[k] * 0.5f + Projectile.oldRot[k + 1] * 0.5f + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
            }
            Texture2D basetexture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            Vector2 drawBaseOrigin = new Vector2(basetexture.Width * 0.5f, basetexture.Height * 0.5f);
            spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            spriteBatch.Draw(basetexture, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, drawBaseOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}
