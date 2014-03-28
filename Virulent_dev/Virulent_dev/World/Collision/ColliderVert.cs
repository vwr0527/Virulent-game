using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Virulent_dev.World.Collision
{
    class ColliderVert
    {
        public Vector2 pos;
        public Boolean active;
        public int index;
        public ColliderVert prev; //pointer
        public ColliderVert next; //pointer

        public static void CopyMembers(ColliderVert subject, ColliderVert target)
        {
            subject.pos = target.pos;
            subject.prev = target.prev;
            subject.next = target.next;
        }
        public static ColliderVert CreateCopy(ColliderVert target)
        {
            ColliderVert copy = new ColliderVert();
            CopyMembers(copy, target);
            return copy;
        }

        public void ResetPointers()
        {
            prev = null;
            next = null;
        }
    }
}
