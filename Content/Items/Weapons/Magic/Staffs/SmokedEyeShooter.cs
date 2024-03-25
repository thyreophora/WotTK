using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WotTK.Utilities;
using WotTK.Common;

namespace WotTK.Content.Items.Weapons.Magic.Staffs
{
    public class SmokedEyeShooter : LevelLockedItem
    {
        public override int MinimalLevel => 15;
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 60;
            Item.value = 20000;
            Item.rare = ItemRarityID.Green;

            Item.useTime = Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.NPCHit8;
            Item.autoReuse = true;

            Item.mana = 8;
            Item.damage = 18;
            Item.knockBack = 1f;
            Item.DamageType = DamageClass.Magic;

            Item.shoot = ModContent.ProjectileType<SmokedEyeShooterProj>();
            Item.shootSpeed = 10;
        }
    }
    public class SmokedEyeShooterProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;

            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;

        }


        public override bool PreDraw(ref Color lightColor)
        { // doing light trail
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("WotTK/Textures/SmookedTrail");
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            SpriteEffects effects = (Projectile.spriteDirection == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            for (int k = 0; k < Projectile.oldPos.Length - 1; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                Color color = new Color(255, 0, 0); // R, G, B 
                spriteBatch.Draw(texture, drawPos, null, color * 0.45f, Projectile.oldRot[k] + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
                spriteBatch.Draw(texture, drawPos - Projectile.oldPos[k] * 0.5f + Projectile.oldPos[k + 1] * 0.5f, null, color * 0.45f, Projectile.oldRot[k] * 0.5f + Projectile.oldRot[k + 1] * 0.5f + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
            }
            return true;
        }

        public int TargetIndex = -1;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            int num623 = Dust.NewDust(Projectile.Center, 4, 4, DustID.Blood, 0f, 0f, 0, default, 1f);


            Color dustColor = new Color(255, 0, 0); // R, G, B 

            if (TargetIndex >= 0)
            {
                if (!Main.npc[TargetIndex].active || !Main.npc[TargetIndex].CanBeChasedBy())
                {
                    TargetIndex = -1;
                }
                else
                {
                    Vector2 value = Projectile.SafeDirectionTo(Main.npc[TargetIndex].Center) * (Projectile.velocity.Length() + 3.5f);
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, value, 0.05f);
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
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(10, 0).RotatedByFullRandom(), ModContent.ProjectileType<SmokedEyeShooterProj2>(), Projectile.damage / 2, Projectile.knockBack / 2, Projectile.owner);
            }
        }
    }
    public class SmokedEyeShooterProj2 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;

            //ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            //ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
        }

        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.Blood, Projectile.velocity / 2);
            //dust.scale = 2f;
            Projectile.velocity.Y += 0.1f;
            if (Projectile.timeLeft == 580)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc == null || !npc.active) continue;
                    float distance = (npc.Center - Projectile.Center).Length();
                    if (distance < 1600f)
                    {
                        Projectile.velocity = (npc.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * Projectile.velocity.Length();
                    }
                }
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            return Projectile.timeLeft < 580;
        }
    }
}
