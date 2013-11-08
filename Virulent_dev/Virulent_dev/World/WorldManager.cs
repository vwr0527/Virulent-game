using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Virulent_dev.Graphics;
using Virulent_dev.Input;
using Virulent_dev.World.Levels;

namespace Virulent_dev.World
{
    class WorldManager
    {
        private bool paused = false;
        private bool save = false;
        private bool demo = true;
        private Level currentLevel;
        private Dictionary<String, Level> levels;
        private EntityManager entMan;

        public WorldManager()
        {
            levels = new Dictionary<string, Level>();
            levels.Add("pretty particles", new PrettyParticlesLevel());
            currentLevel = levels["pretty particles"];
            entMan = new EntityManager();
        }

        public void LoadContent(ContentManager content)
        {
            foreach (KeyValuePair<String, Level> kvp in levels)
            {
                kvp.Value.LoadContent(content);
            }
            entMan.LoadContent(content);
        }

        public void Update(GameTime gameTime, InputManager inputMan)
        {
            currentLevel.Update(gameTime, inputMan);
            entMan.Update(gameTime, inputMan);
            while (currentLevel.NumPendingSpawns() > 0)
                entMan.AddEnt(currentLevel.SpawnNext());
        }

        public void PausedUpdate(GameTime gameTime, InputManager inputMan)
        {
            if (demo)
            {
                Update(gameTime, inputMan);
            }
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            currentLevel.Draw(gameTime, graphMan);
            entMan.Draw(gameTime, graphMan);
        }

        public void Pause()
        {
            paused = true;
        }

        public void Unpause()
        {
            paused = false;
        }

        public bool SaveGame()
        {
            if (save)
            {
                save = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPaused()
        {
            return paused;
        }
    }
}
