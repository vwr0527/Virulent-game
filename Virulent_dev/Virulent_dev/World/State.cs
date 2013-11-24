using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Virulent_dev.Input;
using Virulent_dev.Graphics;

namespace Virulent_dev.World
{
    class State
    {
        public virtual void Init(Entity e)
        {
        }
        public virtual void Update(Entity e, GameTime gameTime, InputManager inputMan)
        {
        }
    }
}
