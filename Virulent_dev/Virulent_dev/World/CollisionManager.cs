using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Virulent_dev.World
{
    class CollisionManager
    {
        List<Entity> ents;
        List<Block> blocks;

        public CollisionManager()
        {
            ents = new List<Entity>();
            blocks = new List<Block>();
        }

        public void RemoveAllBlocks()
        {
            blocks.Clear();
        }

        public void AddBlock(Block addedBlock)
        {
            blocks.Add(addedBlock);
        }

        public void AddEnt(Entity addedEntity)
        {
            ents.Add(addedEntity);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Entity e in ents)
            {
                foreach (Block b in blocks)
                {
                    if (b.DidCollide(e.pos, e.vel))
                    {
                        e.CollideBlock(b);
                    }
                }
            }
            ents.Clear();
        }
    }
}
