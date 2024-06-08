using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WotTK.Common;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Spells
{
    public class Blink : LevelLockedItem
    {
        public override int MinimalLevel => 15;

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.rare = ItemRarityID.Pink;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.expert = true;
            Item.autoReuse = false;
            Item.shootSpeed = 0f;
            Item.noUseGraphic = true;
        }

        public override bool CanUseItem(Player player) => !player.HasBuff(ModContent.BuffType<BlinkCooldown>()) && player.velocity.Y == 0;

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(new SoundStyle("WotTK/Sounds/Custom/BlinkSFX") with { PitchVariance = 0.3f, Volume = 1f }, player.Center);
            BlinkTeleport(player);
            return true;
        }

        private void BlinkTeleport(Player player)
        {
            Vector2 blinkPosition = Main.MouseWorld;

            float maxDistance = 20f * 16f;
            Vector2 teleportDirection = blinkPosition - player.Center;
            if (teleportDirection.Length() > maxDistance)
            {
                teleportDirection.Normalize();
                blinkPosition = player.Center + teleportDirection * maxDistance;
            }

            if (!Collision.SolidCollision(blinkPosition, player.width, player.height))
            {
                RunTeleport(player, blinkPosition);
                player.AddBuff(ModContent.BuffType<BlinkCooldown>(), 10 * 60);
            }
        }

        private static void RunTeleport(Player player, Vector2 pos)
        {
            player.Teleport(pos, 2, 0);
            player.velocity = Vector2.Zero;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Lighting.AddLight(Item.position, 0.2f, .142f, .032f);

            float Timer = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3) / 2 + 0.5f;

            void DrawTex(Texture2D tex, float opacity, Vector2? offset = null) => spriteBatch.Draw(tex, Item.Center + (offset ?? Vector2.Zero) - Main.screenPosition, null, Color.White * opacity, rotation, tex.Size() / 2, scale, SpriteEffects.None, 0);

            for (int i = 0; i < 6; i++)
            {
                Vector2 drawPos = Vector2.UnitX.RotatedBy(i / 6f * MathHelper.TwoPi) * Timer * 6;
            }
        }
    }

    public class BlinkCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
    }
}
