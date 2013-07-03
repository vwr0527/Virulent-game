using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Virulent_dev
{
    //graphic element is the generic representation of any kind of graphic.
    //graphic elements can 'link to' sprites, polygon, or text.
    //they 'link' instead of contain, because they try not to repeat information.
    class GraphicElement
    {
        public Vector2 pos;
        public static void CopyMembers(GraphicElement subject, GraphicElement target)
        {
            subject.pos.X = target.pos.X;
            subject.pos.Y = target.pos.Y;
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            
        }
    }
}
