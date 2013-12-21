using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Virulent_dev.Graphics;

namespace Virulent_dev.World
{
    class BlockManager
    {
        List<Block> blockList;
        public BlockManager()
        {
            blockList = new List<Block>();
        }

        public void AddBlock(Block b)
        {
            blockList.Add(b);
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            foreach (Block b in blockList)
            {
                b.Draw(gameTime, graphMan);
            }
        }

        public void ProcessCollisions(Entity e)
        {
            foreach (Block b in blockList)
            {
                if (b.DidCollide(e.pos, e.vel))
                {
                    e.CollideBlock(b);
                }
            }
        }

        public void RemoveAllBlocks()
        {
            blockList.Clear();
            //blockList.TrimExcess();
        }
    }
}
