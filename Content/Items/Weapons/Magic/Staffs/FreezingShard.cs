using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using WotTK.Utilities;
using Terraria.Audio;
using WotTK.Common;

namespace WotTK.Content.Items.Weapons.Magic.Staffs
{
    public class FreezingShard : LevelLockedItem
    {
        public static readonly SoundStyle iceCast = new("WotTK/Sounds/Casts/FreezingCast", 5);
        public override int MinimalLevel => 35;
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 60;
            Item.height = 10;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;

            Item.useTime = Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            
            Item.UseSound = iceCast;
            Item.autoReuse = true;

            Item.mana = 10;
            Item.damage = 45;
            Item.knockBack = 2f;
            Item.DamageType = DamageClass.Magic;

            Item.shoot = ModContent.ProjectileType<FreezingShardProj>();
            Item.shootSpeed = 20;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(source, position, velocity + Main.rand.NextVector2Square(-2, 2), type, (int)(damage / 3f), knockback, player.whoAmI);
            }
            return false;
        }
        public override Vector2? HoldoutOffset() => new Vector2?(new Vector2(-7f, 3f));
    }
    public class FreezingShardProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 1; Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Magic;

            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
        }
        public override bool PreDraw(ref Color lightColor)
        { 
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("WotTK/Textures/FreezingTrail");
            Vector2 drawOrigin = new Vector2(texture.Width * 0.4f, texture.Height * 0.4f);
            SpriteEffects effects = (Projectile.spriteDirection == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            for (int k = 0; k < Projectile.oldPos.Length - 1; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                Color color = new Color(157, 255, 255); // R, G, B 
                spriteBatch.Draw(texture, drawPos, null, color * 0.45f, Projectile.oldRot[k] + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
                spriteBatch.Draw(texture, drawPos - Projectile.oldPos[k] * 0.5f + Projectile.oldPos[k + 1] * 0.5f, null, color * 0.45f, Projectile.oldRot[k] * 0.5f + Projectile.oldRot[k + 1] * 0.5f + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
            }
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn2, 320);
        }

        public int TargetIndex = -1;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (TargetIndex >= 0)
            {
                if (!Main.npc[TargetIndex].active || !Main.npc[TargetIndex].CanBeChasedBy())
                {
                    TargetIndex = -1;
                }
                else
                {
                    Vector2 value = Projectile.SafeDirectionTo(Main.npc[TargetIndex].Center) * (Projectile.velocity.Length() * 1.5f);
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, value, 0.09f);
                }
            }

            if (TargetIndex == -1)
            {
                NPC nPC = Projectile.Center.ClosestNPCAt(125f);
                if (nPC != null)
                {
                    TargetIndex = nPC.whoAmI;
                }
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = Main.rand.Next(3);
        }

        public static readonly SoundStyle Impacts = new("WotTK/Sounds/SpellImpacts/FreezingShardImpact", 5);
        public override void OnKill(int timeLeft)
        {
            SoundStyle impactSound = Impacts;

            SoundEngine.PlaySound(impactSound, Projectile.position);
            for (int index1 = 0; index1 < 5; ++index1)
            {
                int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 68, 0f, 0f, 0, new Color(), 1f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 1.5f;
                Main.dust[index2].scale *= 1f;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.SnowflakeIce, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}
