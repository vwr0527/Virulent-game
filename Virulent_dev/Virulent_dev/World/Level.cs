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
        public virtual void LoadContent(ContentManager content)
        {
        }

        public virtual void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
        }

        public virtual void Update(GameTime gameTime, InputManager inputMan)
        {
        }
    }
}
