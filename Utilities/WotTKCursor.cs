using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
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
        private static Asset<Texture2D> arrowCursorTexture;
        private static Asset<Texture2D> arrowSmartCursorTexture;
        private static LegacyGameInterfaceLayer cursorLayer;

        private static Vector2 cursorPosition = Vector2.Zero;
        private static float cursorScale = 1f;

        private static readonly int pickaxeCursorWidth = 32;
        private static readonly int pickaxeCursorHeight = 32;

        private static readonly int swordCursorWidth = 32;
        private static readonly int swordCursorHeight = 32;

        private static readonly int arrowCursorWidth = 32;
        private static readonly int arrowCursorHeight = 32;

        public override void Load()
        {
            normalCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichCursor");
            smartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichSmartCursor");
            oreCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichOreCursor"); 
            oreSmartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichOreSmartCursor");
            swordCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichSwordCursor");
            swordSmartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichSwordSmartCursor");
            arrowCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichArrowCursor");
            arrowSmartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/LichArrowSmartCursor");

            cursorLayer = new LegacyGameInterfaceLayer($"{nameof(WotTK)}: My Cursor", () => {
                if (!normalCursorTexture.IsLoaded || !smartCursorTexture.IsLoaded || !oreCursorTexture.IsLoaded || !oreSmartCursorTexture.IsLoaded || !swordCursorTexture.IsLoaded || !swordSmartCursorTexture.IsLoaded || !arrowCursorTexture.IsLoaded || !arrowSmartCursorTexture.IsLoaded)
                {
                    return true;
                }

                var texture = normalCursorTexture.Value;
                Rectangle srcRect = new Rectangle(0, 0, 32, 32); // default cursor size

                bool isUsingPickaxe = Main.LocalPlayer.HeldItem.pick > 0;
                bool isUsingMeleeWeapon = Main.LocalPlayer.HeldItem.CountsAsClass(DamageClass.Melee);
                bool isUsingRangedWeapon = Main.LocalPlayer.HeldItem.CountsAsClass(DamageClass.Ranged);

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
                }
                else if (isUsingRangedWeapon)
                {
                    if (Main.SmartCursorIsUsed)
                    {
                        texture = arrowSmartCursorTexture.Value;
                    }
                    else
                    {
                        texture = arrowCursorTexture.Value;
                    }

                    srcRect = new Rectangle(0, 0, arrowCursorWidth, arrowCursorHeight);
                }
                else if (Main.SmartCursorIsUsed)
                {
                    texture = smartCursorTexture.Value;
                }

                var basePosition = Main.MouseScreen + cursorPosition;
                var drawColor = new Color(255, 255, 255, 255);

                Main.spriteBatch.Draw(texture, basePosition, srcRect, drawColor, 0f, Vector2.Zero, cursorScale, SpriteEffects.None, 0f);

                return true;
            }, InterfaceScaleType.UI);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            // Eliminar capas de cursor predeterminadas
            List<string> vanillaCursors = new List<string> {
                "Cursor",
                "Mouse Over",
                "Cursor Info",
                "Mouse Item"
            };

            foreach (string cursorName in vanillaCursors)
            {
                int cursorIndex = layers.FindIndex(layer => layer.Name == $"Vanilla: {cursorName}");
                if (cursorIndex != -1)
                {
                    layers.RemoveAt(cursorIndex);
                }
            }

            // Insertar capa de cursor personalizado
            int defaultCursorIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Cursor");
            if (defaultCursorIndex != -1)
            {
                layers.Insert(defaultCursorIndex, cursorLayer);
            }
            else
            {
                layers.Add(cursorLayer);
            }
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