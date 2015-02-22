using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Virulent_dev.World.Collision
{
    //keeps track of which squares each entity is in
    //and holds on to a collider that is associated
    //with the same entity.
    class EntityCollisionInfo
    {
        public List<Square> squares;
        public Entity entity;
        public Collider collider;
        public float collideTime = 1;
        public Vector2 pushOut;

        public EntityCollisionInfo()
        {
            squares = new List<Square>();
        }
        public static void CopyMethod(EntityCollisionInfo dst, EntityCollisionInfo src)
        {
            dst.squares.Clear();
            //Won't ever use this, because CopyMethod will only be used for AddEnt, and it will never start with any squares.
            //for (int i = 0, max = src.squares.Count; i < max; ++i)
            //{
            //    dst.squares.Add(src.squares[i]);
            //}
            dst.entity = src.entity;
        }
        public static EntityCollisionInfo CreateCopyMethod(EntityCollisionInfo src)
        {
            EntityCollisionInfo created = new EntityCollisionInfo();
            CopyMethod(created, src);
            return created;
        }
    }
}
