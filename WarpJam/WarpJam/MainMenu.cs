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
        private GameButton play, setting, score;
        private GameSprite bg;
        private GameAnimatedSprite star1, star2, star3, star4, star5;
        private SpriteFont text;

        // Pengecekan State
        private bool isOnMenu = true;

        public MainMenu()
            : base("MainMenu")
        {
        }

        public override void Initialize()
        {
            bg = new GameSprite("menu\\background");
            AddSceneObject(bg);

            star1 = new GameAnimatedSprite("menu\\star", 10, 50, new Point(55, 58));
            star1.Translate(400, 240);
            star1.PlayAnimation(true);
            AddSceneObject(star1);
            CameraManager.getInstance().camera.Focus = star1;

            InitiateStar();

            play = new GameButton("menu\\playbutton", true);
            play.Translate(100, 100);
            play.OnClick += () =>
            {
                SceneManager.push.Play();
                SceneManager.PlaySong(2);
                SceneManager.SetActiveScene("MainLevel");
                SceneManager.ActiveScene.ResetScene();
            };
            AddSceneObject(play);

            setting = new GameButton("menu\\settingbutton", true);
            setting.Translate(100, 220);
            setting.OnClick += () =>
            {
                SceneManager.push.Play();
                isOnMenu = false;
                star1.Translate(1200, 240);
            };
            AddSceneObject(setting);

            score = new GameButton("menu\\scorebutton", true);
            score.Translate(100, 340);
            score.OnClick += () =>
            {
                SceneManager.push.Play();
                isOnMenu = false;
                star1.Translate(400, 720);
            };
            AddSceneObject(score);

            base.Initialize();
        }

        public void InitiateStar()
        {
            star2 = new GameAnimatedSprite("menu\\star", 10, 105, new Point(55, 58));
            star2.Color = Color.Cyan;
            star2.Translate(720, 50);
            star2.PlayAnimation(true);
            AddSceneObject(star2);

            star3 = new GameAnimatedSprite("menu\\star", 10, 80, new Point(55, 58));
            star3.Color = Color.Cyan;
            star3.Translate(10, 340);
            star3.PlayAnimation(true);
            AddSceneObject(star3);

            star4 = new GameAnimatedSprite("menu\\star", 10, 65, new Point(55, 58));
            star4.Color = Color.Cyan;
            star4.Scale(2.0f, 2.0f);
            star4.Translate(400, 400);
            star4.PlayAnimation(true);
            AddSceneObject(star4);

            star5 = new GameAnimatedSprite("menu\\star", 10, 40, new Point(55, 58));
            star5.Color = Color.Cyan;
            star5.Scale(0.5f, 0.5f);
            star5.Translate(500, 90);
            star5.PlayAnimation(true);
            AddSceneObject(star5);
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
        }

        public override void ResetScene()
        {
            play.BackToNormal();
            CameraManager.getInstance().camera.Focus = star1;
        }

        public override bool BackPressed()
        {
            if (isOnMenu)
                return true;
            else
            {
                isOnMenu = true;
                star1.Translate(400, 240);
                return false;
            }
        }
    }
}
