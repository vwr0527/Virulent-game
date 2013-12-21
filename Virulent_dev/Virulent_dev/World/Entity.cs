﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using Virulent_dev.Input;
using Virulent_dev.Graphics;
using System.Diagnostics;

namespace Virulent_dev.World
{
    class Entity
    {
        public Vector2 pos = new Vector2();
        public Vector2 vel = new Vector2();
        public float rot;
        public float rotvel;
        public TimeSpan age;

        public bool dead = false;
        public State state;
        public SpriteElement sprite = new SpriteElement();

        public void LoadContent(ContentManager content)
        {
            if (state != null)
                state.LoadEntityContent(this, content);
        }

        public void Init()
        {
            if (state != null)
                state.InitEntity(this);
        }

        public void Update(GameTime gameTime, InputManager inputMan)
        {
            if (state != null)
                state.UpdateEntity(this, gameTime, inputMan);
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            if (sprite != null)
            {
                state.PositionSprites(this, gameTime);
                recursiveDrawSprite(sprite, graphMan);
            }
        }

        private void recursiveDrawSprite(SpriteElement sprite, GraphicsManager graphMan)
        {
            if (sprite != null)
            {
                graphMan.DrawWorldSprite(sprite);
                recursiveDrawSprite(sprite.linkedSprite, graphMan);
            }
        }

        //sprite copy is performed by EntityManager

        public static void CopyMembers(Entity a, Entity b)
        {
            CopyAllExceptSprite(a, b);
            //SpriteElement.CopyMembers(a.sprite, b.sprite);
        }

        public static Entity CreateCopy(Entity b)
        {
            Entity a = new Entity();
            CopyAllExceptSprite(a, b);
            //a.sprite = SpriteElement.CreateNewCopy(b.sprite);
            return a;
        }

        private static void CopyAllExceptSprite(Entity a, Entity b)
        {
            a.pos = b.pos;
            a.state = b.state;
            a.dead = b.dead;
            a.vel = b.vel;
            a.rot = b.rot;
            a.rotvel = b.rotvel;
            a.age = b.age;
        }

        public void CollideBlock(Block b)
        {
            if (state != null)
                state.CollideBlock(this, b);
        }
    }
}
