using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using WotTK.Utilities;
using Terraria.Audio;

namespace WotTK.Content.Items.Weapons.Magic.Staffs
{
    public class FreezingShard : LevelLockedItem
    {
        public override int MinimalLevel => 32;
        public override bool IsWeapon => true;
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 10;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;

            Item.useTime = Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = new SoundStyle("WotTK/Sounds/Custom/WandBaseSound");
            Item.autoReuse = true;

            Item.mana = 10;
            Item.damage = 30;
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
            Projectile.width = 22; Projectile.height = 14;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Magic;
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
                    Vector2 value = Projectile.SafeDirectionTo(Main.npc[TargetIndex].Center) * (Projectile.velocity.Length() * 2f);
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, value, 0.03f);
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
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = Main.rand.Next(3);
        }
        
        public override void OnKill(int timeLeft)
        {
            SoundStyle impactSound = new SoundStyle("WotTK/Sounds/SpellImpacts/FreezingShardImpact");

            SoundEngine.PlaySound(impactSound, Projectile.position);
            for (int index1 = 0; index1 < 5; ++index1)
            {
                int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 68, 0f, 0f, 0, new Color(), 1f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 1.5f;
                Main.dust[index2].scale *= 0.9f;
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
