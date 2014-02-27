using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectMercury;
using ProjectMercury.Renderers;

namespace WarpJam.Tools
{
    interface ObjectWithParticle
    {
        List<ParticleEffect> Particles { get; set; }

        void LoadParticle(Microsoft.Xna.Framework.Content.ContentManager contentManager, SpriteBatchRenderer particleRenderer);

        void DrawParticle(RenderContext renderContext);
    }
}
