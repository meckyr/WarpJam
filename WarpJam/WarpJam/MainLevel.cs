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

        private const double Delay = 3;
        private double delay = Delay;

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
                    SpawnScore();
                    pesawat.ShieldUp();
                }
            };
            AddHUDObject(fire);

            pesawat = new Pesawat();
            AddSceneObject(pesawat);

            base.Initialize();
        }

        public void SpawnScore()
        {
            var oldPos = leet.LocalPosition;
            leet.Translate(pesawat.Sprite.LocalPosition - new Vector2(-120, 80));
            var newPos = leet.LocalPosition;

            if (oldPos != newPos)
            {
                leet.CanDraw = true;
                leet.PlayAnimation(false);
            }
        }

        public void InitiateScore()
        {
            sux = new GameAnimatedSprite("level\\sux", 5, 40, new Point(200, 70), 1);
            sux.Origin = new Vector2(100, 35);
            sux.Translate(500, 200);
            sux.CanDraw = false;
            AddSceneObject(sux);

            meh = new GameAnimatedSprite("level\\meh", 5, 40, new Point(200, 70), 1);
            meh.Origin = new Vector2(100, 35);
            meh.Translate(500, 200);
            meh.CanDraw = false;
            AddSceneObject(meh);

            gr8 = new GameAnimatedSprite("level\\gr8", 5, 40, new Point(200, 70), 1);
            gr8.Origin = new Vector2(100, 35);
            gr8.Translate(500, 200);
            gr8.CanDraw = false;
            AddSceneObject(gr8);

            leet = new GameAnimatedSprite("level\\leet", 5, 40, new Point(200, 70), 1);
            leet.Origin = new Vector2(100, 35);
            leet.Translate(500, 200);
            leet.CanDraw = false;
            AddSceneObject(leet);
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

            sux.Translate(pesawat.Sprite.LocalPosition - new Vector2(-120, 80));
            meh.Translate(pesawat.Sprite.LocalPosition - new Vector2(-120, 80));
            gr8.Translate(pesawat.Sprite.LocalPosition - new Vector2(-120, 80));
            leet.Translate(pesawat.Sprite.LocalPosition - new Vector2(-120, 80));
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);

            DrawPath(rendercontext);
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

            rendercontext.SpriteBatch.DrawLine(new Vector2(6800f, 100f), new Vector2(7000f, 100f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6920f, 320f), new Vector2(6920f, 220f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7000f, 100f), new Vector2(7000f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6920f, 220f), new Vector2(7100f, 220f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7000f, 10f), new Vector2(7200f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7100f, 220f), new Vector2(7100f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7200f, 10f), new Vector2(7300f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7100f, 130f), new Vector2(7200f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7300f, 10f), new Vector2(7400f, 350f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7200f, 130f), new Vector2(7300f, 470f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7400f, 350f), new Vector2(7500f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7300f, 470f), new Vector2(7500f, 470f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7500f, 10f), new Vector2(7800f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7500f, 470f), new Vector2(7600f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7800f, 10f), new Vector2(7900f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7600f, 130f), new Vector2(7800f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7900f, 10f), new Vector2(8000f, 350f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7800f, 130f), new Vector2(7900f, 470f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8000f, 350f), new Vector2(8250f, 350f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7900f, 470f), new Vector2(8350f, 470f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8250f, 350f), new Vector2(8350f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(8350f, 470f), new Vector2(8450f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8350f, 10f), new Vector2(8750f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(8450f, 130f), new Vector2(8650f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8750f, 10f), new Vector2(8850f, 350f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(8650f, 130f), new Vector2(8750f, 470f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8850f, 350f), new Vector2(9100f, 350f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(8750f, 470f), new Vector2(9100f, 470f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(9100f, 350f), new Vector2(9500f, 150f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(9100f, 470f), new Vector2(9500f, 270f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(9500f, 150f), new Vector2(9950f, 300f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(9500f, 270f), new Vector2(9950f, 420f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(9950f, 300f), new Vector2(10350f, 100f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(9950f, 420f), new Vector2(10350f, 220f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10350f, 100f), new Vector2(10650f, 220f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10350f, 220f), new Vector2(10650f, 340f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10650f, 220f), new Vector2(10750f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10650f, 340f), new Vector2(10750f, 340f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10750f, 10f), new Vector2(10990f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10750f, 340f), new Vector2(10870f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10990f, 10f), new Vector2(10990f, 200f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10870f, 130f), new Vector2(10870f, 320f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10990f, 200f), new Vector2(11100f, 200f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10870f, 320f), new Vector2(11220f, 320f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11100f, 200f), new Vector2(11100f, 80f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11220f, 320f), new Vector2(11220f, 200f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11100f, 80f), new Vector2(11350f, 80f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11220f, 200f), new Vector2(11470f, 200f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11350f, 80f), new Vector2(11350f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11470f, 200f), new Vector2(11470f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11350f, 10f), new Vector2(11620f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11470f, 130f), new Vector2(11500f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11620f, 10f), new Vector2(11620f, 200f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11500f, 130f), new Vector2(11500f, 320f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11620f, 200f), new Vector2(11680f, 200f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11500f, 320f), new Vector2(11800f, 320f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11680f, 200f), new Vector2(11680f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11800f, 320f), new Vector2(11800f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11680f, 10f), new Vector2(12000f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11800f, 130f), new Vector2(11880f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12000f, 10f), new Vector2(12100f, 250f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11880f, 130f), new Vector2(11980f, 370f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12100f, 250f), new Vector2(12100f, 200f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11980f, 370f), new Vector2(12150f, 370f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12100f, 200f), new Vector2(12250f, 200f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12150f, 370f), new Vector2(12150f, 320f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12250f, 200f), new Vector2(12250f, 100f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12150f, 320f), new Vector2(12370f, 320f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12250f, 100f), new Vector2(12370f, 100f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12370f, 320f), new Vector2(12370f, 220f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12370f, 100f), new Vector2(12500f, 10f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12370f, 220f), new Vector2(12500f, 130f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12500f, 10f), new Vector2(12700f, 180f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12500f, 130f), new Vector2(12700f, 300f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12700f, 180f), new Vector2(12870f, 180f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12700f, 300f), new Vector2(12750f, 300f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12870f, 180f), new Vector2(12870f, 300f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12750f, 300f), new Vector2(12750f, 420f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12870f, 300f), new Vector2(13090f, 300f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12750f, 420f), new Vector2(12970f, 420f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12870f, 300f), new Vector2(13090f, 300f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12750f, 420f), new Vector2(12970f, 420f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13090f, 300f), new Vector2(13150f, 350f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12970f, 420f), new Vector2(13030f, 470f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13150f, 350f), new Vector2(13150f, 280f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13030f, 470f), new Vector2(13270f, 470f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13150f, 280f), new Vector2(13200f, 280f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13270f, 470f), new Vector2(13270f, 400f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13200f, 280f), new Vector2(13200f, 230f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13270f, 400f), new Vector2(13320f, 400f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13200f, 230f), new Vector2(13300f, 230f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13320f, 400f), new Vector2(13320f, 350f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13300f, 230f), new Vector2(13300f, 180f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13320f, 350f), new Vector2(13420f, 350f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13300f, 180f), new Vector2(13350f, 180f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13420f, 350f), new Vector2(13420f, 300f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13350f, 180f), new Vector2(13350f, 130f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13420f, 300f), new Vector2(13470f, 300f), Color.White, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13350f, 130f), new Vector2(13400f, 130f), Color.White, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13470f, 300f), new Vector2(13470f, 250f), Color.White, 2);
        }

        public override void ResetScene()
        {
            delay = Delay;
            pesawat.Reset();
            pesawat.CurrentState = Pesawat.StatePesawat.Normal;
        }
    }
}
