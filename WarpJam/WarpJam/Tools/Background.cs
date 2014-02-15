using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace WarpJam.Tools
{
    class Background : GameObject2D
    {
        private GameSprite bg;

        private const float mountain_speed = 0.4f;
        private GameSprite mountain;

        private const float cloud_speed = 0.7f;
        private GameSprite cloud;

        public override void Initialize()
        {
            bg = new GameSprite("level\\background");
            AddChild(bg);

            mountain = new GameSprite("level\\mountain");
            AddChild(mountain);

            cloud = new GameSprite("level\\cloud");
            AddChild(cloud);

            base.Initialize();
        }

        public override void Update(RenderContext renderContext)
        {
            // mountain
            // pastiin frame-rate independent
            var objectSpeed = renderContext.GameSpeed * mountain_speed;
            objectSpeed *= (float)renderContext.GameTime.ElapsedGameTime.TotalSeconds;

            var objectPosX = mountain.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 800;

            mountain.Translate(objectPosX, 225);

            // cloud
            objectSpeed = renderContext.GameSpeed * cloud_speed;
            objectSpeed *= (float)renderContext.GameTime.ElapsedGameTime.TotalSeconds;

            objectPosX = cloud.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 800;

            cloud.Translate(objectPosX, 25);

            base.Update(renderContext);
        }
    }
}
