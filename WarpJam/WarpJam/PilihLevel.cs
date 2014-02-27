using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarpJam.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using ProjectMercury.Renderers;

namespace WarpJam
{
    class PilihLevel : GameScene
    {
        #region assets
        GameSprite bg;
        GameObject2D invisibleObject;
        SpriteFonts text;
        Rectangle swipeLimit = new Rectangle(400, 240, 2000-800, 2000-480);
        Portal portal;
        #endregion

        public PilihLevel()
            : base("PilihLevel")
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            bg = new GameSprite("pilih level\\bg2000");
            bg.Translate(0, 0);
            AddSceneObject(bg);

            text = new SpriteFonts("font\\font");
            text.Translate(520, 50);
            text.Color = Color.Red;
            AddHUDObject(text);

            invisibleObject = new GameObject2D();
            AddSceneObject(invisibleObject);
            invisibleObject.Translate(1000, 1000);

            portal = new Portal("pilih level\\portal dummy", 1, 5, new Point(200,200), 1);
            AddSceneObject(portal);
            AddObjectWithParticle(portal);
            portal.Translate(200, 200);

            //CameraManager.getInstance().camera.Focus = portal;
            CameraManager.getInstance().camera.IsIgnoreY = false;
            TouchPanel.EnabledGestures = GestureType.FreeDrag;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void LoadParticle(Microsoft.Xna.Framework.Content.ContentManager contentmanager, SpriteBatchRenderer particleRenderer)
        {
            base.LoadParticle(contentmanager, particleRenderer);
            portal.LoadParticle(contentmanager, particleRenderer);
        }

        public override void Update(WarpJam.Tools.RenderContext rendercontext, Microsoft.Xna.Framework.Content.ContentManager contentmanager)
        {
            base.Update(rendercontext, contentmanager);

            detectSwipe();
        }

        public override void Draw(WarpJam.Tools.RenderContext rendercontext)
        {
            base.Draw(rendercontext);
        }

        public override bool BackPressed()
        {
            return base.BackPressed();
        }

        private void detectSwipe()
        {
            if (TouchPanel.IsGestureAvailable)
            {
                while (TouchPanel.IsGestureAvailable)
                {
                    Vector2 dragPos = Vector2.Zero;
                    Vector2 dragDelta = Vector2.Zero;
                    GestureSample gs = TouchPanel.ReadGesture();
                    switch (gs.GestureType)
                    {
                        case GestureType.FreeDrag:
                            {
                                dragPos = gs.Position;
                                dragDelta = gs.Delta;
                                break;
                            }

                    }
                    Vector2 newPos = invisibleObject.WorldPosition;
                    if (swipeLimit.Contains((int)invisibleObject.WorldPosition.X - (int)dragDelta.X, (int)invisibleObject.WorldPosition.Y - (int)dragDelta.Y))
                    {
                        newPos -= dragDelta;
                        invisibleObject.Translate(newPos);
                    }
                }
            }
        }
    }
}
