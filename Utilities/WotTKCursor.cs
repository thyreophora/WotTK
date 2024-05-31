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
        //private static Asset<Texture2D> favoriteCursorTexture;
        //private static Asset<Texture2D> trashCursorTexture;
        //private static Asset<Texture2D> sellCursorTexture;
        //private static Asset<Texture2D> lootCursorTexture; // future cursor for looting enemies...

        private static Asset<Texture2D> oreCursorTexture;
        private static Asset<Texture2D> oreSmartCursorTexture;

        private static Asset<Texture2D> swordCursorTexture;
        private static Asset<Texture2D> swordSmartCursorTexture;
        
        private static Asset<Texture2D> arrowCursorTexture;
        private static Asset<Texture2D> arrowSmartCursorTexture;

        private static Asset<Texture2D> hammerCursorTexture;
        private static Asset<Texture2D> hammerSmartCursorTexture;

        private static Asset<Texture2D> axeCursorTexture;
        private static Asset<Texture2D> axeSmartCursorTexture;

        private static Asset<Texture2D> throwerCursorTexture;
        private static Asset<Texture2D> throwerSmartCursorTexture;

        private static LegacyGameInterfaceLayer cursorLayer;
    
        private static Vector2 cursorPosition = Vector2.Zero;
        private static float cursorScale = 1f;


        private static readonly Vector2 CursorSize = new(32,32);

        //private static readonly int pickaxeCursorWidth = 32;
        //private static readonly int pickaxeCursorHeight = 32;

        //private static readonly int swordCursorWidth = 32;
        //private static readonly int swordCursorHeight = 32;

        //private static readonly int arrowCursorWidth = 32;
        //private static readonly int arrowCursorHeight = 32;

        //private static readonly int hammerCursorWidth = 32;
        //private static readonly int hammerCursorHeight = 32;

        public override void Load()
        {
            normalCursorTexture = Mod.Assets.Request<Texture2D>("Textures/Cursors/HandCursor");
            smartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/Cursors/HandSmartCursor");

            oreCursorTexture = Mod.Assets.Request<Texture2D>("Textures/Cursors/PickCursor"); 
            oreSmartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/Cursors/PickSmartCursor");

            swordCursorTexture = Mod.Assets.Request<Texture2D>("Textures/Cursors/SwordCursor");
            swordSmartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/Cursors/SwordSmartCursor");
            
            arrowCursorTexture = Mod.Assets.Request<Texture2D>("Textures/Cursors/ArrowCursor");
            arrowSmartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/Cursors/ArrowSmartCursor");

            hammerCursorTexture = Mod.Assets.Request<Texture2D>("Textures/Cursors/HammerCursor");
            hammerSmartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/Cursors/HammerSmartCursor");

            axeCursorTexture = Mod.Assets.Request<Texture2D>("Textures/Cursors/AxeCursor");
            axeSmartCursorTexture = Mod.Assets.Request<Texture2D>("Textures/Cursors/AxeSmartCursor");

            cursorLayer = new LegacyGameInterfaceLayer($"{nameof(WotTK)}: My Cursor", () => {
                if (!normalCursorTexture.IsLoaded || !smartCursorTexture.IsLoaded || !oreCursorTexture.IsLoaded || !oreSmartCursorTexture.IsLoaded || !swordCursorTexture.IsLoaded || !swordSmartCursorTexture.IsLoaded || !arrowCursorTexture.IsLoaded || !arrowSmartCursorTexture.IsLoaded)
                {
                    return true;
                }

                var texture = normalCursorTexture.Value;
                Rectangle srcRect = new(0, 0, 32, 32); // default cursor size

                bool isUsingPickaxe = Main.LocalPlayer.HeldItem.pick > 0;
                bool isUsingHammer = Main.LocalPlayer.HeldItem.hammer > 0;
                bool isUsingAxe = Main.LocalPlayer.HeldItem.axe > 0;
                
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

                    srcRect = new Rectangle(0, 0, (int)CursorSize.X, (int)CursorSize.Y);
                }
                else if (isUsingHammer)
                {
                    if (Main.SmartCursorIsUsed)
                    {
                        texture = hammerSmartCursorTexture.Value;
                    }
                    else
                    {
                        texture = hammerCursorTexture.Value;
                    }

                    srcRect = new Rectangle(0, 0, (int)CursorSize.X, (int)CursorSize.Y);
                }
                else if (isUsingAxe)
                {
                    if (Main.SmartCursorIsUsed)
                    {
                        texture = axeSmartCursorTexture.Value;
                    }
                    else
                    {
                        texture = axeCursorTexture.Value;
                    }

                    srcRect = new Rectangle(0, 0, (int)CursorSize.X, (int)CursorSize.Y);
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

                    srcRect = new Rectangle(0, 0, (int)CursorSize.X, (int)CursorSize.Y);
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

                    srcRect = new Rectangle(0, 0, (int)CursorSize.X, (int)CursorSize.Y);
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
            List<string> vanillaCursors = [
                "Cursor",
                "Cursor Info",
                "Mouse Item"
            ];

            foreach (string cursorName in vanillaCursors)
            {
                int cursorIndex = layers.FindIndex(layer => layer.Name == $"Vanilla: {cursorName}");
                if (cursorIndex != -1)
                {
                    layers.RemoveAt(cursorIndex);
                }
            }

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

        //public static void SetCursorPosition(Vector2 position)
        //{
        //    cursorPosition = position;
        //}

        //public static void SetCursorScale(float scale)
        //{
        //    cursorScale = scale;
        //}
    }
}