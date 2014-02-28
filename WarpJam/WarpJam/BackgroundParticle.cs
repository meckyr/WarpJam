using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarpJam.Tools;
using ProjectMercury;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WarpJam
{
    class BackgroundParticle : GameObject2D, ObjectWithParticle
    {
        public List<ParticleEffect> particles;

        public List<ProjectMercury.ParticleEffect> Particles
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

        public BackgroundParticle():base()
        {
            particles = new List<ParticleEffect>();
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
                if (emitter.Name.Equals("Fast Beams"))
                {
                    emitter.ParticleTexture = contentManager.Load<Texture2D>("particles//Beam");
                }
                else if (emitter.Name.Equals("Slow Beams"))
                {
                    emitter.ParticleTexture = contentManager.Load<Texture2D>("particles//BeamBlurred");
                }
            }

            particleRenderer.LoadContent(contentManager);
            particles.Add(particleEffect);
        }

        public override void Update(RenderContext renderContext)
        {
            var position = new Vector3(800, 240, 0);
            foreach (ParticleEffect particleEffect in particles)
            {
                particleEffect.Trigger(ref position);
                particleEffect.Update((float)SceneManager.gameTime.ElapsedGameTime.TotalSeconds/2.5f);
            }
            base.Update(renderContext);
        }

        public void DrawParticle(RenderContext renderContext)
        {
            var matrix = Matrix.Identity;
            var cameraPosition = Vector3.Zero;

            foreach (ParticleEffect particleEffect in particles)
            {
                renderContext.particleRenderer.Transformation = Matrix.Identity;
                renderContext.particleRenderer.RenderEffect(particleEffect, ref matrix, ref matrix, ref matrix, ref cameraPosition);
            }
        }
    }
}
