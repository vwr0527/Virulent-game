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
        private bool init = true;
        private Level currentLevel;
        private Dictionary<String, Level> levels;
        private EntityManager entMan;
        private BlockManager blockMan;

        public WorldManager()
        {
            levels = new Dictionary<string, Level>();
            levels.Add("title", new TitleLevel());
            levels.Add("tutorial", new TutorialLevel());
            currentLevel = levels["title"];

            entMan = new EntityManager();
            blockMan = new BlockManager();
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Level level in levels.Values)
            {
                level.LoadContent(content);
            }
            entMan.LoadContent(content);
        }

        public void Update(GameTime gameTime, InputManager inputMan)
        {
            if (init)
            {
                currentLevel.Init(gameTime);
                init = false;
            }

            while (currentLevel.BlockPending())
            {
                blockMan.AddBlock(currentLevel.GetNextBlock());
            }

            while (currentLevel.EntityPending())
            {
                entMan.AddEnt(currentLevel.GetNextEntity());
            }

            currentLevel.Update(gameTime, inputMan);
            entMan.Update(gameTime, blockMan, inputMan);
            if (currentLevel.EndLevel())
            {
                LoadLevel(currentLevel.GetNextLevel());
            }
        }

        public void LoadLevel(String levelName)
        {
            if (levels.ContainsKey(levelName))
            {
                entMan.RemoveAllEnts();
                blockMan.RemoveAllBlocks();
                currentLevel = levels[levelName];
                init = true;
            }
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            currentLevel.Draw(gameTime, graphMan);
            entMan.Draw(gameTime, graphMan);
            blockMan.Draw(gameTime, graphMan);
        }

        public void Pause()
        {
            paused = true;
        }

        public void Unpause()
        {
            paused = false;
        }

        public bool IsPaused()
        {
            return paused;
        }

        public bool IsInTitleScreen()
        {
            return currentLevel == levels["title"];
        }

        public bool IsPlayingDemo()
        {
            return demo;
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
    }
}
