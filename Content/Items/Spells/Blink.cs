using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WotTK.Content.Items.Spells
{
    public class Blink : LevelLockedItem
    {
        public override int MinimalLevel => 25;

        public override void SetDefaults()
        {
            Item.damage = 0;
            Item.noMelee = true;
            Item.channel = true;
            Item.rare = ItemRarityID.Pink;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.expert = true;
            Item.autoReuse = false;
            Item.shootSpeed = 0f;
            Item.noUseGraphic = true;

        }

        public override bool CanUseItem(Player player) => !player.HasBuff(ModContent.BuffType<BlinkCooldown>());


        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(new SoundStyle("WotTK/Sounds/Custom/BlinkSFX") with { PitchVariance = 0.3f, Volume = 1f }, player.Center);
            BlinkTeleport(player);
            return true;
        }

        private void BlinkTeleport(Player player)
        {
            if (!Collision.SolidCollision(Main.MouseWorld, player.width, player.height))
            {
                RunTeleport(player, Main.MouseWorld);
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
