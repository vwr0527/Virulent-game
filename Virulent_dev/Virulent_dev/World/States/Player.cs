using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Virulent_dev.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

using Virulent_dev.Input;

namespace Virulent_dev.World.States
{
    class Player : State
    {
        Random rand;
        TimeSpan maxAge;

        public Player()
        {
            rand = new Random();
            maxAge = new TimeSpan(0, 0, 30);
        }

        public override void LoadEntityContent(Entity e, ContentManager content)
        {
            e.sprite = new SpriteElement(content.Load<Texture2D>("char/head"));
            SpriteElement cur = e.sprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/body"));
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/pelvis"));
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/legrt"));
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/legrt"));
        }

        public override void InitEntity(Entity e)
        {
            e.sprite.col = new Color(0, 255, 0);
            e.sprite.scale = 1f;
            e.sprite.linkedSprite.scale = 0.1f;
        }

        public override void UpdateEntity(Entity e, GameTime gameTime, InputManager inputMan)
        {
            e.vel.X += 0.002f * (float)(rand.NextDouble() - 0.5) * (float)(gameTime.ElapsedGameTime.Milliseconds);
            e.vel.Y += 0.005f * (float)(gameTime.ElapsedGameTime.Milliseconds);
            e.pos += e.vel * (float)(gameTime.ElapsedGameTime.Milliseconds) * 0.1f;
            if (e.pos.Y > 200.0f)
            {
                e.pos.Y = 200.0f;
                e.vel.Y *= -0.99f;
            }
            e.age += gameTime.ElapsedGameTime;
            e.sprite.col = new Color(0, 255, 0);
            e.sprite.scale = 1.5f;
            if (e.age > maxAge) e.dead = true;
        }

        public override void PositionSprites(Entity e, GameTime gameTime)
        {
            SpriteElement head = e.sprite;
            SpriteElement body = head.linkedSprite;
            SpriteElement pelvis = body.linkedSprite;
            SpriteElement legrt = pelvis.linkedSprite;
            SpriteElement legrc = legrt.linkedSprite;

            head.pos.Y = e.pos.Y - 15;
            head.pos.X = e.pos.X - 2;
            head.scale = 0.5f;
            head.col = new Color(0.0f,1f, 1.0f);

            body.scale = 0.4f;
            body.pos = e.pos;
            body.col = new Color(0.2f, 1f, .5f);

            pelvis.pos.Y = e.pos.Y + 12f;
            pelvis.pos.X = e.pos.X + 1f;
            pelvis.scale = 0.4f;
            pelvis.col = new Color(0.0f, 1f, 1.0f);

            legrt.pos.Y = e.pos.Y + 23f;
            legrt.pos.X = e.pos.X - 3f;
            legrt.scale = 0.45f;
            legrt.col = new Color(0.0f, 1f, 1.0f);

            legrc.pos.Y = e.pos.Y + 38f;
            legrc.pos.X = e.pos.X - 5f;
            legrc.scale = 0.5f;
            legrc.col = new Color(0.0f, 1f, 1.0f);
        }
    }
}
