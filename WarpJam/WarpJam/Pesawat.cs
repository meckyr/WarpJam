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

namespace WarpJam
{
    class Pesawat : GameObject2D
    {
        private GameAnimatedSprite pesawat;
        public GameSprite Sprite { get { return pesawat;  } }

        public StatePesawat CurrentState = StatePesawat.Normal;
        public enum StatePesawat
        {
            Normal,
            GoUp,
            GoDown,
            Shielding,
            Destroyed
        }

        public const float Acceleration = 1.5f;
        private const float Threshold = 2.0f;

        public override void Initialize()
        {
            // enable gesture
            TouchPanel.EnabledGestures = GestureType.VerticalDrag;

            pesawat = new GameAnimatedSprite("level\\pesawat", 4, 80, new Point(95, 60));
            pesawat.CreateBoundingRect(58, 50, false);
            pesawat.Origin = new Vector2(29, 25);
            pesawat.Translate(0, 240);
            AddChild(pesawat);

            base.Initialize();
        }

        public void SetState(StatePesawat state)
        {
            CurrentState = state;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
        }

        public override void Update(RenderContext renderContext)
        {
            // Pesawat jalan
            var objectSpeed = renderContext.GameSpeed * Acceleration;
            objectSpeed *= (float)renderContext.GameTime.ElapsedGameTime.TotalSeconds;

            var objectPosX = pesawat.LocalPosition.X + objectSpeed;
            pesawat.Translate(objectPosX, pesawat.LocalPosition.Y);

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

                    // animasi
                    var oldPos = pesawat.LocalPosition.Y;
                    Vector2 nextPosition = pesawat.LocalPosition + (dragDelta * 1.2f);
                    var newPos = nextPosition.Y;

                    var delta = newPos - oldPos;

                    if (delta > 3f)
                        pesawat.CurrentFrame = 1;
                    else if (delta < -3f)
                        pesawat.CurrentFrame = 3;

                    pesawat.Translate(nextPosition);

                    // Cek limit top-bottom
                    var topLimit = pesawat.Height / 2;
                    var bottomLimit = renderContext.GraphicsDevice.Viewport.Height - (pesawat.Height / 2);

                    if (pesawat.LocalPosition.Y <= topLimit)
                    {
                        pesawat.Translate(pesawat.LocalPosition.X, topLimit);
                        SceneManager.Vibrator.Start(TimeSpan.FromMilliseconds(100));
                    }

                    if (pesawat.LocalPosition.Y >= bottomLimit)
                    {
                        pesawat.Translate(pesawat.LocalPosition.X, bottomLimit);
                        SceneManager.Vibrator.Start(TimeSpan.FromMilliseconds(100));
                    }
                }
            }

            base.Update(renderContext);
        }

        public override void Draw(RenderContext renderContext)
        {
            base.Draw(renderContext);

            pesawat.Draw(renderContext);
        }

        public void Reset()
        {
            pesawat.Translate(0, 240);
            CameraManager.getInstance().camera.Focus = pesawat;
            CameraManager.getInstance().camera.IsIgnoreY = true;
            CameraManager.getInstance().camera.SetScreenCenter(4, 2);
        }
    }
}
