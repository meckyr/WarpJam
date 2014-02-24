using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WarpJam.Tools;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;

namespace WarpJam
{
    class MainLevel : GameScene
    {
        private Background backdrop;
        private Pesawat pesawat;
        private GameButton fire;
        private SpriteFonts text;
        private SoundEffect gunfire;
        private GameAnimatedSprite star1, star2, star3, star4, star5;
        private GameAnimatedSprite sux, meh, gr8, leet;

        private double delay = 3;

        public MainLevel()
            : base("MainLevel")
        {
        }

        public override void Initialize()
        {
            // set game speed
            SceneManager.RenderContext.GameSpeed = 100;

            backdrop = new Background();
            AddHUDObject(backdrop);

            text = new SpriteFonts("font\\font");
            text.Translate(520, 50);
            text.Color = Color.Red;
            AddHUDObject(text);

            InitiateStar();
            InitiateScore();

            fire = new GameButton("level\\invisbutton", false);
            fire.CanDraw = false;
            fire.Translate(400, 0);
            fire.OnEnter += () =>
            {
                if (delay <= 0)
                {
                    gunfire.Play();
                    sux.CanDraw = true;
                    sux.PlayAnimation(false);
                    pesawat.ShieldUp();
                }
            };
            AddHUDObject(fire);

            pesawat = new Pesawat();
            AddSceneObject(pesawat);

            base.Initialize();
        }

        public void InitiateScore()
        {
            sux = new GameAnimatedSprite("level\\sux", 5, 80, new Point(200, 70), 1);
            sux.Origin = new Vector2(100, 35);
            sux.Translate(500, 200);
            sux.CanDraw = false;
            AddHUDObject(sux);

            meh = new GameAnimatedSprite("level\\meh", 5, 80, new Point(200, 70), 1);
            meh.Origin = new Vector2(100, 35);
            meh.Translate(500, 200);
            meh.CanDraw = false;
            AddHUDObject(meh);

            gr8 = new GameAnimatedSprite("level\\gr8", 5, 80, new Point(200, 70), 1);
            gr8.Origin = new Vector2(100, 35);
            gr8.Translate(500, 200);
            gr8.CanDraw = false;
            AddHUDObject(gr8);

            leet = new GameAnimatedSprite("level\\leet", 5, 80, new Point(200, 70), 1);
            leet.Origin = new Vector2(100, 35);
            leet.Translate(500, 200);
            leet.CanDraw = false;
            AddHUDObject(leet);
        }

        public void InitiateStar()
        {
            star1 = new GameAnimatedSprite("menu\\star", 10, 10, new Point(55, 58));
            star1.Color = Color.Cyan;
            star1.Translate(500, 100);
            star1.PlayAnimation(true);
            AddSceneObject(star1);

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

            gunfire = contentmanager.Load<SoundEffect>("sfx\\gunfire");
        }

        

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            if (delay > 0)
                delay -= rendercontext.GameTime.ElapsedGameTime.TotalSeconds;

            if (delay <= 0)
            {
                MediaPlayer.Resume();
                pesawat.CurrentState = Pesawat.StatePesawat.Ready;
            }

            text.Text = "Time: " + MediaPlayer.PlayPosition.TotalSeconds;
            UpdateScore();

            base.Update(rendercontext, contentmanager);
        }

        public void UpdateScore()
        {
            if (!sux.IsPlaying)
                sux.CanDraw = false;
            if (!meh.IsPlaying)
                meh.CanDraw = false;
            if (!gr8.IsPlaying)
                gr8.CanDraw = false;
            if (!leet.IsPlaying)
                leet.CanDraw = false;
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);

            DrawPath(rendercontext);
            //farseer
            //for (int i = 0; i < ground.FixtureList.Count; ++i)
            //{
            //    rendercontext.LineBatch.DrawLineShape(ground.FixtureList[i].Shape, Color.Black);
            //}
        }

        public void DrawPath(RenderContext rendercontext)
        {
            rendercontext.SpriteBatch.DrawLine(new Vector2(600f, 180f), new Vector2(1650f, 180f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(600f, 300f), new Vector2(1650f, 300f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(1650f, 180f), new Vector2(2450f, 350f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(1650f, 300f), new Vector2(2450f, 470f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(2450f, 350f), new Vector2(3300f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(2450f, 470f), new Vector2(3300f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(3300f, 10f), new Vector2(4100f, 350f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(3300f, 130f), new Vector2(4100f, 470f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4100f, 350f), new Vector2(4150f, 150f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4100f, 470f), new Vector2(4200f, 470f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4150f, 150f), new Vector2(4300f, 150f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4200f, 470f), new Vector2(4300f, 250f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4300f, 150f), new Vector2(4400f, 200f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4300f, 250f), new Vector2(4400f, 320f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4400f, 200f), new Vector2(4600f, 200f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4400f, 320f), new Vector2(4600f, 320f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4600f, 200f), new Vector2(4800f, 270f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4600f, 320f), new Vector2(4800f, 390f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4800f, 270f), new Vector2(4950f, 270f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4800f, 390f), new Vector2(5050f, 390f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4950f, 270f), new Vector2(5050f, 50f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5050f, 390f), new Vector2(5150f, 170f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5050f, 50f), new Vector2(5400f, 50f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5150f, 170f), new Vector2(5300f, 170f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5400f, 50f), new Vector2(5500f, 270f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5300f, 170f), new Vector2(5400f, 390f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5500f, 270f), new Vector2(5650f, 270f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5400f, 390f), new Vector2(5650f, 390f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5650f, 270f), new Vector2(5700f, 300f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5650f, 390f), new Vector2(5700f, 420f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5700f, 300f), new Vector2(5850f, 300f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5700f, 420f), new Vector2(5900f, 420f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5850f, 300f), new Vector2(6000f, 50f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5900f, 420f), new Vector2(6100f, 170f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6000f, 50f), new Vector2(6200f, 50f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6100f, 170f), new Vector2(6200f, 170f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6200f, 50f), new Vector2(6600f, 300f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6200f, 170f), new Vector2(6600f, 420f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6600f, 300f), new Vector2(6600f, 200f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6600f, 420f), new Vector2(6720f, 420f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6600f, 200f), new Vector2(6800f, 200f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6720f, 420f), new Vector2(6720f, 320f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6800f, 200f), new Vector2(6800f, 100f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6720f, 320f), new Vector2(6920f, 320f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6800f, 100f), new Vector2(6900f, 100f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6920f, 320f), new Vector2(6920f, 220f), Color.White, 2);
        }

        public override void ResetScene()
        {
            delay = 3;
            pesawat.Reset();
            pesawat.CurrentState = Pesawat.StatePesawat.Normal;
        }
    }
}
