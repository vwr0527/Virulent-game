using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using Virulent_dev.Input;
using Virulent_dev.Graphics;

namespace Virulent_dev.World
{
    class EntityManager
    {
        RecycleArray<Entity> entList;

        public EntityManager()
        {
            entList = new RecycleArray<Entity>(Entity.CopyMembers, Entity.CreateCopy);
            entList.SetDataMode(false);
        }

        public void LoadContent(ContentManager content)
        {
        }

        public void Update(GameTime gameTime, InputManager inputMan)
        {
            for (int i = 0; i < entList.Capacity(); ++i)
            {
                if (entList.ElementAt(i).dead)
                {
                    entList.DeleteElementAt(i);
                }
                else
                {
                    entList.ElementAt(i).Update(gameTime, inputMan);
                }
            }
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            for (int i = 0; i < entList.Capacity(); ++i)
            {
                if (!entList.ElementAt(i).dead)
                {
                    entList.ElementAt(i).Draw(gameTime, graphMan);
                }
            }
        }

        public void AddEnt(Entity entity)
        {
            Entity added = entList.Add(entity);
        }
    }
}
