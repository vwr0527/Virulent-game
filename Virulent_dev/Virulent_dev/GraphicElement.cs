using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Virulent_dev
{
    //graphicElement contains starting index and ending index for a collection/grouping of verticies
    class GraphicElement
    {
        public Vector2 pos;
        public static void CopyMembers(GraphicElement subject, GraphicElement target)
        {
            subject.pos.X = target.pos.X;
            subject.pos.Y = target.pos.Y;
        }
        public void Draw(GraphicsDevice graphicsDevice)
        {
            
        }
    }
}
