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

        // farseer
        private Body ground;
        private World world;

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

            fire = new GameButton("level\\invisbutton", false);
            fire.CanDraw = false;
            fire.Translate(400, 0);
            fire.OnEnter += () =>
            {
                gunfire.Play();
            };
            AddHUDObject(fire);

            pesawat = new Pesawat();
            AddSceneObject(pesawat);

            base.Initialize();
        }

        public void InitiateStar()
        {
            star1 = new GameAnimatedSprite("menu\\star", 10, 10, new Point(55, 58));
            star1.Color = Color.Cyan;
            star1.Translate(500, 100);
            star1.PlayAnimation(true);
            AddHUDObject(star1);

            star2 = new GameAnimatedSprite("menu\\star", 10, 105, new Point(55, 58));
            star2.Color = Color.Cyan;
            star2.Translate(720, 50);
            star2.PlayAnimation(true);
            AddHUDObject(star2);

            star3 = new GameAnimatedSprite("menu\\star", 10, 80, new Point(55, 58));
            star3.Color = Color.Cyan;
            star3.Translate(10, 340);
            star3.PlayAnimation(true);
            AddHUDObject(star3);

            star4 = new GameAnimatedSprite("menu\\star", 10, 65, new Point(55, 58));
            star4.Color = Color.Cyan;
            star4.Translate(400, 400);
            star4.PlayAnimation(true);
            AddHUDObject(star4);

            star5 = new GameAnimatedSprite("menu\\star", 10, 40, new Point(55, 58));
            star5.Color = Color.Cyan;
            star5.Scale(0.5f, 0.5f);
            star5.Translate(500, 90);
            star5.PlayAnimation(true);
            AddHUDObject(star5);
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);

            gunfire = contentmanager.Load<SoundEffect>("sfx\\gunfire");

            // farseer
            if (world == null)
                world = new World(Vector2.Zero);
            else
                world.Clear();

            ground = new Body(world);
            {
                Vertices terrain = new Vertices();
                terrain.Add(new Vector2(-20f, -5f));
                terrain.Add(new Vector2(-20f, 0f));
                terrain.Add(new Vector2(20f, 0f));
                terrain.Add(new Vector2(25f, -0.25f));
                terrain.Add(new Vector2(30f, -1f));
                terrain.Add(new Vector2(35f, -4f));
                terrain.Add(new Vector2(40f, 0f));
                terrain.Add(new Vector2(45f, 0f));
                terrain.Add(new Vector2(50f, 1f));
                terrain.Add(new Vector2(55f, 2f));

                for (int i = 0; i < terrain.Count - 1; ++i)
                {
                    FixtureFactory.AttachEdge(terrain[i], terrain[i + 1], ground);
                }

                ground.Friction = 0.6f;
            }
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            text.Text = "Time: " + MediaPlayer.PlayPosition.TotalSeconds;
            world.Step(Math.Min((float)rendercontext.GameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));

            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);
            //rendercontext.SpriteBatch.DrawString(text, "Time: " + MediaPlayer.PlayPosition.TotalSeconds, new Vector2(470, 85), Color.White);

            //farseer
            //for (int i = 0; i < ground.FixtureList.Count; ++i)
            //{
            //    rendercontext.LineBatch.DrawLineShape(ground.FixtureList[i].Shape, Color.Black);
            //}
        }

        public override void ResetScene()
        {
            pesawat.Reset();
        }
    }
}
