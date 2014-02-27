using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarpJam.Tools;
using Microsoft.Xna.Framework;
using ProjectMercury;
using ProjectMercury.Renderers;
using Microsoft.Xna.Framework.Graphics;

namespace WarpJam
{
    class Portal : GameAnimatedSprite, ObjectWithParticle
    {
        private ParticleEffect particleEffect;
        public List<ParticleEffect> particles;

        public List<ParticleEffect> Particles
        {
            get
            {
                return particles;
            }
            set
            {
                value = particles;
            }
        }

        public Portal(string portal_assetfile, int numframes,
            int frameinterval, Point framesize, int framesperrow)
            : base(portal_assetfile, numframes,
                frameinterval, framesize, framesperrow)
        {
            particles = new List<ParticleEffect>();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            base.LoadContent(contentManager);
        }

        public void LoadParticle(Microsoft.Xna.Framework.Content.ContentManager contentManager, SpriteBatchRenderer particleRenderer)
        {
            particleEffect = contentManager.Load<ParticleEffect>("particles//Demo1");

            foreach (var emitter in particleEffect.Emitters)
            {
                if (!emitter.Initialised)
                {
                    emitter.Initialise();
                }
                emitter.ParticleTexture = contentManager.Load<Texture2D>("particles//Star");

            }
            particleRenderer.LoadContent(contentManager);
            particles.Add(particleEffect);
        }

        public override void Initialize()
        {
            base.Initialize();

        }

        public override void Update(Tools.RenderContext renderContext)
        {
            var position = new Vector3(this.WorldPosition.X + this.Width/2, this.WorldPosition.Y, 0);
            particleEffect.Trigger(ref position);
            particleEffect.Update((float) SceneManager.gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(renderContext);
        }

        public override void Draw(Tools.RenderContext renderContext)
        {
            base.Draw(renderContext);
        }

        public void DrawParticle(Tools.RenderContext renderContext)
        {
            var matrix = Matrix.Identity;
            //Vector3 cameraPosition = new Vector3(CameraManager.getInstance().camera.Position.X, CameraManager.getInstance().camera.Position.Y, 0);
            var cameraPosition = Vector3.Zero;
            CameraManager.getInstance().camera.Rotation = 1.56618786f;
            CameraManager.getInstance().camera.Scale = 0.05f;
            renderContext.particleRenderer.Transformation = CameraManager.getInstance().camera.Transform;
            renderContext.particleRenderer.RenderEffect(particleEffect, ref matrix, ref matrix, ref matrix, ref cameraPosition);
        }
    }
}
