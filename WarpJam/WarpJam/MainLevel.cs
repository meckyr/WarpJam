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

namespace WarpJam
{
    class MainLevel : GameScene
    {
        private Background bg;
        private GameButton fire;
        private Texture2D visualizer;
        private GameAnimatedSprite hero, herop;
        private SpriteFont text;
        private SoundEffect gunfire;

        public MainLevel()
            : base("MainLevel")
        {
        }

        public override void Initialize()
        {
            // set game speed
            SceneManager.RenderContext.GameSpeed = 100;
            // enable gesture
            TouchPanel.EnabledGestures = GestureType.VerticalDrag;

            bg = new Background();
            AddSceneObject(bg);

            hero = new GameAnimatedSprite("level\\hero", 8, 80, new Point(58, 50));
            hero.CreateBoundingRect(58, 50, false);
            hero.Origin = new Vector2(29, 25);
            hero.Translate(60, 200);
            hero.PlayAnimation(true);
            AddSceneObject(hero);

            herop = new GameAnimatedSprite("level\\herop", 8, 80, new Point(60, 52));
            herop.CreateBoundingRect(60, 52, false);
            herop.Origin = new Vector2(30, 26);
            herop.Translate(60, 200);
            herop.CanDraw = false;
            herop.PlayAnimation(true);
            AddSceneObject(herop);

            fire = new GameButton("level\\invisbutton", false);
            fire.CanDraw = false;
            fire.Translate(400, 0);
            fire.OnClick += () =>
            {
                gunfire.Play();
                herop.CanDraw = true;
            };
            AddSceneObject(fire);

            base.Initialize();
        }

        private int GetAverage(Point Between, VisualizationData _visData)
        {

            int average = 0;
            for (int i = Between.X; i < Between.Y; i++)
            {
                average += Convert.ToInt32(_visData.Frequencies[i]);
            }
            int diff = Between.Y - Between.X + 1;
            return average / diff;
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);

            gunfire = contentmanager.Load<SoundEffect>("sfx\\gunfire");
            text = contentmanager.Load<SpriteFont>("font\\font");
            visualizer = new Texture2D(SceneManager.RenderContext.GraphicsDevice, 1, 1);
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            // check gesture
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
                hero.Translate(hero.LocalPosition + dragDelta);
                herop.Translate(herop.LocalPosition + dragDelta);

                // Cek limit top-bottom
                var topLimit = 25;
                var bottomLimit = rendercontext.GraphicsDevice.Viewport.Height - 25;

                if (hero.LocalPosition.Y <= topLimit)
                {
                    hero.Translate(60, topLimit);
                    herop.Translate(60, topLimit);
                    SceneManager.Vibrator.Start(TimeSpan.FromMilliseconds(500));
                }

                if (hero.LocalPosition.Y >= bottomLimit)
                {
                    hero.Translate(60, bottomLimit);
                    herop.Translate(60, bottomLimit);
                    SceneManager.Vibrator.Start(TimeSpan.FromMilliseconds(500));
                }
            }

            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);
            rendercontext.SpriteBatch.DrawString(text, "Time: " + MediaPlayer.PlayPosition.TotalSeconds, new Vector2(470, 85), Color.White);
        }

        public override void ResetScene()
        {
            hero.Translate(60, 200);
            herop.Translate(60, 200);
            herop.CanDraw = false;
        }
    }
}
