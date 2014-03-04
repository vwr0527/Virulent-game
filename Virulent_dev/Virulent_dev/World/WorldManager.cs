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
using Virulent_dev.World.Collision;

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
        private CollisionManager collideMan;

        public WorldManager()
        {
            levels = new Dictionary<string, Level>();
            levels.Add("title", new TitleLevel());
            levels.Add("tutorial", new TutorialLevel());
            currentLevel = levels["title"];

            entMan = new EntityManager();
            blockMan = new BlockManager();
            collideMan = new CollisionManager();
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Level level in levels.Values)
            {
                level.LoadContent(content);
            }
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
                Block addedBlock = currentLevel.GetNextBlock();
                blockMan.AddBlock(addedBlock);

                if (addedBlock.GetCollider() != null)
                    collideMan.AddBlock(addedBlock);
            }

            while (currentLevel.EntityPending())
            {
                Entity addedEnt = currentLevel.GetNextEntity();
                entMan.AddEnt(addedEnt);

                //the equivilent collideMan.AddEnt is in entMan.Update
            }

            currentLevel.Update(gameTime, inputMan);
            entMan.Update(gameTime, inputMan, collideMan);
            collideMan.Update(gameTime);

            if (currentLevel.LevelEnded())
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
                collideMan.RemoveAllBlocks();
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
