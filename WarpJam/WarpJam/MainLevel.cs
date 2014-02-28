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
        private GameSprite finish;
        private GameAnimatedSprite sux, meh, gr8, leet, ready, set, warp;

        private const double Delay = 3;
        private double delay = Delay;
        private Color lineColor = Color.DeepPink;
        private bool isFinished = false;
        private bool isWarp = false;
        private bool isDone = false;

        private int colorFactor = 1;
        private int healthFactor = 2;
        private int score = 0;

        #region particles
        
        #endregion

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
            text.Color = lineColor;
            AddHUDObject(text);

            InitiateScore();
            InitiateIgnition();

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

            finish = new GameSprite("level\\finish");
            finish.Translate(183, 100);
            finish.CanDraw = false;
            AddHUDObject(finish);

            pesawat = new Pesawat();
            AddSceneObject(pesawat);

            bg_particle = new BackgroundParticle();

            MediaPlayer.MediaStateChanged += new EventHandler<EventArgs>(MediaPlayer_MediaStateChanged);

            base.Initialize();
        }

        public void InitiateIgnition()
        {
            ready = new GameAnimatedSprite("level\\ready", 5, 170, new Point(450, 170), 1);
            ready.Translate(250, 167);
            ready.PlayAnimation(false);
            AddHUDObject(ready);

            set = new GameAnimatedSprite("level\\set", 5, 170, new Point(450, 170), 1);
            set.Translate(300, 167);
            set.CanDraw = false;
            AddHUDObject(set);

            warp = new GameAnimatedSprite("level\\warp", 5, 170, new Point(450, 170), 1);
            warp.Translate(250, 167);
            warp.CanDraw = false;
            AddHUDObject(warp);
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
            Obstacle obstacle1 = new Obstacle(new Vector2(2433, 400), TimeSpan.FromSeconds(6.729));
            obstacle.Add(obstacle1);
            AddSceneObject(obstacle1);
            // AddObjectWithParticle(obstacle1);

            Obstacle obstacle2 = new Obstacle(new Vector2(3263, 80), TimeSpan.FromSeconds(10.056));
            obstacle.Add(obstacle2);
            AddSceneObject(obstacle2);
            // AddObjectWithParticle(obstacle2);

            Obstacle obstacle3 = new Obstacle(new Vector2(4080, 400), TimeSpan.FromSeconds(13.281));
            obstacle.Add(obstacle3);
            AddSceneObject(obstacle3);
            // AddObjectWithParticle(obstacle3);

            Obstacle obstacle4 = new Obstacle(new Vector2(4280, 210), TimeSpan.FromSeconds(14.131));
            obstacle.Add(obstacle4);
            AddSceneObject(obstacle4);
            // AddObjectWithParticle(obstacle4);

            Obstacle obstacle5 = new Obstacle(new Vector2(4705, 300), TimeSpan.FromSeconds(15.794));
            obstacle.Add(obstacle5);
            AddSceneObject(obstacle5);
            // AddObjectWithParticle(obstacle5);
            
            Obstacle obstacle6 = new Obstacle(new Vector2(5122, 120), TimeSpan.FromSeconds(17.460));
            obstacle.Add(obstacle6);
            AddSceneObject(obstacle6);
            // AddObjectWithParticle(obstacle6);

            Obstacle obstacle7 = new Obstacle(new Vector2(5222, 120), TimeSpan.FromSeconds(17.802));
            obstacle.Add(obstacle7);
            AddSceneObject(obstacle7);
            // AddObjectWithParticle(obstacle7);

            Obstacle obstacle8 = new Obstacle(new Vector2(5513, 330), TimeSpan.FromSeconds(19.014));
            obstacle.Add(obstacle8);
            AddSceneObject(obstacle8);
            // AddObjectWithParticle(obstacle8);

            Obstacle obstacle9 = new Obstacle(new Vector2(5938, 275), TimeSpan.FromSeconds(20.688));
            obstacle.Add(obstacle9);
            AddSceneObject(obstacle9);
            // AddObjectWithParticle(obstacle9);

            Obstacle obstacle10 = new Obstacle(new Vector2(6355, 210), TimeSpan.FromSeconds(22.387));
            obstacle.Add(obstacle10);
            AddSceneObject(obstacle10);
            // AddObjectWithParticle(obstacle10);

            Obstacle obstacle11 = new Obstacle(new Vector2(6772, 270), TimeSpan.FromSeconds(24.085));
            obstacle.Add(obstacle11);
            AddSceneObject(obstacle11);
            // AddObjectWithParticle(obstacle11);

            Obstacle obstacle12 = new Obstacle(new Vector2(6863, 230), TimeSpan.FromSeconds(24.460));
            obstacle.Add(obstacle12);
            AddSceneObject(obstacle12);
            // AddObjectWithParticle(obstacle12);

            Obstacle obstacle13 = new Obstacle(new Vector2(7180, 75), TimeSpan.FromSeconds(25.717));
            obstacle.Add(obstacle13);
            AddSceneObject(obstacle13);
            // AddObjectWithParticle(obstacle13);

            Obstacle obstacle14 = new Obstacle(new Vector2(7605, 66), TimeSpan.FromSeconds(27.380));
            obstacle.Add(obstacle14);
            AddSceneObject(obstacle14);
            // AddObjectWithParticle(obstacle14);

            Obstacle obstacle15 = new Obstacle(new Vector2(7688, 66), TimeSpan.FromSeconds(27.720));
            obstacle.Add(obstacle15);
            AddSceneObject(obstacle15);
            // AddObjectWithParticle(obstacle15);

            Obstacle obstacle16 = new Obstacle(new Vector2(8005, 410), TimeSpan.FromSeconds(29.011));
            obstacle.Add(obstacle16);
            AddSceneObject(obstacle16);
            // AddObjectWithParticle(obstacle16);

            Obstacle obstacle17 = new Obstacle(new Vector2(8438, 85), TimeSpan.FromSeconds(30.672));
            obstacle.Add(obstacle17);
            AddSceneObject(obstacle17);
            // AddObjectWithParticle(obstacle17);

            Obstacle obstacle18 = new Obstacle(new Vector2(8847, 400), TimeSpan.FromSeconds(32.340));
            obstacle.Add(obstacle18);
            AddSceneObject(obstacle18);
            // AddObjectWithParticle(obstacle18);

            Obstacle obstacle19 = new Obstacle(new Vector2(8930, 400), TimeSpan.FromSeconds(32.697));
            obstacle.Add(obstacle19);
            AddSceneObject(obstacle19);
            // AddObjectWithParticle(obstacle18);

            Obstacle obstacle20 = new Obstacle(new Vector2(9272, 315), TimeSpan.FromSeconds(34.038));
            obstacle.Add(obstacle20);
            AddSceneObject(obstacle20);
            //// AddObjectWithParticle(obstacle19);

            Obstacle obstacle21 = new Obstacle(new Vector2(9680, 280), TimeSpan.FromSeconds(35.637));
            obstacle.Add(obstacle21);
            AddSceneObject(obstacle21);
            //// AddObjectWithParticle(obstacle20);

            Obstacle obstacle22 = new Obstacle(new Vector2(9763, 300), TimeSpan.FromSeconds(36.039));
            obstacle.Add(obstacle22);
            AddSceneObject(obstacle22);
            //// AddObjectWithParticle(obstacle21);

            Obstacle obstacle23 = new Obstacle(new Vector2(10088, 270), TimeSpan.FromSeconds(37.300));
            obstacle.Add(obstacle23);
            AddSceneObject(obstacle23);
            //// AddObjectWithParticle(obstacle22);

            Obstacle obstacle24 = new Obstacle(new Vector2(10172, 240), TimeSpan.FromSeconds(37.671));
            obstacle.Add(obstacle24);
            AddSceneObject(obstacle24);
            //// AddObjectWithParticle(obstacle23);

            Obstacle obstacle25 = new Obstacle(new Vector2(10488, 225), TimeSpan.FromSeconds(38.960));
            obstacle.Add(obstacle25);
            AddSceneObject(obstacle25);
            //// AddObjectWithParticle(obstacle24);

            Obstacle obstacle26 = new Obstacle(new Vector2(10588, 270), TimeSpan.FromSeconds(39.300));
            obstacle.Add(obstacle26);
            AddSceneObject(obstacle26);
            //// AddObjectWithParticle(obstacle25);

            Obstacle obstacle27 = new Obstacle(new Vector2(10688, 290), TimeSpan.FromSeconds(39.760));
            obstacle.Add(obstacle27);
            AddSceneObject(obstacle27);
            //// AddObjectWithParticle(obstacle25);

            Obstacle obstacle28 = new Obstacle(new Vector2(10922, 200), TimeSpan.FromSeconds(40.593));
            obstacle.Add(obstacle28);
            AddSceneObject(obstacle28);
            //// AddObjectWithParticle(obstacle26);

            Obstacle obstacle29 = new Obstacle(new Vector2(11330, 125), TimeSpan.FromSeconds(42.255));
            obstacle.Add(obstacle29);
            AddSceneObject(obstacle29);
            //// AddObjectWithParticle(obstacle27);

            Obstacle obstacle30 = new Obstacle(new Vector2(11763, 100), TimeSpan.FromSeconds(43.956));
            obstacle.Add(obstacle30);
            AddSceneObject(obstacle30);
            //// AddObjectWithParticle(obstacle28);

            Obstacle obstacle31 = new Obstacle(new Vector2(11838, 75), TimeSpan.FromSeconds(44.288));
            obstacle.Add(obstacle31);
            AddSceneObject(obstacle31);
            //// AddObjectWithParticle(obstacle29);

            Obstacle obstacle32 = new Obstacle(new Vector2(12155, 260), TimeSpan.FromSeconds(45.584));
            obstacle.Add(obstacle32);
            AddSceneObject(obstacle32);
            //// AddObjectWithParticle(obstacle30);

            Obstacle obstacle33 = new Obstacle(new Vector2(12238, 275), TimeSpan.FromSeconds(45.923));
            obstacle.Add(obstacle33);
            AddSceneObject(obstacle33);
            //// AddObjectWithParticle(obstacle31);

            Obstacle obstacle34 = new Obstacle(new Vector2(12563, 135), TimeSpan.FromSeconds(47.251));
            obstacle.Add(obstacle34);
            AddSceneObject(obstacle34);
            //// AddObjectWithParticle(obstacle32);

            Obstacle obstacle35 = new Obstacle(new Vector2(12980, 350), TimeSpan.FromSeconds(48.879));
            obstacle.Add(obstacle35);
            AddSceneObject(obstacle35);
            //// AddObjectWithParticle(obstacle34);

            Obstacle obstacle36 = new Obstacle(new Vector2(13413, 190), TimeSpan.FromSeconds(50.577));
            obstacle.Add(obstacle36);
            AddSceneObject(obstacle36);
            //// AddObjectWithParticle(obstacle35);

            Obstacle obstacle37 = new Obstacle(new Vector2(13813, 400), TimeSpan.FromSeconds(52.207));
            obstacle.Add(obstacle37);
            AddSceneObject(obstacle37);
            //// AddObjectWithParticle(obstacle37);

            Obstacle obstacle38 = new Obstacle(new Vector2(14238, 210), TimeSpan.FromSeconds(53.871));
            obstacle.Add(obstacle38);
            AddSceneObject(obstacle38);
            //// AddObjectWithParticle(obstacle38);

            Obstacle obstacle39 = new Obstacle(new Vector2(14646, 275), TimeSpan.FromSeconds(55.502));
            obstacle.Add(obstacle39);
            AddSceneObject(obstacle39);
            //// AddObjectWithParticle(obstacle39);

            Obstacle obstacle40 = new Obstacle(new Vector2(14738, 225), TimeSpan.FromSeconds(55.876));
            obstacle.Add(obstacle40);
            AddSceneObject(obstacle40);
            //// AddObjectWithParticle(obstacle40);

            Obstacle obstacle41 = new Obstacle(new Vector2(15046, 300), TimeSpan.FromSeconds(57.234));
            obstacle.Add(obstacle41);
            AddSceneObject(obstacle41);
            //// AddObjectWithParticle(obstacle41);

            Obstacle obstacle42 = new Obstacle(new Vector2(15455, 250), TimeSpan.FromSeconds(58.864));
            obstacle.Add(obstacle42);
            AddSceneObject(obstacle42);
            //// AddObjectWithParticle(obstacle42);

            Obstacle obstacle43 = new Obstacle(new Vector2(15896, 300), TimeSpan.FromSeconds(60.441));
            obstacle.Add(obstacle43);
            AddSceneObject(obstacle43);
            // AddObjectWithParticle(obstacle43);

            Obstacle obstacle44 = new Obstacle(new Vector2(16313, 80), TimeSpan.FromSeconds(62.163));
            obstacle.Add(obstacle44);
            AddSceneObject(obstacle44);
            // AddObjectWithParticle(obstacle44);

            Obstacle obstacle45 = new Obstacle(new Vector2(16396, 200), TimeSpan.FromSeconds(62.503));
            obstacle.Add(obstacle45);
            AddSceneObject(obstacle45);
            // AddObjectWithParticle(obstacle45);

            Obstacle obstacle46 = new Obstacle(new Vector2(16713, 180), TimeSpan.FromSeconds(63.758));
            obstacle.Add(obstacle46);
            AddSceneObject(obstacle46);
            // AddObjectWithParticle(obstacle46);

            Obstacle obstacle47 = new Obstacle(new Vector2(17138, 250), TimeSpan.FromSeconds(65.455));
            obstacle.Add(obstacle47);
            AddSceneObject(obstacle47);
            // AddObjectWithParticle(obstacle47);

            Obstacle obstacle48 = new Obstacle(new Vector2(17229, 250), TimeSpan.FromSeconds(65.829));
            obstacle.Add(obstacle48);
            AddSceneObject(obstacle48);
            // AddObjectWithParticle(obstacle48);

            Obstacle obstacle49 = new Obstacle(new Vector2(17329, 250), TimeSpan.FromSeconds(66.236));
            obstacle.Add(obstacle49);
            AddSceneObject(obstacle49);
            // AddObjectWithParticle(obstacle48);
        }

        public void SetHealthFactor(bool sign, int delta)
        {
            if (sign)
            {
                healthFactor += delta;
                if (healthFactor >= 100)
                    healthFactor = 100;
            }
            else
            {
                healthFactor -= delta;
                if (healthFactor <= 2)
                    healthFactor = 2;
            }

        }

        public void SpawnScore(int i)
        {
            switch (i)
            {
                case 0:
                    SceneManager.sux.Play();
                    sux.CanDraw = true;
                    sux.PlayAnimation(false);

                    SetHealthFactor(true, 20);

                    break;
                case 1:
                    SceneManager.pulse.Play();
                    leet.CanDraw = true;
                    leet.PlayAnimation(false);

                    SetHealthFactor(false, 20);
                    score += 100;

                    break;
                case 2:
                    SceneManager.pulse.Play();
                    meh.CanDraw = true;
                    meh.PlayAnimation(false);

                    SetHealthFactor(false, 5);
                    score += 50;

                    break;
                case 3:
                    SceneManager.pulse.Play();
                    gr8.CanDraw = true;
                    gr8.PlayAnimation(false);

                    SetHealthFactor(false, 15);
                    score += 75;

                    break;
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

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void LoadParticle(ContentManager contentmanager, ProjectMercury.Renderers.SpriteBatchRenderer particleRenderer)
        {
            base.LoadParticle(contentmanager, particleRenderer);
            bg_particle.LoadParticle(contentmanager, particleRenderer);
        }

        void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            if (isFinished)
            {
                isFinished = false;

                SceneManager.PlaySong(1);
                finish.CanDraw = true;
                text.Translate(340, 350);

                //SceneManager.push.Play();
                //SceneManager.SetActiveScene("MainMenu");
                //SceneManager.ActiveScene.ResetScene();
                //SceneManager.PlaySong(1);
            }
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            //particle bg
            bg_particle.Update(rendercontext);
            //end particle bg

            if (delay > 0)
            {
                delay -= rendercontext.GameTime.ElapsedGameTime.TotalSeconds;

                if (!ready.IsPlaying && !isWarp)
                {
                    ready.CanDraw = false;
                    set.CanDraw = true;
                    set.PlayAnimation(false);
                    isWarp = true;
                }

                if (!ready.IsPlaying && !set.IsPlaying && isWarp && !isDone)
                {
                    set.CanDraw = false;
                    warp.CanDraw = true;
                    warp.PlayAnimation(false);
                    isDone = true;
                }

                if (!ready.IsPlaying && !set.IsPlaying && !warp.IsPlaying && isWarp && isDone)
                    warp.CanDraw = false;
            }

            if (delay <= 0)
            {
                MediaPlayer.Resume();
                pesawat.CurrentState = Pesawat.StatePesawat.Ready;
            }

            text.Text = "Score: " + score;
            UpdateScore();
            UpdateLine();

            if (MediaPlayer.PlayPosition >= TimeSpan.FromSeconds(67.0))
            {
                pesawat.CurrentState = Pesawat.StatePesawat.Normal;
                isFinished = true;
                pesawat.Finished();
            }

            base.Update(rendercontext, contentmanager);
        }

        public void UpdateLine()
        {
            // tes ganti warna
            Color tempc = lineColor;
            int tempb = tempc.G + (healthFactor * colorFactor);
            
            if (tempb >= 255) 
            {
                tempb = 255;
                colorFactor = -1;
            }
            if (tempb <= 0) 
            {
                tempb = 0;
                colorFactor = 1;
            }
            
            lineColor.G = (byte)tempb;
            text.Color = lineColor;
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
            rendercontext.SpriteBatch.DrawLine(new Vector2(600f, 180f), new Vector2(1650f, 180f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(600f, 300f), new Vector2(1650f, 300f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(1650f, 180f), new Vector2(2450f, 350f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(1650f, 300f), new Vector2(2450f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(2450f, 350f), new Vector2(3300f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(2450f, 470f), new Vector2(3300f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(3300f, 10f), new Vector2(4100f, 350f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(3300f, 130f), new Vector2(4100f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4100f, 350f), new Vector2(4150f, 150f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4100f, 470f), new Vector2(4200f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4150f, 150f), new Vector2(4300f, 150f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4200f, 470f), new Vector2(4300f, 250f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4300f, 150f), new Vector2(4400f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4300f, 250f), new Vector2(4400f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4400f, 200f), new Vector2(4600f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4400f, 320f), new Vector2(4600f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4600f, 200f), new Vector2(4800f, 270f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4600f, 320f), new Vector2(4800f, 390f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4800f, 270f), new Vector2(4950f, 270f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(4800f, 390f), new Vector2(5050f, 390f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(4950f, 270f), new Vector2(5050f, 50f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5050f, 390f), new Vector2(5150f, 170f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5050f, 50f), new Vector2(5400f, 50f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5150f, 170f), new Vector2(5300f, 170f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5400f, 50f), new Vector2(5500f, 270f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5300f, 170f), new Vector2(5400f, 390f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5500f, 270f), new Vector2(5650f, 270f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5400f, 390f), new Vector2(5650f, 390f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5650f, 270f), new Vector2(5700f, 300f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5650f, 390f), new Vector2(5700f, 420f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5700f, 300f), new Vector2(5850f, 300f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5700f, 420f), new Vector2(5900f, 420f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(5850f, 300f), new Vector2(6000f, 50f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(5900f, 420f), new Vector2(6100f, 170f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6000f, 50f), new Vector2(6200f, 50f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6100f, 170f), new Vector2(6200f, 170f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6200f, 50f), new Vector2(6600f, 300f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6200f, 170f), new Vector2(6600f, 420f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6600f, 300f), new Vector2(6600f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6600f, 420f), new Vector2(6720f, 420f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6600f, 200f), new Vector2(6800f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6720f, 420f), new Vector2(6720f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6800f, 200f), new Vector2(6800f, 100f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6720f, 320f), new Vector2(6920f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(6800f, 100f), new Vector2(7000f, 100f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6920f, 320f), new Vector2(6920f, 220f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7000f, 100f), new Vector2(7000f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(6920f, 220f), new Vector2(7100f, 220f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7000f, 10f), new Vector2(7200f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7100f, 220f), new Vector2(7100f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7200f, 10f), new Vector2(7300f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7100f, 130f), new Vector2(7200f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7300f, 10f), new Vector2(7400f, 350f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7200f, 130f), new Vector2(7300f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7400f, 350f), new Vector2(7500f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7300f, 470f), new Vector2(7500f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7500f, 10f), new Vector2(7800f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7500f, 470f), new Vector2(7600f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7800f, 10f), new Vector2(7900f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7600f, 130f), new Vector2(7800f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(7900f, 10f), new Vector2(8000f, 350f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7800f, 130f), new Vector2(7900f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8000f, 350f), new Vector2(8250f, 350f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(7900f, 470f), new Vector2(8350f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8250f, 350f), new Vector2(8350f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(8350f, 470f), new Vector2(8450f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8350f, 10f), new Vector2(8750f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(8450f, 130f), new Vector2(8650f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8750f, 10f), new Vector2(8850f, 350f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(8650f, 130f), new Vector2(8750f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(8850f, 350f), new Vector2(9100f, 350f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(8750f, 470f), new Vector2(9100f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(9100f, 350f), new Vector2(9500f, 150f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(9100f, 470f), new Vector2(9500f, 270f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(9500f, 150f), new Vector2(9950f, 300f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(9500f, 270f), new Vector2(9950f, 420f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(9950f, 300f), new Vector2(10350f, 100f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(9950f, 420f), new Vector2(10350f, 220f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10350f, 100f), new Vector2(10650f, 220f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10350f, 220f), new Vector2(10650f, 340f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10650f, 220f), new Vector2(10750f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10650f, 340f), new Vector2(10750f, 340f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10750f, 10f), new Vector2(10990f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10750f, 340f), new Vector2(10870f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10990f, 10f), new Vector2(10990f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10870f, 130f), new Vector2(10870f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(10990f, 200f), new Vector2(11100f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(10870f, 320f), new Vector2(11220f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11100f, 200f), new Vector2(11100f, 80f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11220f, 320f), new Vector2(11220f, 200f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11100f, 80f), new Vector2(11350f, 80f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11220f, 200f), new Vector2(11470f, 200f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11350f, 80f), new Vector2(11350f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11470f, 200f), new Vector2(11470f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11350f, 10f), new Vector2(11620f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11470f, 130f), new Vector2(11500f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11620f, 10f), new Vector2(11620f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11500f, 130f), new Vector2(11500f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11620f, 200f), new Vector2(11680f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11500f, 320f), new Vector2(11800f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11680f, 200f), new Vector2(11680f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11800f, 320f), new Vector2(11800f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(11680f, 10f), new Vector2(12000f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11800f, 130f), new Vector2(11880f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12000f, 10f), new Vector2(12100f, 250f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11880f, 130f), new Vector2(11980f, 370f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12100f, 250f), new Vector2(12100f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(11980f, 370f), new Vector2(12150f, 370f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12100f, 200f), new Vector2(12250f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12150f, 370f), new Vector2(12150f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12250f, 200f), new Vector2(12250f, 100f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12150f, 320f), new Vector2(12370f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12250f, 100f), new Vector2(12370f, 100f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12370f, 320f), new Vector2(12370f, 220f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12370f, 100f), new Vector2(12500f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12370f, 220f), new Vector2(12500f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12500f, 10f), new Vector2(12700f, 180f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12500f, 130f), new Vector2(12700f, 300f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12700f, 180f), new Vector2(12870f, 180f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12700f, 300f), new Vector2(12750f, 300f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12870f, 180f), new Vector2(12870f, 300f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12750f, 300f), new Vector2(12750f, 420f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12870f, 300f), new Vector2(13090f, 300f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12750f, 420f), new Vector2(12970f, 420f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(12870f, 300f), new Vector2(13090f, 300f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12750f, 420f), new Vector2(12970f, 420f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13090f, 300f), new Vector2(13150f, 350f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(12970f, 420f), new Vector2(13030f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13150f, 350f), new Vector2(13150f, 280f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13030f, 470f), new Vector2(13270f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13150f, 280f), new Vector2(13200f, 280f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13270f, 470f), new Vector2(13270f, 400f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13200f, 280f), new Vector2(13200f, 230f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13270f, 400f), new Vector2(13320f, 400f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13200f, 230f), new Vector2(13300f, 230f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13320f, 400f), new Vector2(13320f, 350f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13300f, 230f), new Vector2(13300f, 180f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13320f, 350f), new Vector2(13420f, 350f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13300f, 180f), new Vector2(13350f, 180f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13420f, 350f), new Vector2(13420f, 300f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13350f, 180f), new Vector2(13350f, 130f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13420f, 300f), new Vector2(13470f, 300f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13350f, 130f), new Vector2(13400f, 130f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13470f, 300f), new Vector2(13470f, 250f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13400f, 130f), new Vector2(13500f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13470f, 250f), new Vector2(13570f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13500f, 10f), new Vector2(13690f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13570f, 130f), new Vector2(13570f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13690f, 10f), new Vector2(13690f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13570f, 320f), new Vector2(13800f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13690f, 200f), new Vector2(13900f, 350f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13800f, 470f), new Vector2(13900f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(13900f, 350f), new Vector2(14100f, 150f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(13900f, 470f), new Vector2(14100f, 350f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(14100f, 150f), new Vector2(14100f, 150f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(14100f, 350f), new Vector2(14100f, 350f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(14100f, 150f), new Vector2(14520f, 150f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(14100f, 350f), new Vector2(14200f, 350f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(14520f, 150f), new Vector2(14520f, 270f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(14200f, 350f), new Vector2(14200f, 270f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(14520f, 270f), new Vector2(14600f, 270f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(14200f, 270f), new Vector2(14400f, 270f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(14600f, 270f), new Vector2(14700f, 150f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(14400f, 270f), new Vector2(14400f, 390f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(14700f, 150f), new Vector2(15100f, 150f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(14400f, 390f), new Vector2(14700f, 390f), lineColor, 2);

            // ---
            rendercontext.SpriteBatch.DrawLine(new Vector2(14700f, 390f), new Vector2(14800f, 270f), lineColor, 2);

            // ---
            rendercontext.SpriteBatch.DrawLine(new Vector2(14800f, 270f), new Vector2(15000f, 270f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(15100f, 150f), new Vector2(15100f, 250f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(15000f, 270f), new Vector2(15000f, 370f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(15100f, 250f), new Vector2(15400f, 250f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(15000f, 370f), new Vector2(15500f, 370f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(15400f, 250f), new Vector2(15400f, 150f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(15500f, 370f), new Vector2(15500f, 270f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(15400f, 150f), new Vector2(15800f, 150f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(15500f, 270f), new Vector2(15800f, 270f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(15800f, 150f), new Vector2(16000f, 350f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(15800f, 270f), new Vector2(16000f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16000f, 350f), new Vector2(16000f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16000f, 470f), new Vector2(16120f, 470f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16000f, 200f), new Vector2(16060f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16120f, 470f), new Vector2(16120f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16060f, 200f), new Vector2(16060f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16120f, 320f), new Vector2(16180f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16060f, 10f), new Vector2(16400f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16180f, 320f), new Vector2(16180f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16400f, 10f), new Vector2(16450f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16180f, 130f), new Vector2(16300f, 130f), lineColor, 2);

            //
            rendercontext.SpriteBatch.DrawLine(new Vector2(16300f, 130f), new Vector2(16350f, 400f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16450f, 200f), new Vector2(16500f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16350f, 400f), new Vector2(16500f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16500f, 200f), new Vector2(16700f, 100f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16500f, 320f), new Vector2(16700f, 220f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16700f, 100f), new Vector2(16900f, 200f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16700f, 220f), new Vector2(16900f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16900f, 200f), new Vector2(16900f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(16900f, 320f), new Vector2(17020f, 320f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(16900f, 10f), new Vector2(17170f, 10f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(17020f, 320f), new Vector2(17020f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(17170f, 10f), new Vector2(17170f, 180f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(17020f, 130f), new Vector2(17050f, 130f), lineColor, 2);

            rendercontext.SpriteBatch.DrawLine(new Vector2(17170f, 180f), new Vector2(17800f, 180f), lineColor, 2);
            rendercontext.SpriteBatch.DrawLine(new Vector2(17050f, 130f), new Vector2(17050f, 300f), lineColor, 2);

            //
            rendercontext.SpriteBatch.DrawLine(new Vector2(17050f, 300f), new Vector2(17800f, 300f), lineColor, 2);
        }

        public override void ResetScene()
        {
            SceneManager.ignition.Play();

            healthFactor = 2;
            delay = Delay;
            pesawat.Reset();
            pesawat.CurrentState = Pesawat.StatePesawat.Normal;
            score = 0;

            isWarp = false;
            isDone = false;
            ready.CanDraw = true;
            set.CanDraw = false;
            warp.CanDraw = false;
            ready.PlayAnimation(false);

            finish.CanDraw = false;
            text.Translate(600, 50);

            foreach (Obstacle ob in obstacle)
                ob.Reset();
        }
    }
}
