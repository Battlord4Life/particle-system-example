using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSystemExample
{
    public class FireworkParticleSystem : ParticleSystem
    {

        Color[] colors = new Color[]
        {
            Color.Fuchsia,
            Color.Red,
            Color.Crimson,
            Color.CadetBlue,
            Color.Aqua,
            Color.HotPink,
            Color.LimeGreen
        };

        Color color;


        public FireworkParticleSystem(Game game, int MaxExplosions) : base(game, MaxExplosions * 25) { }

        protected override void InitializeConstants()
        {
            textureFilename = "particle";

            minNumParticles = 20;
            maxNumParticles = 25;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velcoity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 200);

            var lifetime = RandomHelper.NextFloat(.5f, 1f);

            var accel = -velcoity / lifetime;

            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

            var angularVelcoity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

            var scale = RandomHelper.NextFloat(4, 6);

            p.Initialize(where, velcoity, accel, color, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelcoity, scale: scale);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);

            float normlaizedlifetime = particle.TimeSinceStart / particle.Lifetime;

            var apha = 4 * normlaizedlifetime * (1 - particle.Lifetime);
            particle.Color = Color.White * apha;

            particle.Scale = .75f + .25f * normlaizedlifetime;
        }

        public void PlaceFirework(Vector2 where)
        {
            color = colors[RandomHelper.Next(colors.Length)];
            AddParticles(where);
        }

    }
}
