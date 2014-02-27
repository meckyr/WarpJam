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
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics;
using FarseerPhysics.Dynamics.Contacts;

namespace WarpJam
{
    class MainMenu : GameScene 
    {
        private GameButton play, setting, score;
        private GameSprite bg;
        private GameAnimatedSprite star1, star2, star3, star4, star5, setting_a, score_a, play_a, ship1, ship2;
        private SpriteFont text;

        // pesawat
        private const double ShipAnimationDelay = 3;
        private double delay = ShipAnimationDelay;
        private int acceleration = 1;

        // Pengecekan State
        private bool isOnMenu = true;

        public MainMenu()
            : base("MainMenu")
        {
        }

        public override void Initialize()
        {
            CameraManager.getInstance().camera.Position = new Vector2(400, 240);

            bg = new GameSprite("menu\\background");
            AddSceneObject(bg);

            star1 = new GameAnimatedSprite("menu\\star", 10, 50, new Point(55, 58));
            star1.Translate(400, 240);
            star1.PlayAnimation(true);
            AddSceneObject(star1);

            InitiateStar();

            play = new GameButton("menu\\playbutton", true);
            play.Translate(120, 100);
            play.CanDraw = false;
            play.OnClick += () =>
            {
                SceneManager.push.Play();
                SceneManager.PlaySong(2);
                MediaPlayer.Pause();
                SceneManager.SetActiveScene("MainLevel");
                SceneManager.ActiveScene.ResetScene();

                play_a.CanDraw = true;
                play.CanDraw = false;
            };
            play.OnEnter += () =>
            {
                play_a.CanDraw = false;
                play.CanDraw = true;
            };
            play.OnLeave += () =>
            {
                play_a.CanDraw = true;
                play.CanDraw = false;
            };
            AddSceneObject(play);

            play_a = new GameAnimatedSprite("menu\\playbuttonanimated", 4, 150, new Point(250, 100), 1);
            play_a.Translate(120, 100);
            play_a.PlayAnimation(true);
            AddSceneObject(play_a);

            setting = new GameButton("menu\\settingbutton", true);
            setting.Translate(120, 250);
            setting.CanDraw = false;
            setting.OnClick += () =>
            {
                SceneManager.whoosh.Play();
                isOnMenu = false;
                star1.Translate(1200, 240);

                setting_a.CanDraw = true;
                setting.CanDraw = false;
            };
            setting.OnEnter += () =>
            {
                setting_a.CanDraw = false;
                setting.CanDraw = true;
            };
            setting.OnLeave += () =>
            {
                setting_a.CanDraw = true;
                setting.CanDraw = false;
            };
            AddSceneObject(setting);

            setting_a = new GameAnimatedSprite("menu\\settingbuttonanimated", 4, 150, new Point(250, 100), 1);
            setting_a.Translate(120, 250);
            setting_a.PlayAnimation(true);
            AddSceneObject(setting_a);

            score = new GameButton("menu\\scorebutton", true);
            score.Translate(120, 340);
            score.CanDraw = false;
            score.OnClick += () =>
            {
                SceneManager.whoosh.Play();
                isOnMenu = false;
                star1.Translate(400, 720);

                score_a.CanDraw = true;
                score.CanDraw = false;
            };
            score.OnEnter += () =>
            {
                score_a.CanDraw = false;
                score.CanDraw = true;
            };
            score.OnLeave += () =>
            {
                score_a.CanDraw = true;
                score.CanDraw = false;
            };
            AddSceneObject(score);

            score_a = new GameAnimatedSprite("menu\\scorebuttonanimated", 4, 150, new Point(250, 100), 1);
            score_a.Translate(120, 340);
            score_a.PlayAnimation(true);
            AddSceneObject(score_a);

            ship1 = new GameAnimatedSprite("menu\\ship1", 4, 180, new Point(300, 300), 1);
            ship1.Translate(450, 100);
            ship1.PlayAnimation(false);
            ship1.CanDraw = false;
            AddSceneObject(ship1);

            ship2 = new GameAnimatedSprite("menu\\ship2", 4, 80, new Point(300, 300), 1);
            ship2.Translate(450, 100);
            ship2.PlayAnimation(false);
            AddSceneObject(ship2);

            base.Initialize();
        }

        public void AnimateShip(RenderContext rendercontext)
        {
            if (delay > 0 && !ship1.IsPlaying && !ship2.IsPlaying)
                delay -= rendercontext.GameTime.ElapsedGameTime.TotalSeconds;

            if (delay <= 0)
            {
                if (ship1.CanDraw && !ship1.IsPlaying)
                {
                    ship1.CanDraw = false;
                    ship2.CanDraw = true;
                    ship2.PlayAnimation(false);
                }
                else if (ship2.CanDraw && !ship2.IsPlaying)
                {
                    ship1.CanDraw = true;
                    ship2.CanDraw = false;
                    ship1.PlayAnimation(false);
                }

                delay = ShipAnimationDelay;
            }

            var newPos = ship1.LocalPosition.Y + (0.5f * acceleration);

            if (newPos >= 120)
                acceleration = -1;

            if (newPos <= 80)
                acceleration = 1;

            ship1.Translate(ship1.LocalPosition.X, newPos);
            ship2.Translate(ship2.LocalPosition.X, newPos);
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
            AnimateShip(rendercontext);

            base.Update(rendercontext, contentmanager);
        }

        bool rectangle_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            SceneManager.Vibrator.Start(TimeSpan.FromMilliseconds(100));
            return true;
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);

            rendercontext.SpriteBatch.DrawString(text, "Music", new Vector2(1000, 100), Color.White);
            rendercontext.SpriteBatch.DrawString(text, "SFX", new Vector2(1000, 300), Color.White);
        }

        public override void ResetScene()
        {
            play.BackToNormal();
            CameraManager.getInstance().camera.MoveSpeed = 10f;
            CameraManager.getInstance().camera.Focus = star1;
            CameraManager.getInstance().camera.IsIgnoreY = false;
            CameraManager.getInstance().camera.ResetScreenCenter();
        }

        public override bool BackPressed()
        {
            if (isOnMenu)
            {
                SceneManager.push.Play();
                return true;
            }
            else
            {
                isOnMenu = true;
                star1.Translate(400, 240);
                SceneManager.whoosh.Play();
                return false;
            }
        }
    }
}
