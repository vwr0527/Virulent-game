using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Virulent_dev.World.Collision
{
    //each square has blocklist and entlist
    //each ent has squarelist
    class CollisionManager
    {
        SquareManager sqrMan;
        RecycleArray<EntityCollisionInfo> entsqrs;
        EntityCollisionInfo addedEntSqrTemp;

        List<Entity> collideAgainstEnts;
        List<Block> collideAgainstBlocks;
        List<Entity> eliminateEnts;
        List<Block> eliminateBlocks;

        CollisionVertsHolder colliderVerts;
        List<Collider> colliderList;

        public CollisionManager()
        {
            sqrMan = new SquareManager();
            entsqrs = new RecycleArray<EntityCollisionInfo>(EntityCollisionInfo.CopyMethod, EntityCollisionInfo.CreateCopyMethod);
            entsqrs.SetDataMode(false);
            addedEntSqrTemp = new EntityCollisionInfo();

            collideAgainstEnts = new List<Entity>();
            collideAgainstBlocks = new List<Block>();
            eliminateEnts = new List<Entity>();
            eliminateBlocks = new List<Block>();

            colliderVerts = new CollisionVertsHolder();
            colliderList = new List<Collider>();
        }

        //only called on worldmanager loadlevel()
        public void RemoveAllBlocks()
        {
            sqrMan.RemoveAllBlocks();
            for (int i = 0; i < entsqrs.Size(); ++i)
            {
                entsqrs.ElementAt(i).squares.Clear();
                entsqrs.ElementAt(i).squares.TrimExcess();
            }
        }

        public void AddBlock(Block addedBlock)
        {
            sqrMan.AddBlock(addedBlock);
        }

        public void AddEnt(Entity addedEntity)
        {
            addedEntSqrTemp.entity = addedEntity;
            //adds an entity to the appropriate Square(s)
            sqrMan.AddEntity(entsqrs.Add(addedEntSqrTemp));
        }

        public void Update(GameTime gameTime)
        {
            //entities have been added,
            //blocks and entities are already inside squares.
            //collide all entities with blocks within their squares.
            //  slide along surfaces
            //  second surface hit = no slide, sticky bounce
            //collide all entities with entities within their squares.
            //  naive collisions = gather all likely collisions
            //  eliminate collisions that won't happen (a->b, b->c)

            ///////////////////////////////////////////////////////////////////
            //  For each entity
            ///////////////////////////////////////////////////////////////////
            for(int i = 0, maxEnts = entsqrs.Size(); i < maxEnts; ++i)
            {
                EntityCollisionInfo e = entsqrs.ElementAt(i);

                //  Add other entities and block occupying the same square to "collideAgainst..." lists
                AddToCollideAgainst(e);

                //Collide against the things you added

                //first eliminate all non-"likely" collisions (collisions that will happen if nothing ever stops and goes thru eachother)
                DoNaiveElimination(e);

                //then eliminate collisions that will not happen (because of prior collisions with other objects)
                DoAdvancedElimination();

                //then, perform collision actions on the remaining colliders
                foreach (Block b in collideAgainstBlocks)
                {
                    e.entity.CollideBlock(b);
                }
                foreach (Entity e2 in collideAgainstEnts)
                {
                    e.entity.CollideEntity(e2);
                }

                //ready for the next entity to use
                collideAgainstEnts.Clear();
                collideAgainstBlocks.Clear();
                eliminateEnts.Clear();
                eliminateBlocks.Clear();

                ////////////////////////////////////////////////////////////////


            }

            //Finished collision detection for all entities.
            //reset the Entity-Squares temporary holder.
            entsqrs.EmptyAll();
        }


        private void DeleteAllEnts()
        {
            entsqrs.DeleteAll();
            collideAgainstBlocks.Clear();
            collideAgainstBlocks.TrimExcess();
            collideAgainstEnts.Clear();
            collideAgainstEnts.TrimExcess();
        }

        //////////////////////////////////////////////////////
        //  Add other entities and block occupying the same square
        //////////////////////////////////////////////////////
        private void AddToCollideAgainst(EntityCollisionInfo e) 
        {
            //
            //  for each square touching to the entity...
            //
            for (int j = 0, maxSqrs = e.squares.Count; j < maxSqrs; ++j)
            {
                Square sqr = e.squares.ElementAt(j);

                //first square doesnt need to check for duplicates
                //any more squares than 1, it needs to check for duplicates

                //          First square
                if (j == 0)
                {
                    foreach (Block b in sqr.blocks)
                    {
                        collideAgainstBlocks.Add(b);
                    }
                    foreach (Entity collideEntity in sqr.ents)
                    {
                        collideAgainstEnts.Add(collideEntity);
                    }
                }
                else
                //      All squares after the first
                {
                    foreach (Block b in sqr.blocks)
                    {
                        //add if not duplicate
                        if (!collideAgainstBlocks.Contains(b))
                            collideAgainstBlocks.Add(b);
                    }
                    foreach (Entity collideEntity in sqr.ents)
                    {
                        //add if not duplicate
                        if (!collideAgainstEnts.Contains(collideEntity))
                            collideAgainstEnts.Add(collideEntity);
                    }
                }


                //remove references to itself from the squares it references.
                //this prevents collisions from being performed twice, once when a->b, another when b->a
                sqr.ents.Remove(e.entity);
            }
        }

        //Works by getting rid of entities from the "to collide" list.
        //Detects if it currently is overlapping
        //another entity or or block, or has intersected another
        //entity or block to get here. If it has collided, don't
        //do anything; if it hasn't, then remove it from the "to collide" list.
        private void DoNaiveElimination(EntityCollisionInfo e)
        {
            Collider entityCollider = e.entity.GetCollider();
            foreach (Entity e2 in collideAgainstEnts)
            {
                if (!EntityEntityCollision(e2.GetCollider(), entityCollider)) //needs to collide both ways. e2->e1 and e1->e2. find smallest collidetime
                {
                    eliminateEnts.Add(e2);
                }
            }
            entityCollider = e.entity.GetCollider(); //unknown why this fixes weird bug
            foreach (Block b in collideAgainstBlocks)
            {
                if (!b.GetCollider().DidCollide(entityCollider))
                {
                    eliminateBlocks.Add(b);
                }
            }
            foreach (Entity e2 in eliminateEnts)
            {
                collideAgainstEnts.Remove(e2);
            }
            foreach (Block b in eliminateBlocks)
            {
                collideAgainstBlocks.Remove(b);
            }

        }

        private bool EntityEntityCollision(Collider otherCollider, Collider entityCollider)
        {
            return otherCollider.DidCollide(entityCollider);
        }

        //TODO:
        //then eliminate collisions that will not happen (because of prior collisions with other objects)
        private void DoAdvancedElimination()
        {
        }
    }
}
