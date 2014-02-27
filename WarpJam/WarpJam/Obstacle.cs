using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarpJam.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace WarpJam
{
    class Obstacle : GameObject2D
    {
        private GameAnimatedSprite monster, explosion;
        public GameSprite Sprite { get { return monster; } }

        private TimeSpan time;
        private Vector2 position;
        public StateObstacle CurrentState = StateObstacle.Normal;
        public enum StateObstacle
        {
            Normal,
            Destroyed
        }

        public Obstacle(Vector2 pos, TimeSpan timeSpan)
        {
            position = pos;
            time = timeSpan;
        }

        public override void Initialize()
        {
            explosion = new GameAnimatedSprite("level\\obstacledestroyed", 4, 80, new Point(80, 80));
            explosion.Origin = new Vector2(40, 40);
            explosion.Translate(position);
            explosion.CanDraw = false;
            explosion.PlayAnimation(true);

            monster = new GameAnimatedSprite("level\\obstacle", 4, 80, new Point(80, 80));
            monster.Origin = new Vector2(40, 40);
            monster.Translate(position);
            monster.PlayAnimation(true);

            AddChild(explosion);
            AddChild(monster);

            base.Initialize();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            base.LoadContent(contentManager);
        }

        public int CheckCollision()
        {
            if (CurrentState == StateObstacle.Normal)
            {
                if (MediaPlayer.PlayPosition > time - TimeSpan.FromSeconds(0.30) && MediaPlayer.PlayPosition < time + TimeSpan.FromSeconds(0.30))
                {
                    monster.CanDraw = false;
                    explosion.CanDraw = true;

                    if (MediaPlayer.PlayPosition > time - TimeSpan.FromSeconds(0.2) && MediaPlayer.PlayPosition < time + TimeSpan.FromSeconds(0.2))
                    {
                        if (MediaPlayer.PlayPosition > time - TimeSpan.FromSeconds(0.1) && MediaPlayer.PlayPosition < time + TimeSpan.FromSeconds(0.1))
                        {
                            CurrentState = StateObstacle.Destroyed;
                            return 1;
                        }

                        CurrentState = StateObstacle.Destroyed;
                        return 3;
                    }

                    CurrentState = StateObstacle.Destroyed;
                    return 2;
                }
            }
            return 0;
        }

        public override void Update(RenderContext renderContext)
        {
            base.Update(renderContext);
        }

        public override void Draw(RenderContext renderContext)
        {
            base.Draw(renderContext);

            monster.Draw(renderContext);
        }

        public void Reset()
        {
            CurrentState = StateObstacle.Normal;
            monster.CanDraw = true;
            explosion.CanDraw = false;
        }
    }
}
