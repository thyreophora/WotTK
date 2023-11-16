using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using WotTK.Common.Utils;

namespace WotTK.Content.Items.Weapons.Magic.Staffs
{
    public class SmokedEyeShooter : ModItem
    {
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

            Item.mana = 6;
            Item.damage = 10;
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
            Projectile.tileCollide = false;

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
                    Vector2 value = Projectile.SafeDirectionTo(Main.npc[TargetIndex].Center) * (Projectile.velocity.Length() + 3.5f);
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
