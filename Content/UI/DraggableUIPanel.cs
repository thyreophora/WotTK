using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria;

namespace WotTK.Content.UI
{
    public class DraggableUIPanel : UIPanel
    {
        public Vector2 offset;
        public bool dragging;
        public bool mouseOn;
        /*public void SetConfigValue(ref float refX, ref float refY)
        {
            if (dragging)
            {
                refX = (float)Main.mouseX / Main.screenWidth;
                refY = (float)Main.mouseY / Main.screenHeight;
                //Recalculate();
            }
        }*/
        public virtual void UpdateConfigs(float posX, float posY)
        {

        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            DragStart(evt);
        }

        public override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);
            DragEnd(evt);
        }

        private void DragStart(UIMouseEvent evt)
        {
            offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            dragging = true;
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);
            mouseOn = true;
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            base.MouseOut(evt);
            mouseOn = false;
        }

        private void DragEnd(UIMouseEvent evt)
        {
            Vector2 endMousePosition = evt.MousePosition;
            dragging = false;

            Left.Set(endMousePosition.X - offset.X, 0f);
            Top.Set(endMousePosition.Y - offset.Y, 0f);

            Recalculate();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (dragging)
            {
                Left.Set(Main.mouseX - offset.X, 0f); // Main.MouseScreen.X and Main.mouseX are the same
                Top.Set(Main.mouseY - offset.Y, 0f);
                Recalculate();

                UpdateConfigs((float)Left.Pixels / Main.screenWidth, (float)Top.Pixels / Main.screenHeight);
                WotTK.SaveConfig(WotTKConfig.Instance);
            }

            var parentSpace = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parentSpace))
            {
                Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
                Recalculate();
            }

        }
    }
}
