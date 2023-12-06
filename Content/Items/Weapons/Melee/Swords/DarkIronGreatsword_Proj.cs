using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WotTK.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Weapons.Melee.Swords
{
    public class DarkIronGreatsword_Proj : ModProjectile
    {
        public float[] oldrot = new float[6];
        public override string Texture => "WotTK/Content/Items/Weapons/Melee/Swords/DarkIronGreatsword";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool ShouldUpdatePosition() => false;
        public override void SetDefaults()
        {
            Projectile.width = 76;
            Projectile.height = 88;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Length = 80;
            Rot = MathHelper.ToRadians(3);
            Projectile.usesLocalNPCImmunity = true;
            Projectile.extraUpdates = 1;
        }
        private Vector2 startVector;
        private Vector2 vector;
        private Vector2 mouseOrig;
        public ref float Length => ref Projectile.localAI[0];
        public ref float Rot => ref Projectile.localAI[1];

        public float Timer;

        private float speed;
        private float SwingSpeed = 5f;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.noItems || player.CCed || player.dead || !player.active)
                Projectile.Kill();

            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            Projectile.Center = player.MountedCenter + vector;

            Projectile.spriteDirection = player.direction;
            if (Projectile.spriteDirection == 1)
                Projectile.rotation = (Projectile.Center - player.Center).ToRotation() + MathHelper.PiOver4;
            else
                Projectile.rotation = (Projectile.Center - player.Center).ToRotation() - MathHelper.Pi - MathHelper.PiOver4;

            if (Main.myPlayer == Projectile.owner)
            {
                switch (Projectile.ai[0])
                {
                    case 0:
                        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (player.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2);
                        if (Timer == 0)
                        {
                            mouseOrig = Main.MouseWorld;
                            SoundEngine.PlaySound(SoundID.Item71, player.position);
                            startVector = (mouseOrig - player.Center).SafeNormalize(Vector2.UnitX);
                            speed = MathHelper.ToRadians(Main.rand.Next(6, 3));
                        }
                        if (Timer++ == (int)(5 * SwingSpeed))
                        {
                            Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.Center,
                                (mouseOrig - player.Center).SafeNormalize(Vector2.UnitX) * Main.rand.Next(45, 66),
                                ModContent.ProjectileType<DarkIronGreatswordSlash_Proj>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        }
                        if (Timer < 5 * SwingSpeed)
                        {
                            Rot += speed / SwingSpeed * Projectile.spriteDirection;
                            speed += 0.14f;
                            vector = startVector.RotatedBy(Rot) * Length;
                        }
                        else
                        {
                            Rot += speed / SwingSpeed * Projectile.spriteDirection;
                            speed *= 0.8f;
                            vector = startVector.RotatedBy(Rot) * Length;
                        }
                        if (Timer >= Main.rand.Next(28, 33) * SwingSpeed)
                        {
                            if (!player.channel)
                            {
                                Projectile.Kill();
                                return;
                            }
                            if (Main.MouseWorld.X < player.Center.X)
                                player.direction = -1;
                            else
                                player.direction = 1;
                            startVector = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX);
                            mouseOrig = Main.MouseWorld;
                            Projectile.alpha = 255;
                            SoundEngine.PlaySound(SoundID.Item71, Projectile.position);
                            Projectile.ai[0]++;
                            Timer = 0;
                            Projectile.netUpdate = true;
                        }
                        break;
                    case 1:
                        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (player.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2);
                        if (Timer++ == (int)(5 * SwingSpeed))
                        {
                            Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.Center,
                                (mouseOrig - player.Center).SafeNormalize(Vector2.UnitX) * Main.rand.Next(45, 66),
                                ModContent.ProjectileType<DarkIronGreatswordSlash_Proj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 1);
                        }
                        if (Timer < 5 * SwingSpeed)
                        {
                            Rot -= speed / SwingSpeed * Projectile.spriteDirection;
                            speed += 0.5f;
                            vector = startVector.RotatedBy(Rot) * Length;
                        }
                        else
                        {
                            Rot -= speed / SwingSpeed * Projectile.spriteDirection;
                            speed *= 0.5f;
                            vector = startVector.RotatedBy(Rot) * Length;
                        }
                        if (Timer >= Main.rand.Next(28, 33) * SwingSpeed)
                            Projectile.Kill();
                        break;
                }
            }

            if (Timer < 8)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile target = Main.projectile[i];
                    if (!target.active || target.whoAmI == Projectile.whoAmI || !target.hostile)
                        continue;

                    if (target.damage > 100 / 4 || Projectile.alpha > 0 || target.width + target.height > Projectile.width + Projectile.height)
                        continue;

                    SoundEngine.PlaySound(SoundID.Tink, Projectile.position);
                    target.Kill();
                }
            }

            if (Timer > 1)
                Projectile.alpha = 0;

            for (int k = Projectile.oldPos.Length - 1; k > 0; k--)
                oldrot[k] = oldrot[k - 1];

            oldrot[0] = Projectile.rotation;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return Timer <= 8 && Projectile.ai[0] < 2 ? null : false;
        }
    }
}
