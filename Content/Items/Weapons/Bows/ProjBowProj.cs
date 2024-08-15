using Microsoft.Build.Construction;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Weapons.Bows
{
    public class ProjBowProj : ModProjectile
    {
        private const int NumAnimationFrames = 5;
        private const float AimResponsiveness = 0.15f;
        private const int ChargeTimePerLevel = 60;
        private int chargeLevel;
        private int chargeTimer;
        private bool hasShot; 

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = NumAnimationFrames;
            ProjectileID.Sets.NeedsUUID[Projectile.type] = true;
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.timeLeft = 100000000;
            chargeLevel = 0;
            chargeTimer = 0;
            hasShot = false;
            Projectile.height = 20;
            Projectile.width = 20;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 playerHandPos = player.RotatedRelativePoint(player.MountedCenter, true);

            UpdateCharge(player);
            UpdateAnimation();
            UpdatePlayerVisuals(player, playerHandPos);

            if (Projectile.owner == Main.myPlayer)
            {
                UpdateAim(playerHandPos, player.HeldItem.shootSpeed);

                bool stillInUse = player.channel && !player.noItems && !player.CCed;
                if (!stillInUse && !hasShot)
                {
                    HandleShooting(playerHandPos);
                    hasShot = true;
                    Projectile.Kill();
                }
            }

        }

        private void UpdateCharge(Player player)
        {
            if (chargeLevel < 5)
            {
                chargeTimer++;
                if (chargeTimer >= ChargeTimePerLevel)
                {
                    chargeTimer = 0;
                    chargeLevel++;
                    SoundEngine.PlaySound(SoundID.Item6, Projectile.position);
                    for (int i = 0; i < chargeLevel * 2; i++)
                    {
                        Vector2 dustPos = Projectile.Center + Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi) * Projectile.width / 2;
                        Dust.NewDustPerfect(dustPos, DustID.GreenMoss, null, 0, default, 1.5f);
                    }
                }
            }
        }

        private void UpdateAnimation()
        {
            if (Projectile.frame < NumAnimationFrames - 1) 
            {
                Projectile.frameCounter++;
                int framesPerUpdate = 12; 
                if (Projectile.frameCounter >= framesPerUpdate)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                    if (Projectile.frame >= NumAnimationFrames - 1)
                    {
                        Projectile.frame = NumAnimationFrames - 1; 
                    }
                }
            }
        }

        private void UpdatePlayerVisuals(Player player, Vector2 playerHandPos)
        {
            Projectile.Center = playerHandPos;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
        }

        private void UpdateAim(Vector2 source, float speed)
        {
            Vector2 aim = Vector2.Normalize(Main.MouseWorld - source);
            if (aim.HasNaNs())
            {
                aim = -Vector2.UnitY;
            }
            aim = Vector2.Normalize(Vector2.Lerp(Vector2.Normalize(Projectile.velocity), aim, AimResponsiveness));
            aim *= speed;
            if (aim != Projectile.velocity)
            {
                Projectile.netUpdate = true;
            }
            Projectile.velocity = aim;
        }

     private void HandleShooting(Vector2 source)
     {
         if (chargeLevel > 0)
         {
             Vector2 shootDirection = Projectile.velocity;
             shootDirection.Normalize();
             shootDirection *= 10f + chargeLevel * 2.4f; 

             Vector2 spawnPos = source;

             int damage = Projectile.damage + chargeLevel * 6; 
             Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawnPos, shootDirection, ModContent.ProjectileType<PoisonArrow>(), damage, Projectile.knockBack, Projectile.owner);
             SoundEngine.PlaySound(SoundID.Item5, Projectile.position);
             for (int i = 0; i < chargeLevel * 2; i++)
             {
                 Vector2 dustPos = Projectile.Center + Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi) * Projectile.width / 2;
                 Dust.NewDustPerfect(dustPos, DustID.GreenMoss, null, 0, default, 1.5f);
             }
         }
     }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int spriteSheetOffset = frameHeight * Projectile.frame;
            Vector2 sheetInsertPosition = (Projectile.Center + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            Color drawColor = Color.White;
            Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0f);
            return false;
        }
    }
}
