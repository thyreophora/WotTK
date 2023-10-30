using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Biomes;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Weapons.Melee.Mace
{
    public abstract class BaseMace : ModItem
    {
        /// <summary>
        /// Use time of weapon
        /// Used in Item.useTime and Item.useAnimation
        /// </summary>
        public virtual int MaceUseTime => 10;
        /// <summary>
        /// Minimal level for use weapon
        /// from [N-10; N] damage is [1 dmg; 100% of dmg]
        /// at 0<N<10= damage is CurrectLvl/N % of dmg (max 100% or CurrectLvl==N)
        /// At default value (0) damage always is 100%
        /// </summary>
        public virtual int MinimalPlayerLevel => 0;
        public override void SetDefaults()
        {
            Item.DamageType = ModContent.GetInstance<PaladinDamageType>();

            Item.useTime = Item.useAnimation = MaceUseTime;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            //Item.channel = true;
            SafeSetDefaults();
        }
        public virtual void SafeSetDefaults()
        {

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float level = (float)player.GetModPlayer<WotTKPlayer>().playerLevel;
            float percent = 1f;
            int damage2 = damage;

            if (MinimalPlayerLevel == 0)
                percent = 1f;
            else if (MinimalPlayerLevel <= 10 && MinimalPlayerLevel > 0)
            {
                percent = level / MinimalPlayerLevel;
                if (level > MinimalPlayerLevel)
                    percent = 1f;
            }
            else
            {
                percent = 1f - (MinimalPlayerLevel - level) / 10f;

                if (MinimalPlayerLevel - level >= 10) //Low when minimal
                {
                    percent = 1;
                    damage2 = 1;
                }
                if (MinimalPlayerLevel - level <= 0) //High and Equal when minimal
                {
                    percent = 1;
                }
            }
            if (WotTKConfig.Instance.Debug)
                damage2 = damage;
            Projectile.NewProjectile(source, position, velocity, type, (int)(damage2 * percent), knockback, player.whoAmI);
            return false;
        }
    }
    public abstract class BaseMaceProj<T> : ModProjectile where T : ModItem
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
                + Projectile.rotation.ToRotationVector2() * Projectile.Size.Length() * Projectile.scale * Projectile.spriteDirection
                - Projectile.rotation.ToRotationVector2() * PositionOffset * Projectile.spriteDirection;
            float collisionPoint = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 10f * Projectile.scale, ref collisionPoint);
        }
        public override void CutTiles()
        {
            Vector2 start = Owner.MountedCenter;
            //Vector2 end = start + Projectile.rotation.ToRotationVector2() * (Projectile.Size.Length() - PositionOffset) * Projectile.scale * Projectile.spriteDirection;
            Vector2 end = start
                + Projectile.rotation.ToRotationVector2() * Projectile.Size.Length() * Projectile.scale * Projectile.spriteDirection
                - Projectile.rotation.ToRotationVector2() * PositionOffset * Projectile.spriteDirection;

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
        /// <summary>
        /// Changes position in world regarding player
        /// </summary>
        public virtual float PositionOffset => 0;
        /// <summary>
        /// Changes position of HitOnGround
        /// </summary>
        public virtual float HeadOffset => 0;
        /// <summary>
        /// Can value of i-frames per hit
        /// That is, you can make more hits on target per use
        /// </summary>
        public virtual float ImmunityFramePercent => 1f;
        public Vector2 HammerCenter = new Vector2(0, 0);
        public virtual void ExtraRotation(float baseSinRot, ref float rot)
        {

        }
        public override void OnSpawn(IEntitySource source)
        {
            Timer = Projectile.timeLeft = Owner.itemAnimationMax;
            Projectile.idStaticNPCHitCooldown = (int)(Owner.itemAnimationMax * ImmunityFramePercent);
            Projectile.spriteDirection = Main.MouseWorld.X > Owner.MountedCenter.X ? 1 : -1;
            Projectile.scale = Owner.HeldItem.scale;
            Owner.GetModPlayer<WotTKPlayer>().maceHitOnGround = false;


            //Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            //Projectile.width = (int)(texture.Width * Projectile.scale);
            //Projectile.height = (int)(texture.Height * Projectile.scale);
        }
        //I don t understand how to fix 3x trigering of AI()
        //extraUpdates is 0 but don t work
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

            //Base Rotation
            Timer--;
            float percent = Timer / Owner.itemAnimationMax;
            float baseSinRot = MathF.Pow(MathF.Sin(percent * percent * MathF.PI * Projectile.spriteDirection), 5) * MathF.PI;
            float rot = baseSinRot - MathHelper.ToRadians(135f);
            rot += Projectile.spriteDirection < 0 ? -MathHelper.PiOver2 : 0;
            ExtraRotation(baseSinRot, ref rot);
            Vector2 center = Owner.MountedCenter
                + new Vector2(Projectile.spriteDirection, 0).RotatedBy(rot) * Projectile.Size.Length() / 2f * Projectile.scale;
            //rot += MathHelper.ToRadians(15f) * Projectile.spriteDirection;

            //Base AI
            Projectile.rotation = rot;
            Projectile.Center = center
                - new Vector2(Projectile.spriteDirection, 0).RotatedBy(rot) * (PositionOffset + 2f * Projectile.scale);

            //Hand pos
            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.ToRadians(90f) * Projectile.spriteDirection);

            //Hammer Effect
            HammerCenter = center
                + new Vector2(Projectile.spriteDirection, 0).RotatedBy(rot) * Projectile.Size.Length() / 2f * Projectile.scale
                - new Vector2(Projectile.spriteDirection, 0).RotatedBy(rot) * HeadOffset;
            if (
                (
                    Timer == (int)(Owner.itemAnimationMax * 0.65f) 
                    /*|| 
                    (
                        Projectile.rotation > (Projectile.spriteDirection > 0 ? -0.1f : MathHelper.ToRadians(-350)) 
                        && 
                        Projectile.rotation < (Projectile.spriteDirection > 0 ? MathHelper.ToRadians(10) : -MathHelper.TwoPi)
                    )*/
                ) 
                && 
                !Owner.GetModPlayer<WotTKPlayer>().maceHitOnGround
            )
            {
                //HitOnGround(Owner, Owner.MountedCenter + new Vector2(Projectile.spriteDirection, 0).RotatedBy(rot) * (Projectile.Size.Length() - PositionOffset) * Projectile.scale, ref Projectile.damage, ref Projectile.knockBack);
                //HitOnGround(Owner, hammerPos, ref Projectile.damage, ref Projectile.knockBack);
                OnHitGround(Owner, new Vector2(HammerCenter.X, Owner.position.Y + Owner.height - 5f), ref Projectile.damage, ref Projectile.knockBack);
                Owner.GetModPlayer<WotTKPlayer>().maceHitOnGround = true;
            }
        }
        /// <summary>
        /// Make a effect than hammer is horizontally
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="hitCenter">Position of hammer head (can be changed by HeadOffset)</param>
        /// <param name="damage">Damage of weapon</param>
        /// <param name="kb">Knockback of Weapon</param>
        public virtual void OnHitGround(Player player, Vector2 hitCenter, ref int damage, ref float kb)
        {

        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = target.position.X > Owner.MountedCenter.X ? 1 : -1;
            SafeModifyHitNPC(target, ref modifiers);
        }
        public virtual void SafeModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

        }
    }
}
