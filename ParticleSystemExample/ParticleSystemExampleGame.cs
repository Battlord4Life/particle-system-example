using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParticleSystemExample
{
    /// <summary>
    /// An example game demonstrating the use of particle systems
    /// </summary>
    public class ParticleSystemExampleGame : Game, IParticleEmitter
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ExplosionParticleSystem _explode;
        private FireworkParticleSystem _firework;
        private MouseState _priorMS;

        public Vector2 Position { get; set; }

        public Vector2 Veclocity { get; set; }

        /// <summary>
        /// Constructs an instance of the game
        /// </summary>
        public ParticleSystemExampleGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            RainParticleSystem rain = new(this, new(100, -20, 500, 10));
            Components.Add(rain);
            _explode = new(this, 20);
            Components.Add(_explode);
            _firework = new(this, 20);
            Components.Add(_firework);

            PixieParticleSystem pixie = new(this, this);
            Components.Add(pixie);

            base.Initialize();
        }

        /// <summary>
        /// Loads the game content
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// Updates the game.  Called every frame of the game loop.
        /// </summary>
        /// <param name="gameTime">The time in the game</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState currentMS = Mouse.GetState();
            
            Vector2 MousePS = new(currentMS.X, currentMS.Y);

            if(currentMS.LeftButton == ButtonState.Pressed && _priorMS.LeftButton != ButtonState.Pressed)
            {
                _explode.PlaceExplosions(MousePS);
            }

            if (currentMS.RightButton == ButtonState.Pressed && _priorMS.RightButton != ButtonState.Pressed)
            {
                _firework.PlaceFirework(MousePS);
            }

            Veclocity = MousePS - Position;
            Position = MousePS;


            _priorMS = currentMS;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game.  Called every frame of the game loop.
        /// </summary>
        /// <param name="gameTime">The time in the game</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
