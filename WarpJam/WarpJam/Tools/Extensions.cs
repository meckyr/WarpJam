using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WarpJam.Tools
{
    static class Extensions
    {
        private static Texture2D pixel;

        public static Rectangle Update(this Rectangle rectangle, Matrix transform)
        {
            var corners = new Vector2[] 
            {
                new Vector2(rectangle.Left, rectangle.Top),
                new Vector2(rectangle.Right, rectangle.Bottom),
                new Vector2(rectangle.Left, rectangle.Bottom),
                new Vector2(rectangle.Right, rectangle.Top)
            };

            var transformedCorners = new Vector2[corners.Length];

            Vector2.Transform(corners, ref transform, transformedCorners);

            var newMin = new Vector3(float.MaxValue);
            var newMax = new Vector3(float.MinValue);

            foreach (var corner in transformedCorners)
            {
                newMin.X = Math.Min(newMin.X, corner.X);
                newMin.Y = Math.Min(newMin.Y, corner.Y);

                newMax.X = Math.Max(newMax.X, corner.X);
                newMax.Y = Math.Max(newMax.Y, corner.Y);
            }

            int width = (int)(newMax.X - newMin.X);
            int height = (int)(newMax.Y - newMin.Y);

            return new Rectangle((int)newMin.X, (int)newMin.Y, width, height);
        }

        public static void LoadContent(ContentManager contentManager)
        {
            pixel = contentManager.Load<Texture2D>("WhitePixel");
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)Math.Atan2((point2.Y - point1.Y), (point2.X - point1.X));

            spriteBatch.Draw(pixel, point1, null, color, angle, Vector2.Zero, new Vector2(distance, 1), SpriteEffects.None, 1.0f);
        }

        public static void Draw(this Rectangle rectangle, RenderContext renderContext, Color color)
        {
            renderContext.SpriteBatch.DrawLine(new Vector2(rectangle.Left, rectangle.Top), new Vector2(rectangle.Right, rectangle.Top), color);
            renderContext.SpriteBatch.DrawLine(new Vector2(rectangle.Left, rectangle.Top), new Vector2(rectangle.Left, rectangle.Bottom), color);
            renderContext.SpriteBatch.DrawLine(new Vector2(rectangle.Left, rectangle.Bottom), new Vector2(rectangle.Right, rectangle.Bottom), color);
            renderContext.SpriteBatch.DrawLine(new Vector2(rectangle.Right, rectangle.Bottom), new Vector2(rectangle.Right, rectangle.Top), color);
        }
    }
}
