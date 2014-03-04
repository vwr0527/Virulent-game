using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Virulent_dev.World.Collision
{
    class Square
    {
        public List<Entity> ents;
        public List<Block> blocks;

        public Square()
        {
            ents = new List<Entity>();
            blocks = new List<Block>();
        }
    }
}
