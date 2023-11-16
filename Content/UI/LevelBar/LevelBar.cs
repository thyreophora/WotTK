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
using WotTK.Common.Players;

namespace WotTK.Content.UI.LevelBar
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
        private LevelBarPart[] barPart = new LevelBarPart[242];
        private UIText text;
        private UIImage iconLevel;
        private UIText text2;
        private UIImage iconXP;
        private UIText text3;
        public override void OnInitialize()
        {
            string way = "WotTK/Content/UI/LevelBar/";

            dragPanel = new DraggableUIPanel();
            dragPanel.SetPadding(0);
            SetRectangle(dragPanel, left: WotTKConfig.Instance.LevelBarX * Main.screenWidth, top: WotTKConfig.Instance.LevelBarY * Main.screenHeight, width: 512f, height: 20f);
            //SetRectangle(dragPanel, left: 400, top: 100, width: 208f, height: 16f);
            //CoinCounterPanel.BackgroundColor = new Color(73, 94, 171);

            barFrame = new UIImage(ModContent.Request<Texture2D>(way + "LevelBarFrame"));
            SetRectangle(barFrame, 0, 0, 512, 12);
            dragPanel.Append(barFrame);

            for (int i = 0; i < 242; i++)
            {
                barPart[i] = new LevelBarPart(ModContent.Request<Texture2D>(way + "LevelBarPart" + (i == 0 || i == 245 ? "Edge" : "")), 1f / 242 * (i + 2));
                SetRectangle(barPart[i], i * 2 + 12, 8, 2, 0);
                dragPanel.Append(barPart[i]);
                //barPart[i].
            }

            iconLevel = new UIImage(ModContent.Request<Texture2D>(way + "LevelBarIconLevel"));
            SetRectangle(iconLevel, 0, 20f, 32, 12);
            text2 = new UIText("", 0.8f);
            SetRectangle(text2, 35, 0, 1, 1);
            iconLevel.Append(text2);
            dragPanel.Append(iconLevel);

            iconXP = new UIImage(ModContent.Request<Texture2D>(way + "LevelBarIconPoints"));
            SetRectangle(iconXP, 410, 20f, 22, 12);
            text3 = new UIText("", 0.8f);
            SetRectangle(text3, 25, 0, 1, 1);
            iconXP.Append(text3);
            dragPanel.Append(iconXP);


            text = new UIText("Level: 0\nPoints: 0/0", 0.8f);
            SetRectangle(text, 0, -20, 208, 32);
            //dragPanel.Append(text); //Can be removed to remove text from UI

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

            var modPlayer = Main.LocalPlayer.GetModPlayer<WotTKPlayer>();
            text2.SetText(modPlayer.playerLevel.ToString());
            text3.SetText(modPlayer.playerLevelPoints.ToString() + "/" + modPlayer.playerLevelPointsNeed.ToString());

            if (dragPanel.mouseOn)
            {
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
