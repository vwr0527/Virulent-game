using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using Virulent_dev.Graphics;
using Virulent_dev.Input;

namespace Virulent_dev.World
{
    class Level
    {
        protected int numPendingSpawns = 0;

        public virtual void LoadContent(ContentManager content)
        {
        }

        public virtual void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
        }

        public virtual void Update(GameTime gameTime, InputManager inputMan)
        {
        }

        public virtual int NumPendingSpawns()
        {
            return numPendingSpawns;
        }

        public virtual Entity SpawnNext()
        {
            return null;
        }

        public virtual bool Victory()
        {
            return false;
        }

        public virtual bool Failure()
        {
            return false;
        }
    }
}
