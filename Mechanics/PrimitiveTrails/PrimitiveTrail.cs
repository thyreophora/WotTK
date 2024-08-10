using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.UI;

namespace WotTK.Mechanics.PrimitiveTrails
{
    public class PrimitiveTrail
    {
        private List<Vector2> points;
        private VertexPositionColorTexture[] vertices;
        private short[] indices;
        private Texture2D texture;

        public PrimitiveTrail(Texture2D texture)
        {
            points = new List<Vector2>();
            this.texture = texture;
        }

        public void AddPoint(Vector2 point)
        {
            points.Add(point);
            if (points.Count > 50) // Limit the number of points to avoid performance issues
            {
                points.RemoveAt(0);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (points.Count < 2)
                return;

            vertices = new VertexPositionColorTexture[points.Count * 2];
            indices = new short[(points.Count - 1) * 6];

            for (int i = 0; i < points.Count; i++)
            {
                float alpha = 1f - (i / (float)points.Count);
                Color color = new Color(255, 255, 255, (int)(alpha * 255));

                vertices[i * 2] = new VertexPositionColorTexture(new Vector3(points[i], 0f), color, new Vector2(0f, i / (float)points.Count));
                vertices[i * 2 + 1] = new VertexPositionColorTexture(new Vector3(points[i] + new Vector2(0f, 32f), 0f), color, new Vector2(1f, i / (float)points.Count));
            }

            for (int i = 0; i < points.Count - 1; i++)
            {
                indices[i * 6] = (short)(i * 2);
                indices[i * 6 + 1] = (short)(i * 2 + 1);
                indices[i * 6 + 2] = (short)(i * 2 + 2);

                indices[i * 6 + 3] = (short)(i * 2 + 1);
                indices[i * 6 + 4] = (short)(i * 2 + 3);
                indices[i * 6 + 5] = (short)(i * 2 + 2);
            }

            spriteBatch.End();
            Main.graphics.GraphicsDevice.Textures[0] = texture;
            Main.graphics.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
            spriteBatch.Begin();
        }
    }
}