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
    public class Virulent : Microsoft.Xna.Framework.Game
    {
        PersistanceManager persist;
        GraphicsManager graphics;
        MenuManager menu;
        WorldManager world;
        InputManager input;

        private bool exit = false;
        private bool savegame = false;
        private bool paused = false;
        private bool menuactive = false;

        public Virulent()
        {
            Content.RootDirectory = "Content";
            Components.Add(new GamerServicesComponent(this));

            persist = new PersistanceManager();
            graphics = new GraphicsManager(new GraphicsDeviceManager(this));
            input = new InputManager();
            menu = new MenuManager();
            world = new WorldManager();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            graphics.LoadContent(Content);
            menu.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            input.Update(gameTime);
            exit = input.IsBackPressed();
            savegame = input.SKeyPressed();

            if (exit)
            {
                this.Exit();
            }

            if (savegame)
            {
                //TODO: multiple players
                persist.DoSaveRequest(Guide.IsVisible, PlayerIndex.One);
            }

            if (paused)
            {
                world.PausedUpdate(gameTime, input);
            }
            else
            {
                world.Update(gameTime, input);
            }

            if (menuactive)
            {
                menu.Update(gameTime, input);
            }
            else
            {
            }

            persist.DoPendingSave();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            world.Draw(gameTime, graphics);
            menu.Draw(gameTime, graphics);

            graphics.DrawAll(gameTime);

            base.Draw(gameTime);
        }
    }
}
