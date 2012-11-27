using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Virulent_dev
{
    class GraphicElement
    {
        public Vector2 pos;
        public static void CopyMembers(GraphicElement subject, GraphicElement target)
        {
            subject.pos.X = target.pos.X;
            subject.pos.Y = target.pos.Y;
        }
    }
}
