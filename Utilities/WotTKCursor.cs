using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace WotTK.Utilities
{
    [Autoload(Side = ModSide.Client)]
    public class WotTKCursor : ModSystem
    {
        private static Asset<Texture2D> normalCursorTexture;
        private static Asset<Texture2D> smartCursorTexture;
        private static Asset<Texture2D> oreCursorTexture;
        private static Asset<Texture2D> oreSmartCursorTexture;
        private static Asset<Texture2D> swordCursorTexture;
        private static Asset<Texture2D> swordSmartCursorTexture;
        private static LegacyGameInterfaceLayer cursorLayer;


        private static Vector2 cursorPosition = Vector2.Zero;
        private static float cursorScale = 0.9f;


        private static readonly int pickaxeCursorWidth = 32;
        private static readonly int pickaxeCursorHeight = 32;

        private static readonly int swordCursorWidth = 32;
        private static readonly int swordCursorHeight = 32;

        public override void Load()
        {
            normalCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichCursor");
            smartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichSmartCursor");
            oreCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichOreCursor"); 
            oreSmartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichOreSmartCursor");
            swordCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichSwordCursor");
            swordSmartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichSwordSmartCursor");
            

            cursorLayer = new LegacyGameInterfaceLayer($"{nameof(WotTK)}: My Cursor", () => {

                if (!normalCursorTexture.IsLoaded || !smartCursorTexture.IsLoaded || !oreCursorTexture.IsLoaded || !oreSmartCursorTexture.IsLoaded || !swordCursorTexture.IsLoaded || !swordSmartCursorTexture.IsLoaded)
                {
                    return true;
                }

                var texture = normalCursorTexture.Value;
                Rectangle srcRect = new Rectangle(0, 0, 32, 32); // default cursor size, yes

                bool isUsingPickaxe = Main.LocalPlayer.HeldItem.pick > 0; // make sure if layer is using any pickaxe // fuck my life
                bool isUsingMeleeWeapon = Main.LocalPlayer.HeldItem.CountsAsClass(DamageClass.Melee); // the same for this shit... (will add more cursors for)

                if (isUsingPickaxe)
                {
                    if (Main.SmartCursorIsUsed)
                    {
                        texture = oreSmartCursorTexture.Value;
                    }
                    else
                    {
                        texture = oreCursorTexture.Value;
                    }

                    srcRect = new Rectangle(0, 0, pickaxeCursorWidth, pickaxeCursorHeight);

                    var basePosition = Main.MouseScreen + cursorPosition;
                    var drawColor = new Color(255, 255, 255, 255);

                    Main.spriteBatch.Draw(texture, basePosition, srcRect, drawColor, 0f, Vector2.Zero, cursorScale, SpriteEffects.None, 0f);

                    return true;
                }
                else if (isUsingMeleeWeapon)
                {
                    if (Main.SmartCursorIsUsed)
                    {
                        texture = swordSmartCursorTexture.Value;
                    }
                    else
                    {
                        texture = swordCursorTexture.Value;
                    }

                    srcRect = new Rectangle(0, 0, swordCursorWidth, swordCursorHeight);

                    var basePosition = Main.MouseScreen + cursorPosition;
                    var drawColor = new Color(255, 255, 255, 255);

                    Main.spriteBatch.Draw(texture, basePosition, srcRect, drawColor, 0f, Vector2.Zero, cursorScale, SpriteEffects.None, 0f);

                    return true;
                }
                else if (Main.SmartCursorIsUsed)
                {
                    texture = smartCursorTexture.Value;

                    var basePosition = Main.MouseScreen + cursorPosition;
                    var drawColor = new Color(255, 255, 255, 255);

                    Main.spriteBatch.Draw(texture, basePosition, srcRect, drawColor, 0f, Vector2.Zero, cursorScale, SpriteEffects.None, 0f);

                    return true;
                }
                else
                {
                    var basePosition = Main.MouseScreen + cursorPosition;
                    var drawColor = new Color(255, 255, 255, 255);

                    Main.spriteBatch.Draw(texture, basePosition, srcRect, drawColor, 0f, Vector2.Zero, cursorScale, SpriteEffects.None, 0f);

                    return true;
                }
            });
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int cursorIndex = layers.FindIndex(l => l.Name == "Vanilla: Cursor");
            if (cursorIndex >= 0)
                layers[cursorIndex] = cursorLayer;
        }

        public static void SetCursorPosition(Vector2 position)
        {
            cursorPosition = position;
        }

        public static void SetCursorScale(float scale)
        {
            cursorScale = scale;
        }
    }
}