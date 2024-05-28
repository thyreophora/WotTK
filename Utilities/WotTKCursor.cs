using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace WotTK.Utilities
{
    [Autoload(Side = ModSide.Client)]
    public class WotTKCursor : ModSystem
    {
        private static Asset<Texture2D> normalCursorTexture;
        private static Asset<Texture2D> smartCursorTexture;
        private static LegacyGameInterfaceLayer layer;

        // new property for mouse scale (default is 1f)
        private static float scale = 0.8f;

        public override void Load()
        {
            normalCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichCursor");
            smartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichSmartCursor");

            layer = new LegacyGameInterfaceLayer($"{nameof(WotTK)}: My Cursor", () => {

                if (!normalCursorTexture.IsLoaded || !smartCursorTexture.IsLoaded)
                {
                    return true;
                }

                var texture = Main.SmartCursorIsUsed ? smartCursorTexture.Value : normalCursorTexture.Value;

                var basePosition = new Vector2(Main.mouseX, Main.mouseY);
                var drawColor = new Color(255, 255, 255, 255);
                var srcRect = new Rectangle(0, 0, 32, 32);

                // apply scale for mouse drawing
                Main.spriteBatch.Draw(texture, basePosition, srcRect, drawColor, 0f, new Vector2(3f, 3f), scale, SpriteEffects.None, 0f);

                

                return true;
            });
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int preferredIndex = layers.FindIndex(l => l.Name == "Vanilla: Cursor");
            if (preferredIndex >= 1)
                layers[preferredIndex] = layer;

        }

        // method for changing cursor scale
        public static void SetCursorScale(float newScale)
        {
            scale = newScale;
        }
    }
}