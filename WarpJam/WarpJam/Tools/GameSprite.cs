using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WarpJam.Tools
{
    class GameSprite : GameObject2D
    {
        private readonly string assetFile;
        private Texture2D texture;

        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }

        public float Depth { get; set; }
        public Vector2 Origin { get; set; }
        public Color Color { get; set; }
        public SpriteEffects Effect { get; set; }
        public Rectangle? DrawRect { get; set; }

        public GameSprite(string assetfile)
        {
            assetFile = assetfile;
            Color = Color.White;
            Effect = SpriteEffects.None;
            Origin = Vector2.Zero;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
            texture = contentManager.Load<Texture2D>(assetFile);
        }

        public override void Draw(RenderContext renderContext)
        {
            if (CanDraw)
            {
                renderContext.SpriteBatch.Draw(
                    texture, WorldPosition, DrawRect, Color, MathHelper.ToRadians(WorldRotation),
                    Origin, WorldScale, Effect, Depth
                    );
                base.Draw(renderContext);
            }
        }
    }
}
