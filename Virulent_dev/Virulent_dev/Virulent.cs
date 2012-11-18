using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Media;

namespace Virulent_dev
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Virulent : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        XMLReaderTest xmlReader;
        GamerServicesComponent storageComponent;

        PersistantStorageManager persistMan;
        GraphicsDrawingManager graphMan;


        public Virulent()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            storageComponent = new GamerServicesComponent(this);
            this.Components.Add(storageComponent);

            persistMan = new PersistantStorageManager(null);
            graphMan = new GraphicsDrawingManager();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            xmlReader = new XMLReaderTest();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        GamePadState currentState;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GamePadState previousState = currentState;
            currentState = GamePad.GetState(PlayerIndex.One);
            // Allows the default game to exit on Xbox 360 and Windows
            if (currentState.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if ((currentState.Buttons.A == ButtonState.Pressed) &&
                (previousState.Buttons.A == ButtonState.Released))
            {
                persistMan.DoSaveRequest(Guide.IsVisible, PlayerIndex.One);
            }

            // If a save is pending, save as soon as the
            // storage device is chosen
            persistMan.DoPendingSave();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            graphMan.DrawAll(GraphicsDevice, spriteBatch, spriteFont);

            base.Draw(gameTime);
        }
    }
}
