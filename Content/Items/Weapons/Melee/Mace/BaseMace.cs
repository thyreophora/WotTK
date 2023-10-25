using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Humanizer.In;

namespace WotTK.Content.Items.Weapons.Melee.Mace
{
    public abstract class BaseMace : ModItem
    {
        int Timer = 0;
        public virtual int MaceUseTime => 10;
        public override void SetDefaults()
        {
            Item.DamageType = ModContent.GetInstance<PaladinDamageType>();

            Item.useTime = Item.useAnimation = MaceUseTime;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.channel = true;
            SafeSetDefaults();
        }
        public virtual void SafeSetDefaults()
        {

        }
        /*public override void HoldItem(Player player)
        {
            if (player.itemAnimation == 0)
            {
                Timer = 0;
                return;
            }
            if (player.itemAnimation == player.itemAnimationMax)
            {
                Timer = player.itemAnimationMax;
            }
            if (player.itemAnimation > 0)
            {
                Timer--;
            }
            if (Timer == 1)
            {
                HitOnGround(player);
            }
            //float cos = MathF.Cos(1.5f * Timer * MathF.PI / player.itemAnimationMax);
            //float a = cos / 2f + 0.5f;
            float a = MathF.Cos(4f * Timer * MathF.PI / player.itemAnimationMax / 3f - MathF.PI / 3f) / 2f + 0.5f;
            if (Timer > 0)
                player.itemAnimation = (int)(player.itemAnimationMax * a);
            else
                player.itemAnimation = 0;
        }
        public virtual void HitOnGround(Player player)
        {

        }*/
    }
    public abstract class BaseMaceProj<T> : ModProjectile where T : ModItem
    {
        public override string Texture => ModContent.GetInstance<T>().Texture;
        private Player Owner => Main.player[Projectile.owner];
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.timeLeft = 2;
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Projectile.width = texture.Width;
            Projectile.height = texture.Height;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 1;
            Projectile.DamageType = PaladinDamageType.Instance;
            Projectile.ownerHitCheck = true;
            Projectile.netUpdate = true;
            AIType = -1;

            SafeSetDefaults();
        }
        public virtual void SafeSetDefaults()
        {

        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((sbyte)Projectile.spriteDirection);
            //writer.Write(Hited);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.spriteDirection = reader.ReadSByte();
            //Hited = reader.ReadBoolean();
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 start = Owner.MountedCenter;
            Vector2 end = start + Projectile.rotation.ToRotationVector2() * Projectile.Size.Length() * (Projectile.scale) * Projectile.spriteDirection;
            float collisionPoint = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 10f * Projectile.scale, ref collisionPoint);
        }
        public override void CutTiles()
        {
            Vector2 start = Owner.MountedCenter;
            Vector2 end = start + Projectile.rotation.ToRotationVector2() * Projectile.Size.Length() * (Projectile.scale) * Projectile.spriteDirection;
            Utils.PlotTileLine(start, end, 10f * Projectile.scale, DelegateMethods.CutTiles);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 origin = new Vector2(Projectile.width, Projectile.height) / 2f;
            float rotationOffset;
            SpriteEffects effects;
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            if (Projectile.spriteDirection > 0)
            {
                rotationOffset = MathHelper.ToRadians(45f);
                effects = SpriteEffects.None;
            }
            else
            {
                rotationOffset = -MathHelper.ToRadians(45f);
                effects = SpriteEffects.FlipHorizontally;
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, default, lightColor * Projectile.Opacity, Projectile.rotation + rotationOffset, origin, Projectile.scale, effects, 0);

            return false;
        }
        private ref float Timer => ref Projectile.ai[0];
        //private ref float Hited => ref Projectile.ai[1];
        //private bool Hited = false;
        public override void OnSpawn(IEntitySource source)
        {
            Timer = Projectile.timeLeft = Owner.itemAnimationMax;
            Projectile.idStaticNPCHitCooldown = Owner.itemAnimationMax * 3 / 2;
            Projectile.spriteDirection = Main.MouseWorld.X > Owner.MountedCenter.X ? 1 : -1;
            Projectile.scale = Owner.HeldItem.scale;
            //Timer = Owner.itemAnimationMax;
            //Hited = false;
            Owner.GetModPlayer<WotTKPlayer>().maceHitOnGround = false;

            //Main.NewText(Projectile.scale);
        }
        public override void AI()
        {
            Owner.itemAnimation = 2;
            Owner.itemTime = 2;
            Owner.heldProj = Projectile.whoAmI;

            if (!Owner.active || Owner.dead || Owner.noItems || Owner.CCed)
            {
                Projectile.Kill();
                return;
            }

            Timer--;
            float percent = Timer / Owner.itemAnimationMax;
            float rot = MathF.Pow(MathF.Sin(percent * percent * MathF.PI * Projectile.spriteDirection), 3) * MathF.PI - MathHelper.ToRadians(135f);
            if (Projectile.spriteDirection < 0)
                rot += MathHelper.PiOver2 * Projectile.spriteDirection;
            Projectile.rotation = rot;
            Projectile.Center = Owner.MountedCenter + new Vector2(Projectile.spriteDirection, 0).RotatedBy(rot) * Projectile.Size.Length() / 2f * Projectile.scale;

            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.ToRadians(90f) * Projectile.spriteDirection);

            if (Timer == (int)(Owner.itemAnimationMax * 0.7f) && !Owner.GetModPlayer<WotTKPlayer>().maceHitOnGround)
            {
                HitOnGround(Owner, Owner.MountedCenter + new Vector2(Projectile.spriteDirection, 0).RotatedBy(rot) * Projectile.Size.Length() * Projectile.scale);
                Owner.GetModPlayer<WotTKPlayer>().maceHitOnGround = true;
            }
        }
        public virtual void HitOnGround(Player player, Vector2 hitCenter)
        {

        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = target.position.X > Owner.MountedCenter.X ? 1 : -1;
        }
    }
}
