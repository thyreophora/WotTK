using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.UI;

namespace WotTK.Utilities
{
    public class WotTKCursor : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int cursorLayerIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Cursor");

            if (cursorLayerIndex != -1)
            {
                layers[cursorLayerIndex] = new LegacyGameInterfaceLayer(
                    "WotTK: LichCursor",
                    delegate
                    {
                        return true;
                    },
                    InterfaceScaleType.UI
                );
            }
        }

        public class WotTKCursorMod
        {
            private int cursorX;
            private int cursorY;

            private Texture2D defaultcursor;

            private int cursorWidth = 30;
            private int cursorHeight = 28;

            public WotTKCursorMod()
            {
                defaultcursor = (Texture2D)ModContent.Request<Texture2D>("WotTK/Textures/LichCursor");
            }

            public void Update()
            {
                cursorX = Main.mouseX;
                cursorY = Main.mouseY;

            }

            public void Draw()
            {
                Main.spriteBatch.Draw(defaultcursor, new Vector2(cursorX, cursorY), Color.White);
            }
        }
    }
}