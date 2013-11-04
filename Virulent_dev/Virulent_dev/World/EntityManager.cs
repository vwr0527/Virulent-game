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
        List<Entity> ents;
        List<Entity> remove;
        ContentManager c;

        public void LoadContent(ContentManager content)
        {
            ents = new List<Entity>();
            remove = new List<Entity>();
            c = content;
        }

        public void Update(GameTime gameTime, InputManager inputMan)
        {
            foreach (Entity e in ents)
            {
                e.Update(gameTime, inputMan);
                if (e.IsDead())
                {
                    remove.Add(e);
                }
            }
            foreach (Entity e in remove)
            {
                ents.Remove(e);
            }
            remove.Clear();
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            foreach (Entity e in ents)
            {
                e.Draw(gameTime, graphMan);
            }
        }

        public void AddEnt(Entity entity)
        {
            entity.LoadContent(c);
            ents.Add(entity);
        }
    }
}
