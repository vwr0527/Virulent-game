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
            levels.Add("title", new TitleLevel());
            levels.Add("tutorial", new TutorialLevel());
            currentLevel = levels["title"];
            //currentLevel = levels["tutorial"];
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
            {
                Entity toSpawn = currentLevel.SpawnNext();
                toSpawn.Init();
                entMan.AddEnt(toSpawn);
            }
            if (currentLevel.Victory())
            {
                currentLevel = levels[currentLevel.GetNextLevel()];
            }
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

        public void LoadLevel(String levelName)
        {
            if (levels.ContainsKey(levelName))
            {
                entMan.RemoveAllEnts();
                currentLevel = levels[levelName];
                currentLevel.Init();
            }
        }

        public bool IsInTitleScreen()
        {
            return currentLevel == levels["title"];
        }
    }
}
