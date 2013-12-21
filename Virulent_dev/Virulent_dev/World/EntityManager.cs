using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using Virulent_dev.Input;
using Virulent_dev.Graphics;
using System.Diagnostics;

namespace Virulent_dev.World
{
    class EntityManager
    {
        RecycleArray<Entity> entList;
        RecycleArray<SpriteElement> spriteList;

        public EntityManager()
        {
            entList = new RecycleArray<Entity>(Entity.CopyMembers, Entity.CreateCopy);
            spriteList = new RecycleArray<SpriteElement>(SpriteElement.CopyMembers, SpriteElement.CreateCopy);
            entList.SetDataMode(false);
            spriteList.SetDataMode(false);
        }

        public void LoadContent(ContentManager content)
        {
        }

        public void Update(GameTime gameTime, BlockManager blockMan, InputManager inputMan)
        {
            for (int i = 0; i < entList.Capacity(); ++i)
            {
                Entity cur = entList.ElementAt(i);
                if (cur.dead)
                {
                    recursiveDeleteSprite(cur.sprite);
                    cur.sprite = null;
                    entList.DeleteElementAt(i);
                }
                else
                {
                    cur.Update(gameTime, inputMan);
                    blockMan.ProcessCollisions(cur);
                }
            }
        }

        private void recursiveDeleteSprite(SpriteElement spriteElement)
        {
            if (spriteElement != null)
            {
                recursiveDeleteSprite(spriteElement.linkedSprite);
                spriteList.DeleteElement(spriteElement);
            }
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            for (int i = 0; i < entList.Capacity(); ++i)
            {
                Entity cur = entList.ElementAt(i);
                if (!cur.dead)
                {
                    cur.Draw(gameTime, graphMan);
                    //Debug.WriteLine("Entity number " + i + " has " + count + " sprites");
                }
            }
            //Debug.WriteLine(spriteList.Size());
        }

        public Entity AddEnt(Entity entityToAdd)
        {
            Entity added = entList.Add(entityToAdd);
            SpriteElement b = entityToAdd.sprite;
            SpriteElement a;
            //int i = 0;
            if (b != null)
            {
                added.sprite = spriteList.Add(b);
                a = added.sprite;
                //Debug.WriteLine("ADDING" + i);
                //Debug.WriteLine(spriteList.Size());
                //Debug.WriteLine(a.GetHashCode());
                while (b.linkedSprite != null)
                {
                    //Debug.WriteLine("ADDING" + ++i);
                    a.linkedSprite = spriteList.Add(b.linkedSprite);
                    //Debug.WriteLine(spriteList.Size());
                    //Debug.WriteLine(a.linkedSprite.GetHashCode());
                    b = b.linkedSprite;
                    a = a.linkedSprite;
                }
                a.linkedSprite = null;
            }
            added.Init();
            return added;
        }

        public void RemoveAllEnts()
        {
            for (int i = 0; i < entList.Capacity(); ++i)
            {
                entList.ElementAt(i).dead = true;
            }
            spriteList.EmptyAll();
        }
    }
}
