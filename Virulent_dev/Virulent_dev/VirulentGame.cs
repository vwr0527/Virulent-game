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

using Virulent_dev.Menu;
using Virulent_dev.World;
using Virulent_dev.Input;
using Virulent_dev.Graphics;
using Virulent_dev.Cinematic;

namespace Virulent_dev
{
    public class VirulentGame : Microsoft.Xna.Framework.Game
    {
        StorageManager storage;
        GraphicsManager graphics;
        MenuManager menu;
        WorldManager world;
        CinematicManager cinema;
        InputManager input;

        bool exit = false;
        bool savegame = false;
        bool paused = false;
        bool menuactive = false;
        bool cinematicactive = true;

        public VirulentGame()
        {
            Content.RootDirectory = "Content";
            Components.Add(new GamerServicesComponent(this));

            storage = new StorageManager();
            graphics = new GraphicsManager(new GraphicsDeviceManager(this));
            input = new InputManager();
            menu = new MenuManager();
            world = new WorldManager();
            cinema = new CinematicManager();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            graphics.LoadContent(Content);
            cinema.LoadContent(Content);
            world.LoadContent(Content);
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

            if (cinematicactive)
            {
                cinema.Update(gameTime, input);
                if (input.StartPressed()) cinematicactive = false;
            }
            else
            {
                if (input.StartPressed()) menuactive = !menuactive;
                if (menuactive)
                {
                    menu.Update(gameTime, input);
                    paused = true;
                }
                else
                {
                    paused = false;
                }

                if (paused)
                {
                    world.PausedUpdate(gameTime, input);
                }
                else
                {
                    world.Update(gameTime, input);
                }
            }

            if (savegame)
            {
                storage.DoSaveRequest(Guide.IsVisible, PlayerIndex.One);
            }

            storage.DoPendingSave();

            if (exit)
            {
                this.Exit();
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            if (cinematicactive) cinema.Draw(gameTime, graphics);
            else
            {
                world.Draw(gameTime, graphics);
                if (menuactive)
                    menu.Draw(gameTime, graphics);
            }
            graphics.DrawAll(gameTime);

            base.Draw(gameTime);
        }
    }
}

//cinematic active  / menu inactive / world inactive | startup!
//cinematic inactive/ menu active   / world demoing  | main menu
//cinematic inactive/ menu inactive / world active   | playing
//cinematic inactive/ menu active   / world paused   | paused menu