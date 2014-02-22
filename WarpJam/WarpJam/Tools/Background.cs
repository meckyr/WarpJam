using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace WarpJam.Tools
{
    class Background : GameObject2D
    {
        

        private const float bg_speed = 5.0f;
        private GameSprite bg1, bg2;

        public override void Initialize()
        {
            bg1 = new GameSprite("level\\background1");
            AddChild(bg1);

            bg2 = new GameSprite("level\\background2");
            bg2.Translate(800, 0);
            AddChild(bg2);

            base.Initialize();
        }

        public override void Update(RenderContext renderContext)
        {
            // pastiin frame-rate independent
            var objectSpeed = renderContext.GameSpeed * bg_speed;
            objectSpeed *= (float)renderContext.GameTime.ElapsedGameTime.TotalSeconds;

            var objectPosX = bg1.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            bg1.Translate(objectPosX, 0);

            objectPosX = bg2.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            bg2.Translate(objectPosX, 0);

            base.Update(renderContext);
        }
    }
}
