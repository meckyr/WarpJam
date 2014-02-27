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
        private List<Obstacle> obstacle;
        private GameButton fire;
        private SpriteFonts text;
        private GameAnimatedSprite sux, meh, gr8, leet;

        private const double Delay = 3;
        private double delay = Delay;
        private Color lineColor = Color.Purple;
        private bool isFinished = false;

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

            obstacle = new List<Obstacle>();

            text = new SpriteFonts("font\\font");
            text.Translate(600, 50);
            AddHUDObject(text);

            InitiateScore();

            fire = new GameButton("level\\invisbutton", false);
            fire.CanDraw = false;
            fire.Translate(400, 0);
            fire.OnEnter += () =>
            {
                if (pesawat.CurrentState == Pesawat.StatePesawat.Ready)
                {
                    SceneManager.Vibrator.Start(TimeSpan.FromMilliseconds(100));
                    pesawat.ShieldUp();
                    SpawnScore(ObstacleCheck());
                }
            };
            AddHUDObject(fire);

            InitiateEnemy();
            

            pesawat = new Pesawat();
            AddSceneObject(pesawat);

            MediaPlayer.MediaStateChanged += new EventHandler<EventArgs>(MediaPlayer_MediaStateChanged);

            base.Initialize();
        }

        public int ObstacleCheck()
        {
            int num = 0;

            foreach (Obstacle ob in obstacle)
            {
                num = ob.CheckCollision();

                if (num != 0)
                    break;
            }

            return num;
        }

        public void InitiateEnemy()
        {
            Obstacle obstacle1 = new Obstacle(new Vector2(2425, 400), TimeSpan.FromSeconds(6.6));
            obstacle.Add(obstacle1);
            AddSceneObject(obstacle1);

            Obstacle obstacle2 = new Obstacle(new Vector2(2600, 400), TimeSpan.FromSeconds(10));
            obstacle.Add(obstacle2);
            AddSceneObject(obstacle2);

            Obstacle obstacle3 = new Obstacle(new Vector2(3000, 400), TimeSpan.FromSeconds(14));
            obstacle.Add(obstacle3);
            AddSceneObject(obstacle3);

            Obstacle obstacle4 = new Obstacle(new Vector2(4000, 400), TimeSpan.FromSeconds(15));
            obstacle.Add(obstacle4);
            AddSceneObject(obstacle4);

            Obstacle obstacle5 = new Obstacle(new Vector2(6000, 400), TimeSpan.FromSeconds(18));
            obstacle.Add(obstacle5);
            AddSceneObject(obstacle5);
        }

        public void SpawnScore(int i)
        {
            switch (i)
            {
                case 0:
                    SceneManager.sux.Play();
                    sux.CanDraw = true;
                    sux.PlayAnimation(false);
                    break;
                case 1:
                    SceneManager.pulse.Play();
                    leet.CanDraw = true;
                    leet.PlayAnimation(false);
                    break;
                case 2:
                    SceneManager.pulse.Play();
                    meh.CanDraw = true;
                    meh.PlayAnimation(false);
                    break;
                case 3:
                    SceneManager.pulse.Play();
                    gr8.CanDraw = true;
                    gr8.PlayAnimation(false);
                    break;
            }
        }

        public void InitiateScore()
        {
            sux = new GameAnimatedSprite("level\\sux", 5, 15, new Point(200, 70), 1);
            sux.Origin = new Vector2(100, 35);
            sux.Translate(500, 200);
            sux.CanDraw = false;
            AddSceneObject(sux);

            meh = new GameAnimatedSprite("level\\meh", 5, 15, new Point(200, 70), 1);
            meh.Origin = new Vector2(100, 35);
            meh.Translate(500, 200);
            meh.CanDraw = false;
            AddSceneObject(meh);

            gr8 = new GameAnimatedSprite("level\\gr8", 5, 15, new Point(200, 70), 1);
            gr8.Origin = new Vector2(100, 35);
            gr8.Translate(500, 200);
            gr8.CanDraw = false;
            AddSceneObject(gr8);

            leet = new GameAnimatedSprite("level\\leet", 5, 15, new Point(200, 70), 1);
            leet.Origin = new Vector2(100, 35);
            leet.Translate(500, 200);
            leet.CanDraw = false;
            AddSceneObject(leet);
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            if (isFinished)
            {
                isFinished = false;

                SceneManager.push.Play();
                SceneManager.SetActiveScene("MainMenu");
                SceneManager.ActiveScene.ResetScene();
                SceneManager.PlaySong(1);
            }
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

            text.Text = "Score: " + MediaPlayer.PlayPosition.TotalSeconds;
            UpdateScore();

            if (MediaPlayer.PlayPosition >= TimeSpan.FromSeconds(67.0))
            {
                pesawat.CurrentState = Pesawat.StatePesawat.Normal;
                isFinished = true;
                pesawat.Finished();
            }

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

            if (sux.LocalPosition.Y <= 50)
            {
                sux.Translate(pesawat.Sprite.LocalPosition - new Vector2(-120, -80));
                meh.Translate(pesawat.Sprite.LocalPosition - new Vector2(-120, -80));
                gr8.Translate(pesawat.Sprite.LocalPosition - new Vector2(-120, -80));
                leet.Translate(pesawat.Sprite.LocalPosition - new Vector2(-120, -80));
            }
        }

        public override void Draw(RenderContext rendercontext)
        {
            DrawPath(rendercontext);

            base.Draw(rendercontext);
        }

        public void DrawPath(RenderContext rendercontext)
        {
            rendercontext.SpriteBatch.DrawLine(new Vector2(600f, 180f), new Vector2(1650f, 180f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(600f, 300f), new Vector2(1650f, 300f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(1650f, 180f), new Vector2(2450f, 350f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(1650f, 300f), new Vector2(2450f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(2450f, 350f), new Vector2(3300f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(2450f, 470f), new Vector2(3300f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(3300f, 10f), new Vector2(4100f, 350f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(3300f, 130f), new Vector2(4100f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4100f, 350f), new Vector2(4150f, 150f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4100f, 470f), new Vector2(4200f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4150f, 150f), new Vector2(4300f, 150f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4200f, 470f), new Vector2(4300f, 250f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4300f, 150f), new Vector2(4400f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4300f, 250f), new Vector2(4400f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4400f, 200f), new Vector2(4600f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4400f, 320f), new Vector2(4600f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4600f, 200f), new Vector2(4800f, 270f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4600f, 320f), new Vector2(4800f, 390f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4800f, 270f), new Vector2(4950f, 270f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4800f, 390f), new Vector2(5050f, 390f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4950f, 270f), new Vector2(5050f, 50f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5050f, 390f), new Vector2(5150f, 170f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5050f, 50f), new Vector2(5400f, 50f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5150f, 170f), new Vector2(5300f, 170f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5400f, 50f), new Vector2(5500f, 270f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5300f, 170f), new Vector2(5400f, 390f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5500f, 270f), new Vector2(5650f, 270f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5400f, 390f), new Vector2(5650f, 390f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5650f, 270f), new Vector2(5700f, 300f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5650f, 390f), new Vector2(5700f, 420f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5700f, 300f), new Vector2(5850f, 300f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5700f, 420f), new Vector2(5900f, 420f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5850f, 300f), new Vector2(6000f, 50f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5900f, 420f), new Vector2(6100f, 170f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6000f, 50f), new Vector2(6200f, 50f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6100f, 170f), new Vector2(6200f, 170f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6200f, 50f), new Vector2(6600f, 300f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6200f, 170f), new Vector2(6600f, 420f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6600f, 300f), new Vector2(6600f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6600f, 420f), new Vector2(6720f, 420f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6600f, 200f), new Vector2(6800f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6720f, 420f), new Vector2(6720f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6800f, 200f), new Vector2(6800f, 100f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6720f, 320f), new Vector2(6920f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6800f, 100f), new Vector2(7000f, 100f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6920f, 320f), new Vector2(6920f, 220f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7000f, 100f), new Vector2(7000f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6920f, 220f), new Vector2(7100f, 220f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7000f, 10f), new Vector2(7200f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7100f, 220f), new Vector2(7100f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7200f, 10f), new Vector2(7300f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7100f, 130f), new Vector2(7200f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7300f, 10f), new Vector2(7400f, 350f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7200f, 130f), new Vector2(7300f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7400f, 350f), new Vector2(7500f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7300f, 470f), new Vector2(7500f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7500f, 10f), new Vector2(7800f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7500f, 470f), new Vector2(7600f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7800f, 10f), new Vector2(7900f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7600f, 130f), new Vector2(7800f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7900f, 10f), new Vector2(8000f, 350f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7800f, 130f), new Vector2(7900f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8000f, 350f), new Vector2(8250f, 350f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7900f, 470f), new Vector2(8350f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8250f, 350f), new Vector2(8350f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(8350f, 470f), new Vector2(8450f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8350f, 10f), new Vector2(8750f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(8450f, 130f), new Vector2(8650f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8750f, 10f), new Vector2(8850f, 350f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(8650f, 130f), new Vector2(8750f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8850f, 350f), new Vector2(9100f, 350f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(8750f, 470f), new Vector2(9100f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(9100f, 350f), new Vector2(9500f, 150f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(9100f, 470f), new Vector2(9500f, 270f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(9500f, 150f), new Vector2(9950f, 300f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(9500f, 270f), new Vector2(9950f, 420f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(9950f, 300f), new Vector2(10350f, 100f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(9950f, 420f), new Vector2(10350f, 220f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10350f, 100f), new Vector2(10650f, 220f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10350f, 220f), new Vector2(10650f, 340f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10650f, 220f), new Vector2(10750f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10650f, 340f), new Vector2(10750f, 340f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10750f, 10f), new Vector2(10990f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10750f, 340f), new Vector2(10870f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10990f, 10f), new Vector2(10990f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10870f, 130f), new Vector2(10870f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10990f, 200f), new Vector2(11100f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10870f, 320f), new Vector2(11220f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11100f, 200f), new Vector2(11100f, 80f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11220f, 320f), new Vector2(11220f, 200f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11100f, 80f), new Vector2(11350f, 80f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11220f, 200f), new Vector2(11470f, 200f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11350f, 80f), new Vector2(11350f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11470f, 200f), new Vector2(11470f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11350f, 10f), new Vector2(11620f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11470f, 130f), new Vector2(11500f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11620f, 10f), new Vector2(11620f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11500f, 130f), new Vector2(11500f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11620f, 200f), new Vector2(11680f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11500f, 320f), new Vector2(11800f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11680f, 200f), new Vector2(11680f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11800f, 320f), new Vector2(11800f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11680f, 10f), new Vector2(12000f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11800f, 130f), new Vector2(11880f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12000f, 10f), new Vector2(12100f, 250f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11880f, 130f), new Vector2(11980f, 370f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12100f, 250f), new Vector2(12100f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11980f, 370f), new Vector2(12150f, 370f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12100f, 200f), new Vector2(12250f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12150f, 370f), new Vector2(12150f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12250f, 200f), new Vector2(12250f, 100f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12150f, 320f), new Vector2(12370f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12250f, 100f), new Vector2(12370f, 100f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12370f, 320f), new Vector2(12370f, 220f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12370f, 100f), new Vector2(12500f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12370f, 220f), new Vector2(12500f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12500f, 10f), new Vector2(12700f, 180f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12500f, 130f), new Vector2(12700f, 300f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12700f, 180f), new Vector2(12870f, 180f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12700f, 300f), new Vector2(12750f, 300f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12870f, 180f), new Vector2(12870f, 300f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12750f, 300f), new Vector2(12750f, 420f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12870f, 300f), new Vector2(13090f, 300f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12750f, 420f), new Vector2(12970f, 420f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12870f, 300f), new Vector2(13090f, 300f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12750f, 420f), new Vector2(12970f, 420f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13090f, 300f), new Vector2(13150f, 350f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12970f, 420f), new Vector2(13030f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13150f, 350f), new Vector2(13150f, 280f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13030f, 470f), new Vector2(13270f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13150f, 280f), new Vector2(13200f, 280f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13270f, 470f), new Vector2(13270f, 400f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13200f, 280f), new Vector2(13200f, 230f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13270f, 400f), new Vector2(13320f, 400f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13200f, 230f), new Vector2(13300f, 230f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13320f, 400f), new Vector2(13320f, 350f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13300f, 230f), new Vector2(13300f, 180f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13320f, 350f), new Vector2(13420f, 350f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13300f, 180f), new Vector2(13350f, 180f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13420f, 350f), new Vector2(13420f, 300f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13350f, 180f), new Vector2(13350f, 130f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13420f, 300f), new Vector2(13470f, 300f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13350f, 130f), new Vector2(13400f, 130f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13470f, 300f), new Vector2(13470f, 250f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13400f, 130f), new Vector2(13500f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13470f, 250f), new Vector2(13570f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13500f, 10f), new Vector2(13690f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13570f, 130f), new Vector2(13570f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13690f, 10f), new Vector2(13690f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13570f, 320f), new Vector2(13800f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13690f, 200f), new Vector2(13900f, 350f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13800f, 470f), new Vector2(13900f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13900f, 350f), new Vector2(14100f, 150f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13900f, 470f), new Vector2(14100f, 350f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(14100f, 150f), new Vector2(14100f, 150f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(14100f, 350f), new Vector2(14100f, 350f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(14100f, 150f), new Vector2(14520f, 150f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(14100f, 350f), new Vector2(14200f, 350f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(14520f, 150f), new Vector2(14520f, 270f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(14200f, 350f), new Vector2(14200f, 270f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(14520f, 270f), new Vector2(14600f, 270f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(14200f, 270f), new Vector2(14400f, 270f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(14600f, 270f), new Vector2(14700f, 150f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(14400f, 270f), new Vector2(14400f, 390f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(14700f, 150f), new Vector2(15100f, 150f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(14400f, 390f), new Vector2(14700f, 390f), lineColor, 5);

            // ---
            rendercontext.SpriteBatch.DrawLine(new Vector2(14700f, 390f), new Vector2(14800f, 270f), lineColor, 5);

            // ---
            rendercontext.SpriteBatch.DrawLine(new Vector2(14800f, 270f), new Vector2(15000f, 270f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(15100f, 150f), new Vector2(15100f, 250f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(15000f, 270f), new Vector2(15000f, 370f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(15100f, 250f), new Vector2(15400f, 250f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(15000f, 370f), new Vector2(15500f, 370f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(15400f, 250f), new Vector2(15400f, 150f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(15500f, 370f), new Vector2(15500f, 270f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(15400f, 150f), new Vector2(15800f, 150f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(15500f, 270f), new Vector2(15800f, 270f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(15800f, 150f), new Vector2(16000f, 350f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(15800f, 270f), new Vector2(16000f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16000f, 350f), new Vector2(16000f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16000f, 470f), new Vector2(16120f, 470f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16000f, 200f), new Vector2(16060f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16120f, 470f), new Vector2(16120f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16060f, 200f), new Vector2(16060f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16120f, 320f), new Vector2(16180f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16060f, 10f), new Vector2(16400f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16180f, 320f), new Vector2(16180f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16400f, 10f), new Vector2(16450f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16180f, 130f), new Vector2(16300f, 130f), lineColor, 5);

            //
            rendercontext.SpriteBatch.DrawLine(new Vector2(16300f, 130f), new Vector2(16350f, 400f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16450f, 200f), new Vector2(16500f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16350f, 400f), new Vector2(16500f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16500f, 200f), new Vector2(16700f, 100f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16500f, 320f), new Vector2(16700f, 220f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16700f, 100f), new Vector2(16900f, 200f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16700f, 220f), new Vector2(16900f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16900f, 200f), new Vector2(16900f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16900f, 320f), new Vector2(17020f, 320f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16900f, 10f), new Vector2(17170f, 10f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(17020f, 320f), new Vector2(17020f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(17170f, 10f), new Vector2(17170f, 180f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(17020f, 130f), new Vector2(17050f, 130f), lineColor, 5);

            rendercontext.SpriteBatch.DrawLine(new Vector2(17170f, 180f), new Vector2(17800f, 180f), lineColor, 5);
            rendercontext.SpriteBatch.DrawLine(new Vector2(17050f, 130f), new Vector2(17050f, 300f), lineColor, 5);

            //
            rendercontext.SpriteBatch.DrawLine(new Vector2(17050f, 300f), new Vector2(17800f, 300f), lineColor, 5);
        }

        public override void ResetScene()
        {
            delay = Delay;
            pesawat.Reset();
            pesawat.CurrentState = Pesawat.StatePesawat.Normal;

            foreach (Obstacle ob in obstacle)
                ob.Reset();
        }
    }
}
