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
    //states now also contain a spriteelement.
    //the spriteelement should not be used to
    //fill the entity's sprite field.

    //states hold on to the sprite information just
    //so they don't have to load it again with
    //loadcontent. they're holding onto it for
    //other spawnpoints that may want to use it down
    //the road. To prevent co-editing of the
    //sprite field, state only gives copies of
    //the sprite.
    class State
    {
        protected SpriteElement sprite;

        public virtual void LoadContent(ContentManager content)
        {
        }
        public SpriteElement GetSprite()
        {
            SpriteElement spriteCopy = new SpriteElement(sprite);
            return spriteCopy;
        }
        public virtual string GetUniqueName()
        {
            return "";
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
    }
}
