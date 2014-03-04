using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Virulent_dev.World.Collision
{
    class Collider
    {
        public LinkedList<Vector2> pts;
        public LinkedListNode<Vector2> cur;
        public Rectangle rect;
        public Vector2 pos;
        public Vector2 vel;

        public Collider()
        {
            pts = new LinkedList<Vector2>();
            cur = pts.First;
        }

        public bool DidCollide(Collider other)
        {
            if (rect.Contains(new Point(other.rect.Left + (int)(other.pos.X - pos.X), other.rect.Top + (int)(other.pos.Y - pos.Y))))
            {
                return true;
            }
            if (rect.Contains(new Point(other.rect.Right + (int)(other.pos.X - pos.X), other.rect.Top + (int)(other.pos.Y - pos.Y))))
            {
                return true;
            }
            if (rect.Contains(new Point(other.rect.Left + (int)(other.pos.X - pos.X), other.rect.Bottom + (int)(other.pos.Y - pos.Y))))
            {
                return true;
            }
            if (rect.Contains(new Point(other.rect.Right + (int)(other.pos.X - pos.X), other.rect.Bottom + (int)(other.pos.Y - pos.Y))))
            {
                return true;
            }
            return false;
        }

        public Vector2 PushOut(Collider other)
        {
            return new Vector2(0, ((float)rect.Top + pos.Y)-((float)other.rect.Bottom + other.pos.Y));
        }

        private bool IsLeft(Vector2 a, Vector2 b, Vector2 c)
        {
            return (b.X - a.X) * (c.Y - a.Y) > (b.Y - a.Y) * (c.X - a.X);
        }
    }
}
