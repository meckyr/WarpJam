using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using WarpJam.Tools;

namespace WarpJam
{
    class MainMenu : GameScene 
    {
        private GameButton play;
        private GameSprite bg;
        private GameAnimatedSprite hero;
        private SpriteFont text;

        public MainMenu()
            : base("MainMenu")
        {
        }

        public override void Initialize()
        {
            bg = new GameSprite("menu\\background");
            AddSceneObject(bg);

            hero = new GameAnimatedSprite("menu\\hero", 8, 80, new Point(60, 52));
            hero.Translate(400, 200);
            hero.PlayAnimation(true);
            AddSceneObject(hero);
            CameraManager.getInstance().camera.Focus = hero;
            play = new GameButton("menu\\playbutton", true);
            play.Translate(200, 200);
            play.OnClick += () =>
            {
                SceneManager.push.Play();
                SceneManager.PlaySong(2);
                SceneManager.SetActiveScene("MainLevel");
                SceneManager.ActiveScene.ResetScene();
            };
            AddHUDObject(play);

            base.Initialize();
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
            play.BackToNormal();
        }
    }
}
