using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.UI;

namespace WotTK
{
    public class WotTK : Mod
    {
        public static WotTK Instance;
        private WotTKCursorMod cursor;

        public override void Load()
        {
            Instance = this;
            cursor = new WotTKCursorMod();
        }

        public override void Unload()
        {
            Instance = null;
        }

        internal static void SaveConfig(WotTKConfig cfg)
        {
            try
            {
                MethodInfo method = typeof(ConfigManager).GetMethod("Save", (BindingFlags)40);
                if (method != null)
                    ((MethodBase)method).Invoke(null, new object[1]
                    {
                        cfg
                    });
            }
            catch
            {

            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int cursorLayerIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");

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
            cursorTexture = ModContent.GetTexture("WotTK/Textures/DefaultCursor");
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