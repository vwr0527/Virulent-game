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
        XMLReaderTest xmlReader;
        GamerServicesComponent storageComponent;

        StorageManager persistMan;
        GraphicsManager graphMan;
        GUIManager guiMan;
        WorldManager worldMan;
        InputManager inputMan;


        public Virulent()
        {
            Content.RootDirectory = "Content";

            storageComponent = new GamerServicesComponent(this);//unnecessary?
            this.Components.Add(storageComponent);

            persistMan = new StorageManager(null);
            graphMan = new GraphicsManager(this);
            inputMan = new InputManager();
            guiMan = new GUIManager(inputMan, graphMan);
            worldMan = new WorldManager(inputMan, graphMan);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            graphMan.LoadContent(Content);
            xmlReader = new XMLReaderTest();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            inputMan.Update(gameTime);

            if (worldMan.ExitRequested() || guiMan.ExitRequested())
            {
                this.Exit();
            }

            if (worldMan.SaveRequested() || guiMan.SaveRequested())
            {
                persistMan.DoSaveRequest(Guide.IsVisible, PlayerIndex.One);
            }

            guiMan.Update(gameTime);
            worldMan.Update(gameTime);

            // If a save is pending, save as soon as the
            // storage device is chosen
            persistMan.DoPendingSave();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            worldMan.Draw(gameTime);
            guiMan.Draw(gameTime);
            
            graphMan.DrawAll();

            base.Draw(gameTime);
        }
    }
}
