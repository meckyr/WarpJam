using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WarpJam.Tools
{
    class SpriteFonts : GameObject2D
    {
        private readonly string assetFile;
        private SpriteFont texture;

        public string Text { get; set; }
        public float Depth { get; set; }
        public Vector2 Origin { get; set; }
        public Color Color { get; set; }
        public SpriteEffects Effect { get; set; }

        public SpriteFonts(string assetfile)
        {
            assetFile = assetfile;
            Text = "";
            Color = Color.White;
            Effect = SpriteEffects.None;
            Origin = Vector2.Zero;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
            texture = contentManager.Load<SpriteFont>(assetFile);
        }

        public override void Draw(RenderContext renderContext)
        {
            if (CanDraw)
            {
                renderContext.SpriteBatch.DrawString(texture, Text, WorldPosition, Color, MathHelper.ToRadians(WorldRotation), 
                    Origin, WorldScale, Effect, Depth);
                base.Draw(renderContext);
            }
        }
    }
}
