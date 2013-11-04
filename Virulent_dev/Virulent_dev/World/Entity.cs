using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using Virulent_dev.Input;
using Virulent_dev.Graphics;

namespace Virulent_dev.World
{
    class Entity
    {
        public Vector2 pos = new Vector2();
        protected bool dead = false;
        public static Random StaticRandom = new Random();

        public virtual void LoadContent(ContentManager content)
        {
        }

        public virtual void Update(GameTime gameTime, InputManager inputMan)
        {
        }

        public virtual void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
        }

        public virtual bool IsDead()
        {
            return dead;
        }
    }
}
