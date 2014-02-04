using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WarpJam.Tools
{
    class GameButton : GameSprite
    {
        private bool isSpriteSheet;
        private Rectangle? normalRect, pressedRect;
        private bool isPressed;
        private int touchId;

        public event Action OnClick;
        public event Action OnEnter;
        public event Action OnLeave;

        public GameButton(string assetfile, bool isspritesheet)
            : base(assetfile)
        {
            isSpriteSheet = isspritesheet;
        }

        public void BackToNormal()
        {
            DrawRect = normalRect;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            if (isSpriteSheet)
            {
                CreateBoundingRect((int)Width, (int)Height / 2, false);
                normalRect = new Rectangle(0, 0, (int)Width, (int)(Height / 2f));
                pressedRect = new Rectangle(0, (int)(Height / 2f), (int)Width, (int)(Height / 2f));
            }
            else
                CreateBoundingRect((int)Width, (int)Height, false);
        }

        public override void Update(RenderContext renderContext)
        {
            base.Update(renderContext);

            var touchStates = renderContext.TouchPanelState;
            if (!isPressed)
            {
                DrawRect = normalRect;

                foreach (var location in touchStates)
                {
                    if (HitTest(location.Position, false))
                    {
                        isPressed = true;
                        touchId = location.Id;

                        //Entered
                        if (OnEnter != null)
                            OnEnter();
                        DrawRect = pressedRect;
                        break;
                    }
                }
            }
            else
            {
                var location = touchStates.FirstOrDefault(tloc => tloc.Id == touchId);

                if (location == null || !HitTest(location.Position, false))
                {
                    touchId = -1;
                    isPressed = false;

                    //Left
                    if (OnLeave != null)
                        OnLeave();
                }
                else
                {
                    if (location.State == TouchLocationState.Released)
                    {
                        touchId = -1;
                        isPressed = false;

                        //Clicked
                        if (OnClick != null)
                            OnClick();
                    }
                }
            }
        }
    }
}
