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
        private GameSprite bg;
        private Background backdrop;
        private Pesawat pesawat;
        private GameButton fire;
        private SpriteFont text;
        private SoundEffect gunfire;

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

            bg = new GameSprite("level\\background");
            bg.Translate(-400, 0);
            AddSceneObject(bg);

            backdrop = new Background();
            AddHUDObject(backdrop);

            fire = new GameButton("level\\invisbutton", false);
            fire.CanDraw = false;
            fire.Translate(400, 0);
            fire.OnClick += () =>
            {
                gunfire.Play();
            };
            AddSceneObject(fire);

            pesawat = new Pesawat();
            AddSceneObject(pesawat);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);

            gunfire = contentmanager.Load<SoundEffect>("sfx\\gunfire");
            text = contentmanager.Load<SpriteFont>("font\\font");

            // farseer
            world = new World(Vector2.Zero);

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
            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);
            rendercontext.SpriteBatch.DrawString(text, "Time: " + MediaPlayer.PlayPosition.TotalSeconds, new Vector2(470, 85), Color.White);

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
