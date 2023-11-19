using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;

namespace WotTK.Content.Items.Tools.Pickaxe
{
    public abstract class BasePickaxe : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            SafeSetDefaults();
        }
        public virtual void SafeSetDefaults()
        {

        }

    }
    public abstract class BasePickaxeProj<T> : ModProjectile where T : ModItem
    {
        public override string Texture => ModContent.GetInstance<T>().Texture;
        public Player Owner => Main.player[Projectile.owner];
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
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.spriteDirection = reader.ReadSByte();
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 start = Owner.MountedCenter;
            Vector2 end = start
                + Projectile.rotation.ToRotationVector2() * Projectile.Size.Length() * Projectile.scale * Projectile.spriteDirection;
                //- Projectile.rotation.ToRotationVector2() * PositionOffset * Projectile.spriteDirection;
            float collisionPoint = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 10f * Projectile.scale, ref collisionPoint);
        }
        public override void CutTiles()
        {
            Vector2 start = Owner.MountedCenter;
            //Vector2 end = start + Projectile.rotation.ToRotationVector2() * (Projectile.Size.Length() - PositionOffset) * Projectile.scale * Projectile.spriteDirection;
            Vector2 end = start
                + Projectile.rotation.ToRotationVector2() * Projectile.Size.Length() * Projectile.scale * Projectile.spriteDirection;
                //- Projectile.rotation.ToRotationVector2() * PositionOffset * Projectile.spriteDirection;

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
        public ref float Timer => ref Projectile.ai[0];
        public override void OnSpawn(IEntitySource source)
        {
            Timer = Projectile.timeLeft = Owner.itemAnimationMax;
            Projectile.idStaticNPCHitCooldown = Owner.itemAnimationMax;
            Projectile.spriteDirection = Main.MouseWorld.X > Owner.MountedCenter.X ? 1 : -1;
            Projectile.scale = Owner.HeldItem.scale;
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
            float baseSinRot = MathF.Pow(-MathF.Sin(percent * MathF.PI / 2f + MathF.PI), 5) * MathF.PI * 3 / 2;
            float rot = baseSinRot;
            Vector2 center = Owner.MountedCenter
                + new Vector2(Projectile.spriteDirection, 0).RotatedBy(rot) * Projectile.Size.Length() / 2f * Projectile.scale;

            Projectile.rotation = rot;
            Projectile.Center = center;

            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.ToRadians(90f) * Projectile.spriteDirection);

        }
    }
}
