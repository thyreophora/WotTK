using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace WotTK.Content.UI
{
    public class LevelBarPart : UIImage
    {
        private float percent;
        public LevelBarPart(Asset<Texture2D> texture, float percent) : base(texture)
        {
            this.percent = percent;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<WotTKPlayer>();
            float quotient = (float)modPlayer.playerLevelPoints / modPlayer.playerLevelPointsNeed;
            quotient = Utils.Clamp(quotient, 0f, 1f);

            if (quotient < percent)
                return;

            base.Draw(spriteBatch);

        }
    }
    public class LevelBar : UIState
    {
        public DraggableUIPanel dragPanel;
        private UIImage barFrame;
        private LevelBarPart[] barPart = new LevelBarPart[100];
        private UIText text;
        private UIImage iconLevel;
        private UIText text2;
        private UIImage iconXP;
        private UIText text3;
        public override void OnInitialize()
        {
            dragPanel = new DraggableUIPanel();
            dragPanel.SetPadding(0);
            SetRectangle(dragPanel, left: WotTKConfig.Instance.LevelBarX * Main.screenWidth, top: WotTKConfig.Instance.LevelBarY * Main.screenHeight, width: 208f, height: 16f);
            //SetRectangle(dragPanel, left: 400, top: 100, width: 208f, height: 16f);
            //CoinCounterPanel.BackgroundColor = new Color(73, 94, 171);

            barFrame = new UIImage(ModContent.Request<Texture2D>("WotTK/Content/UI/LevelBarFrame"));
            SetRectangle(barFrame, 0, 0, 208, 12);
            dragPanel.Append(barFrame);

            for (int i = 0; i < 100; i++)
            {
                barPart[i] = new LevelBarPart(ModContent.Request<Texture2D>("WotTK/Content/UI/LevelBarPart" + ((i == 0 || i == 99) ? "Edge" : "")), 0.01f * (i + 1));
                SetRectangle(barPart[i], i * 2 + 4, 4, 2, 4);
                dragPanel.Append((UIImage)barPart[i]);
                //barPart[i].
            }

            text = new UIText("Level: 0\nPoints: 0/0", 0.8f);
            SetRectangle(text, 0, -20, 208, 32);
            dragPanel.Append(text); //Can be removed to remove text from UI

            Append(dragPanel);
        }
        /*public override void Update(GameTime gameTime)
        {
            dragPanel.SetConfigValue(ref WotTKConfig.Instance.LevelBarX, ref WotTKConfig.Instance.LevelBarY);
        }*/
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            dragPanel.SetConfigValue(ref WotTKConfig.Instance.LevelBarX, ref WotTKConfig.Instance.LevelBarY);

            if (dragPanel.mouseOn)
            {
                var modPlayer = Main.LocalPlayer.GetModPlayer<WotTKPlayer>();
                text.SetText(LevelBarSystem.levelBarText.Format(modPlayer.playerLevel, modPlayer.playerLevelPoints, modPlayer.playerLevelPointsNeed));
            }
            else
            {
                text.SetText("");
            }
        }
        private void SetRectangle(UIElement uiElement, float left, float top, float width, float height)
        {
            uiElement.Left.Set(left, 0f);
            uiElement.Top.Set(top, 0f);
            uiElement.Width.Set(width, 0f);
            uiElement.Height.Set(height, 0f);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }
    }
}
