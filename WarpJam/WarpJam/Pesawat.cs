using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using WarpJam.Tools;
using Microsoft.Xna.Framework.Input.Touch;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics;
using FarseerPhysics.Common;

namespace WarpJam
{
    class Pesawat : GameObject2D
    {
        private GameAnimatedSprite pesawat, shield;
        public GameSprite Sprite { get { return pesawat;  } }

        public StatePesawat CurrentState = StatePesawat.Normal;
        public enum StatePesawat
        {
            Normal,
            Ready,
            Destroyed
        }

        public const float Acceleration = 2.5f;

        // farseer
        private Body roof;
        private Body ground;
        private World world;
        private Body rectangle;

        public override void Initialize()
        {
            // enable gesture
            TouchPanel.EnabledGestures = GestureType.VerticalDrag;

            shield = new GameAnimatedSprite("level\\shield", 2, 15, new Point(135, 135));
            shield.Origin = new Vector2(67.5f, 67.5f);
            shield.CanDraw = false;
            shield.PlayAnimation(true);

            pesawat = new GameAnimatedSprite("level\\pesawat", 4, 80, new Point(95, 60));
            pesawat.Origin = new Vector2(47.5f, 30);

            pesawat.AddChild(shield);
            AddChild(pesawat);

            base.Initialize();
        }

        public void InitiatePath()
        {
            // farseer
            roof = new Body(world);
            {
                Vertices terrain = new Vertices();
                terrain.Add(new Vector2(6f, 1.8f));
                terrain.Add(new Vector2(16.5f, 1.8f));
                terrain.Add(new Vector2(24.5f, 3.5f));

                for (int i = 0; i < terrain.Count - 1; ++i)
                {
                    FixtureFactory.AttachEdge(terrain[i], terrain[i + 1], roof);
                }
            }

            ground = new Body(world);
            {
                Vertices terrain = new Vertices();
                terrain.Add(new Vector2(6f, 3f));
                terrain.Add(new Vector2(16.5f, 3f));
                terrain.Add(new Vector2(24.5f, 4.7f));

                for (int i = 0; i < terrain.Count - 1; ++i)
                {
                    FixtureFactory.AttachEdge(terrain[i], terrain[i + 1], ground);
                }
            }
        }

        public void InitiatePhysics()
        {
            // farseer
            world = new World(Vector2.Zero);

            InitiatePath();

            rectangle = new Body(world);
            rectangle = BodyFactory.CreateRectangle(world, 0.85f, 0.5f, 8f);
            rectangle.BodyType = BodyType.Dynamic;
            rectangle.Position = new Vector2(0, 2.4f);
            rectangle.IgnoreGravity = true;

            pesawat.Translate(ConvertUnits.ToDisplayUnits(rectangle.Position));

            rectangle.OnCollision += new OnCollisionEventHandler(rectangle_OnCollision);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            InitiatePhysics();
        }

        bool rectangle_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            SceneManager.Vibrator.Start(TimeSpan.FromMilliseconds(100));
            return true;
        }

        public override void Update(RenderContext renderContext)
        {
            // check shield
            if (!shield.IsPlaying)
                shield.CanDraw = false;

            // Pesawat jalan
            var objectSpeed = renderContext.GameSpeed * Acceleration;
            objectSpeed *= (float)renderContext.GameTime.ElapsedGameTime.TotalSeconds;

            //rectangle.ResetDynamics();
            //rectangle.ApplyForce(new Vector2(90, 20f));
            var objectPosX = ConvertUnits.ToDisplayUnits(rectangle.Position.X) + objectSpeed;
            rectangle.Position = new Vector2(ConvertUnits.ToSimUnits(objectPosX), rectangle.Position.Y);
            pesawat.Translate(ConvertUnits.ToDisplayUnits(rectangle.Position));

            // check gesture
            if (!TouchPanel.IsGestureAvailable)
                pesawat.CurrentFrame = 0;
            else
            {
                while (TouchPanel.IsGestureAvailable)
                {
                    Vector2 dragPos = Vector2.Zero;
                    Vector2 dragDelta = Vector2.Zero;
                    GestureSample gs = TouchPanel.ReadGesture();
                    switch (gs.GestureType)
                    {
                        case GestureType.VerticalDrag:
                            dragPos = gs.Position;
                            dragDelta = gs.Delta;
                            break;
                    }

                    if (CurrentState == StatePesawat.Ready)
                    {
                        // animasi
                        var oldPos = ConvertUnits.ToDisplayUnits(rectangle.Position.Y);
                        Vector2 nextPosition = ConvertUnits.ToDisplayUnits(rectangle.Position) + (dragDelta * 1.2f);
                        var newPos = nextPosition.Y;

                        var delta = newPos - oldPos;

                        if (delta > 2.5f)
                            pesawat.CurrentFrame = 1;
                        else if (delta < -2.5f)
                            pesawat.CurrentFrame = 3;

                        rectangle.Position = ConvertUnits.ToSimUnits(nextPosition);
                        pesawat.Translate(ConvertUnits.ToDisplayUnits(rectangle.Position));
                    }
                }
            }

            world.Step(Math.Min((float)renderContext.GameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));

            base.Update(renderContext);
        }

        public override void Draw(RenderContext renderContext)
        {
            base.Draw(renderContext);

            pesawat.Draw(renderContext);
        }

        public void Reset()
        {
            InitiatePhysics();
            shield.CanDraw = false;
            CameraManager.getInstance().camera.Focus = pesawat;
            CameraManager.getInstance().camera.IsIgnoreY = true;
            CameraManager.getInstance().camera.SetScreenCenter(4, 2);
        }

        public void ShieldUp()
        {
            shield.CanDraw = true;
            shield.PlayAnimation(false);
        }
    }
}
