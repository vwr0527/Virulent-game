using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Virulent_dev.World
{
    class Collider
    {
        LinkedList<Vector2> pts;
        LinkedListNode<Vector2> cur;
        public Collider()
        {
            pts = new LinkedList<Vector2>();
            cur = pts.First;
        }
    }
}
