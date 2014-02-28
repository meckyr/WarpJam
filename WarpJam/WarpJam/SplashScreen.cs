using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarpJam.Tools;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WarpJam
{
    class SplashScreen : GameScene
    {
        private GameSprite bg;

        private int alphaValue = 255;
        private int fadeIncrement = 5;

        private double fadeDelay = 1;

        public SplashScreen()
            : base("SplashScreen")
        {
        }

        public override void Initialize()
        {
            bg = new GameSprite("splash\\background");
            AddSceneObject(bg);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            if (fadeDelay > 0)
                fadeDelay -= rendercontext.GameTime.ElapsedGameTime.TotalSeconds;

            if (fadeDelay <= 0)
            {
                alphaValue -= fadeIncrement;
                bg.Color = new Color(255, 255, 255, (byte)MathHelper.Clamp(alphaValue, 0, 255));
            }

            if (alphaValue <= 0)
            {
                SceneManager.SetActiveScene("TitleScreen");
                SceneManager.ActiveScene.ResetScene();
                SceneManager.PlaySong(1);
            }

            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);
        }

        public override void ResetScene()
        {
        }
    }
}
