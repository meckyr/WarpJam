using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarpJam.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using ProjectMercury;
using Microsoft.Xna.Framework.Graphics;

namespace WarpJam
{
    class Obstacle : GameObject2D, ObjectWithParticle
    {
        private GameAnimatedSprite monster, explosion;
        public GameSprite Sprite { get { return monster; } }

        private TimeSpan time;
        private Vector2 position;
        public StateObstacle CurrentState = StateObstacle.Normal;

        public List<ParticleEffect> particles;
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
            particles = new List<ParticleEffect>();

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
                    var position = new Vector3(this.WorldPosition.X, this.WorldPosition.Y, 0);
                    foreach (ParticleEffect particleEffect in particles)
                    {
                        particleEffect.Trigger(ref position);
                        particleEffect.Update((float)SceneManager.gameTime.ElapsedGameTime.TotalSeconds);
                    }

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

        public List<ParticleEffect> Particles
        {
            get
            {
                return particles;
            }
            set
            {
                particles = value;
            }
        }

        public void LoadParticle(Microsoft.Xna.Framework.Content.ContentManager contentManager, ProjectMercury.Renderers.SpriteBatchRenderer particleRenderer)
        {
            ParticleEffect particleEffect = contentManager.Load<ParticleEffect>("particles//Warp");

            foreach (var emitter in particleEffect.Emitters)
            {
                if (!emitter.Initialised)
                {
                    emitter.Initialise();
                }

                emitter.ParticleTexture = contentManager.Load<Texture2D>("particles//LensFlare");
            }

            particleRenderer.LoadContent(contentManager);
            particles.Add(particleEffect);
        }

        public void DrawParticle(RenderContext renderContext)
        {
            var matrix = Matrix.Identity;
            var cameraPosition = Vector3.Zero;
            foreach (ParticleEffect particleEffect in particles)
            {
                renderContext.particleRenderer.Transformation = CameraManager.getInstance().camera.Transform;
                renderContext.particleRenderer.RenderEffect(particleEffect, ref matrix, ref matrix, ref matrix, ref cameraPosition);
            }
        }
    }
}
