using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarpJam.Tools;
using Microsoft.Xna.Framework;

namespace WarpJam
{
    class PilihLevel:GameScene
    {
        #region assets
        GameSprite bg;
        Vector2 stageDimension;
        GameObject2D invisibleObject;
        SpriteFonts text;
        int xxx = 0;
        #endregion

        public PilihLevel()
            : base("PilihLevel")
        {
            stageDimension = new Vector2(800, 800);
        }

        public override void Initialize()
        {
            base.Initialize();
            bg = new GameSprite("menu\\background");
            bg.Translate(0, 0);
            AddSceneObject(bg);

            text = new SpriteFonts("font\\font");
            text.Translate(520, 50);
            text.Color = Color.Red;
            AddHUDObject(text);

            invisibleObject = new GameObject2D();
            AddSceneObject(invisibleObject);

            CameraManager.getInstance().camera.Focus = invisibleObject;
            CameraManager.getInstance().camera.Position = new Vector2(1000, 1000);
            

        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void Update(RenderContext rendercontext, Microsoft.Xna.Framework.Content.ContentManager contentmanager)
        {
            base.Update(rendercontext, contentmanager);
            Vector2 newPos = invisibleObject.WorldPosition;
            //newPos.X += 5;
            text.Text = xxx.ToString();
            if (xxx == 100)
            {
                invisibleObject.Translate(new Vector2(1000, 0));
            }
            xxx++;
            
            //invisibleObject.Translate(newPos);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);
        }

        public override bool BackPressed()
        {
            return base.BackPressed();
        }
    }
}
