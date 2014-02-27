using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarpJam.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

namespace WarpJam
{
    class TitleScreen : GameScene 
    {
        private GameSprite bg;
        private GameAnimatedSprite logo, text, star1, star2, star3, star4, star5, star6, star7, star8;

        private const double Delay = 2.5f;
        private double delay = Delay;

        public TitleScreen()
            : base("TitleScreen")
        {
        }

        public void InitiateStar()
        {
            star1 = new GameAnimatedSprite("menu\\star", 10, 50, new Point(55, 58));
            star1.Color = Color.Cyan;
            star1.Translate(500, 100);
            star1.PlayAnimation(true);
            AddSceneObject(star1);

            star2 = new GameAnimatedSprite("menu\\star", 10, 105, new Point(55, 58));
            star2.Color = Color.Purple;
            star2.Translate(720, 50);
            star2.PlayAnimation(true);
            AddSceneObject(star2);

            star3 = new GameAnimatedSprite("menu\\star", 10, 80, new Point(55, 58));
            star3.Color = Color.Purple;
            star3.Translate(10, 340);
            star3.PlayAnimation(true);
            AddSceneObject(star3);

            star4 = new GameAnimatedSprite("menu\\star", 10, 65, new Point(55, 58));
            star4.Color = Color.Cyan;
            star4.Translate(600, 400);
            star4.PlayAnimation(true);
            AddSceneObject(star4);

            star5 = new GameAnimatedSprite("menu\\star", 10, 40, new Point(55, 58));
            star5.Color = Color.Purple;
            star5.Scale(0.5f, 0.5f);
            star5.Translate(500, 90);
            star5.PlayAnimation(true);
            AddSceneObject(star5);

            star6 = new GameAnimatedSprite("menu\\star", 10, 70, new Point(55, 58));
            star6.Color = Color.Aqua;
            star6.Scale(1.2f, 1.2f);
            star6.Translate(180, 70);
            star6.PlayAnimation(true);
            AddSceneObject(star6);

            star7 = new GameAnimatedSprite("menu\\bintang", 8, 40, new Point(121, 26), 1);
            star7.Color = Color.Cyan;
            star7.Translate(100, 70);
            star7.Rotate(45);
            star7.PlayAnimation(true);
            AddSceneObject(star7);

            star8 = new GameAnimatedSprite("menu\\bintang", 8, 80, new Point(121, 26), 1);
            star8.Color = Color.Cyan;
            star8.Translate(700, 100);
            star8.Rotate(135);
            star8.PlayAnimation(true);
            AddSceneObject(star8);
        }

        public override void Initialize()
        {
            bg = new GameSprite("title\\background");
            AddSceneObject(bg);

            InitiateStar();

            logo = new GameAnimatedSprite("title\\logo", 4, 120, new Point(650, 250), 2);
            logo.PlayAnimation(false);
            logo.Translate(75, 100);
            AddSceneObject(logo);

            text = new GameAnimatedSprite("title\\text", 4, 100, new Point(250, 100), 1);
            text.PlayAnimation(true);
            text.Translate(290, 350);
            AddSceneObject(text);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            if ((delay > 0) && (!logo.IsPlaying))
                delay -= rendercontext.GameTime.ElapsedGameTime.TotalSeconds;

            if (delay <= 0)
            {
                logo.PlayAnimation(false);
                delay = Delay;
            }

            if (rendercontext.TouchPanelState.Count > 0 && rendercontext.TouchPanelState[0].State == TouchLocationState.Released)
            {
                SceneManager.push.Play();
                SceneManager.SetActiveScene("MainMenu");
                SceneManager.ActiveScene.ResetScene();
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

        public override bool BackPressed()
        {
            SceneManager.push.Play();
            return true;
        }
    }
}
