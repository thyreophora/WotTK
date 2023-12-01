using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace WotTK.Common
{
    public class WotTKSystem : ModSystem
    {
        private WotTKCursorMod cursor;
        public override void Load()
        {
            cursor = new WotTKCursorMod();
        }

        public override void Unload()
        {
            cursor = null;
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            /*int cursorLayerIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Cursor");

            if (cursorLayerIndex != -1)
            {
                layers[cursorLayerIndex] = new LegacyGameInterfaceLayer(
                    "WotTK: DefaultCursor",
                    delegate
                    {
                        cursor.Update();
                        cursor.Draw();

                        return true;
                    },
                    InterfaceScaleType.UI);
            }*/
            if (Main.cursorOverride == -1)
            {
                for (int i = 0; i < layers.Count; i++)
                {
                    if (layers[i].Name.Equals("Vanilla: Cursor"))
                    {
                        //This only removes the default cursor, see DetourSecondCursor for the second one
                        layers[i].Active = false;
                        //layers.RemoveAt(i); //Do not remove layers, mods with no defensive code cause this to break/delete all UI
                        layers.Insert(++i, new LegacyGameInterfaceLayer(
                            "WotTK: DefaultCursor",
                            delegate
                            {
                                cursor.Update();
                                cursor.Draw();

                                return true;
                            },
                            InterfaceScaleType.UI));
                        break;
                    }
                }
                //cursor.Update();
                //cursor.Draw();
            }
        }
    }
    public class WotTKCursorMod
    {
        private int cursorX;
        private int cursorY;

        private Texture2D cursorTexture;

        private int cursorWidth = 30;
        private int cursorHeight = 28;

        public WotTKCursorMod()
        {
            cursorTexture = ModContent.Request<Texture2D>("WotTK/Textures/DefaultCursor").Value;
        }

        public void Update()
        {
            cursorX = Main.mouseX;
            cursorY = Main.mouseY;

        }

        public void Draw()
        {
            Main.spriteBatch.Draw(cursorTexture, new Vector2(cursorX, cursorY), Color.White);
        }
    }

}
