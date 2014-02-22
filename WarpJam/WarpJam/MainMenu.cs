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
        private GameAnimatedSprite star1, star2, star3, star4, star5;
        private SpriteFont text;

        // farseer
        World world;
        Body rectangle;
        Body rectangle2;
        Body rectangle3;
        Texture2D rectangleSprite;

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
            
            // farseer
            if (world == null)
                world = new World(new Vector2(0, 10f));
            else
                world.Clear();

            rectangle = BodyFactory.CreateRectangle(world, 1f, 1f, 1.0f);
            rectangle.BodyType = BodyType.Dynamic;
            rectangle.Position = new Vector2(3.5f, 0);

            rectangle.OnCollision += new OnCollisionEventHandler(rectangle_OnCollision);

            rectangle2 = BodyFactory.CreateRectangle(world, 1f, 1f, 1.0f);
            rectangle2.BodyType = BodyType.Static;
            rectangle2.Position = new Vector2(2.7f, 2);

            rectangle3 = BodyFactory.CreateRectangle(world, 1f, 1f, 1.0f);
            rectangle3.BodyType = BodyType.Static;
            rectangle3.Position = new Vector2(5.0f, 3);

            rectangleSprite = contentmanager.Load<Texture2D>("kotak");
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            // farseer
            world.Step(Math.Min((float)rendercontext.GameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));

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

            rendercontext.SpriteBatch.Draw(rectangleSprite, ConvertUnits.ToDisplayUnits(rectangle.Position), null, Color.White,
                rectangle.Rotation, new Vector2(rectangleSprite.Width / 2f, rectangleSprite.Height / 2f), 1f, SpriteEffects.None, 0f);
            rendercontext.SpriteBatch.Draw(rectangleSprite, ConvertUnits.ToDisplayUnits(rectangle2.Position), null, Color.White,
                rectangle2.Rotation, new Vector2(rectangleSprite.Width / 2f, rectangleSprite.Height / 2f), 1f, SpriteEffects.None, 0f);
            rendercontext.SpriteBatch.Draw(rectangleSprite, ConvertUnits.ToDisplayUnits(rectangle3.Position), null, Color.White,
                rectangle3.Rotation, new Vector2(rectangleSprite.Width / 2f, rectangleSprite.Height / 2f), 1f, SpriteEffects.None, 0f);
        }

        public override void ResetScene()
        {
            play.BackToNormal();
            CameraManager.getInstance().camera.Focus = star1;
            CameraManager.getInstance().camera.IsIgnoreY = false;
            CameraManager.getInstance().camera.ResetScreenCenter();
            rectangle.Position = new Vector2(3.5f, 0);
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
