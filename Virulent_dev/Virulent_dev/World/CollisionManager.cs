using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Virulent_dev.World
{
    //each square has blocklist and entlist
    //each ent has squarelist
    class CollisionManager
    {
        SquareManager sqrMan;
        RecycleArray<EntitySquareAssociation> entsqrs;
        EntitySquareAssociation addedEntSqrTemp;

        List<Entity> collideAgainstEnts;
        List<Block> collideAgainstBlocks;

        public CollisionManager()
        {
            sqrMan = new SquareManager();
            entsqrs = new RecycleArray<EntitySquareAssociation>(EntitySquareAssociation.CopyMethod, EntitySquareAssociation.CreateCopyMethod);
            entsqrs.SetDataMode(false);
            addedEntSqrTemp = new EntitySquareAssociation();

            collideAgainstEnts = new List<Entity>();
            collideAgainstBlocks = new List<Block>();
        }

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
            //entities have been added, entities and squares have been paired up.
            //blocks are already inside squares.
            //collide all entities with blocks within their squares.
            for(int i = 0, maxEnts = entsqrs.Size(); i < maxEnts; ++i)
            {
                EntitySquareAssociation e = entsqrs.ElementAt(i);
                if (e == null)
                {
                    Debug.WriteLine("CollisionManager Update(): got a null entity in the middle when it should be all contiguous.");
                    continue;
                }
                for (int j = 0, maxSqrs = e.squares.Count; j < maxSqrs; ++j)
                {
                    Square sqr = e.squares.ElementAt(j);

                    //first square doesnt need to check for duplicates
                    //any more squares than 1, it needs to check for duplicates
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
                    {
                        foreach (Block b in sqr.blocks)
                        {
                            if (!collideAgainstBlocks.Contains(b))
                                collideAgainstBlocks.Add(b);
                        }
                        foreach (Entity collideEntity in sqr.ents)
                        {
                            if (!collideAgainstEnts.Contains(collideEntity))
                                collideAgainstEnts.Add(collideEntity);
                        }
                    }

                    //it can begin to clean up here. remove references to itself from the squares it references.
                    //this prevents collisions from being performed twice, once when a->b, another when b->a
                    sqr.ents.Remove(e.entity);
                }
                //now it has the info it needs. it can do collision detection
                foreach (Block b in collideAgainstBlocks)
                {
                    if (b.DidCollide(e.entity.pos, e.entity.vel))
                    {
                        e.entity.CollideBlock(b);
                    }
                }
                foreach (Entity entCollide in collideAgainstEnts)
                {
                    //TODO: Entity - Entity collision
                }
                //clean up
                collideAgainstEnts.Clear();
                collideAgainstBlocks.Clear();
            }
            entsqrs.EmptyAll();
        }

        public void DeleteAllEnts()
        {
            entsqrs.DeleteAll();
            collideAgainstBlocks.Clear();
            collideAgainstBlocks.TrimExcess();
            collideAgainstEnts.Clear();
            collideAgainstEnts.TrimExcess();
        }
    }

    class SquareManager
    {
        public List<Square> squares;
        public Vector2 topLeft;
        public Vector2 size;
        public int columns;
        public int rows;

        public SquareManager()
        {
            columns = 1;
            rows = 1;
            squares = new List<Square>();
            squares.Add(new Square());
            topLeft = new Vector2(-1, -1);
            size = new Vector2(2, 2);
        }

        public void RemoveAllBlocks()
        {
            foreach (Square s in squares)
            {
                s.blocks.Clear();
                s.blocks.TrimExcess();
            }
        }

        // TODO:
        public void AddBlock(Block addedBlock)
        {
            Square square = squares.ElementAt(0);
            if (square != null)
            {
                square.blocks.Add(addedBlock);
            }
        }

        // TODO:
        public void AddEntity(EntitySquareAssociation addedEntitySquare)
        {
            Square square = squares.ElementAt(0);
            if (square != null)
            {
                square.ents.Add(addedEntitySquare.entity);
                addedEntitySquare.squares.Add(square);
            }
        }
    }

    class Square
    {
        public List<Entity> ents;
        public List<Block> blocks;

        public Square()
        {
            ents = new List<Entity>();
            blocks = new List<Block>();
        }
    }

    class EntitySquareAssociation
    {
        public List<Square> squares;
        public Entity entity;

        public EntitySquareAssociation()
        {
            squares = new List<Square>();
        }
        public static void CopyMethod(EntitySquareAssociation dst, EntitySquareAssociation src)
        {
            dst.squares.Clear();
            //Won't ever use this, because CopyMethod will only be used for AddEnt, and it will never start with any squares.
            //for (int i = 0, max = src.squares.Count; i < max; ++i)
            //{
            //    dst.squares.Add(src.squares[i]);
            //}
            dst.entity = src.entity;
        }
        public static EntitySquareAssociation CreateCopyMethod(EntitySquareAssociation src)
        {
            EntitySquareAssociation created = new EntitySquareAssociation();
            CopyMethod(created, src);
            return created;
        }
    }
}
