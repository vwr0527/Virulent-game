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
    class State
    {
        public virtual void LoadEntityContent(Entity e, ContentManager content)
        {
        }
        public virtual void InitEntity(Entity e)
        {
        }
        public virtual void UpdateEntity(Entity e, GameTime gameTime, InputManager inputMan)
        {
        }

        public virtual void PositionSprites(Entity e, GameTime gameTime)
        {
            SpriteElement s = e.sprite;
            s.pos = e.pos;
            while (s.linkedSprite != null)
            {
                s.linkedSprite.pos = e.pos;
                s = s.linkedSprite;
            }
        }

        public virtual void CollideBlock(Entity e, Block b)
        {
        }
    }
}
