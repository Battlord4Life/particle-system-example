using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSystemExample
{
    public class PixieParticleSystem : ParticleSystem
    {
        IParticleEmitter _emitter; 

        public PixieParticleSystem(Game game, IParticleEmitter particleEmitter) :base(game, 2000)
        {
            _emitter = particleEmitter;
        }

        protected override void InitializeConstants()
        {
            textureFilename = "circle";

            minNumParticles = 2;
            maxNumParticles = 5;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;


        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var vel = _emitter.Veclocity;

            var acceleration = Vector2.UnitY * 400;

            var scale = RandomHelper.NextFloat(0.1f, .5f);

            var lifetime = RandomHelper.NextFloat(0.1f, 1f);

            p.Initialize(where, vel, acceleration, Color.Goldenrod, scale: scale, lifetime: lifetime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            AddParticles(_emitter.Position);
        }

    }
}
