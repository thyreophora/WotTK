using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.DataStructures;
using WotTK.Mechanics.PrimitiveTrails;

namespace WotTK.Content.Items.Spells
{
    public class Sprint : LevelLockedItem
    {
        public override int MinimalLevel => 5;

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.rare = ItemRarityID.Pink;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.UseSound = new SoundStyle("WotTK/Sounds/Custom/SprintCast");

            Item.expert = true;
            Item.autoReuse = false;
            Item.shootSpeed = 0f;
            Item.noUseGraphic = true;
        }

        public override bool CanUseItem(Player player) => !player.HasBuff(ModContent.BuffType<SprintBuffCooldown>()) && player.velocity.Y == 0;

        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<SprintBuff>(), 480); // 8 seconds in ticks
            //player.AddBuff(ModContent.BuffType<SprintBuffCooldown>(), 7200); // 2 minutes in ticks

            CombatText.NewText(player.getRect(), Color.Yellow, "Sprint Activated!", true, false);

            return true;
        }
    }

    public class SprintBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; // Defines if the buff is a debuff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.5f; // Increase movement speed by 50%

            // Create white trail effect
            if (player.velocity != Vector2.Zero && Main.rand.NextBool(3))
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<SprintTrail>(), 0, 0, player.whoAmI);
            }
        }
    }

    public class SprintBuffCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; // Defines if the buff is a debuff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // No additional effect needed, just the presence of this buff is enough
        }
    }

    public class SprintTrail : ModProjectile
    {
        private PrimitiveTrail trail;
        private Texture2D trailTexture;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 20;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.timeLeft = 60; // Ajusta este valor según sea necesario
            Projectile.alpha = 255;

            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 90;
        }

        public override void OnSpawn(IEntitySource source)
        {
            trailTexture = ModContent.Request<Texture2D>("WotTK/Content/Items/Spells/SprintTrail").Value;
            trail = new PrimitiveTrail(trailTexture);
        }

        public override void AI()
        {
            if (++Projectile.frameCounter > 8)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Type];
            }

            Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * .025f / 255f, (255 - Projectile.alpha) * .25f / 255f, (255 - Projectile.alpha) * .05f / 255f);

            Player player = Main.player[Projectile.owner];
            Vector2 newPosition = player.Center - new Vector2(Projectile.width / 2, Projectile.height / 2);

            trail.AddPoint(newPosition);

            if (Projectile.velocity != Vector2.Zero)
                Projectile.rotation = Projectile.velocity.ToRotation();

            Projectile.alpha -= 25;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }

            Projectile.scale = 1f - (Projectile.alpha / 255f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            trail.Draw(Main.spriteBatch);
            return false;
        }
    }
}