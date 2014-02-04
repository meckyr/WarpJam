using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WarpJam.Tools;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WarpJam
{
    class MainLevel : GameScene 
    {
        private GameSprite bg;
        private GameAnimatedSprite hero;
        private SpriteFont text;

        public MainLevel()
            : base("MainLevel")
        {
            bg = new GameSprite("menu\\background");
            AddSceneObject(bg);

            hero = new GameAnimatedSprite("menu\\hero", 8, 80, new Point(60,52));
            hero.Translate(400, 50);
            hero.PlayAnimation(true);
            AddSceneObject(hero);
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);

            text = contentmanager.Load<SpriteFont>("font\\font");
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);

            rendercontext.SpriteBatch.DrawString(text, "Ngetes deh...", new Vector2(470, 85), Color.Black);
        }

        public override void ResetScene()
        {
        }
    }
}
