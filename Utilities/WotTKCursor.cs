using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace WotTK.Utilities
{
    [Autoload(Side = ModSide.Client)]
    public class WotTKCursor : ModSystem
    {
        private static Asset<Texture2D> meterTexture;
        private static LegacyGameInterfaceLayer layer;
        public override void Load()
        {
            meterTexture = Mod.Assets.Request<Texture2D>("Textures/LichCursor");

            layer = new LegacyGameInterfaceLayer($"{nameof(WotTK)}: My Cursor", () => {

                if (!meterTexture.IsLoaded)
                {
                    return true;
                }

                var texture = meterTexture.Value;
                var basePosition = new Vector2(Main.mouseX, Main.mouseY);

                var drawColor = new Color(255, 255, 255, 255);
                var srcRect = new Rectangle(0, 0, 32, 32);

                Main.spriteBatch.Draw(texture, basePosition, srcRect, drawColor, 0f, new Vector2(6f, 6f), 1f, SpriteEffects.None, 0f);

                return true;
            });
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int preferredIndex = layers.FindIndex(l => l.Name == "Vanilla: Cursor");
            if (preferredIndex >= 1)
                layers[preferredIndex] = layer;
        }
    }
}