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
using Virulent_dev.World.Collision;

namespace Virulent_dev.World.States
{
    class Player : State
    {
        Random rand;
        TimeSpan maxAge;
        Collider collider;

        public Player()
        {
            rand = new Random();
            maxAge = new TimeSpan(0, 0, 30);
            collider = new Collider();
            collider.rect = new Rectangle(-15, -15, 30, 65);
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
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/head")); //footl
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/legrt"));
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/legrt"));
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/head")); //footr
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/legrt"));
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/legrt"));
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/legrt"));
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/legrt"));
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/shoulder"));
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/shoulder"));
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/shoulder"));
            cur = cur.linkedSprite;
            cur.linkedSprite = new SpriteElement(content.Load<Texture2D>("char/shoulder"));
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

        public override Collider GetCollider(Entity e)
        {
            collider.pos = e.pos;
            collider.vel = e.vel;
            return collider;
        }

        public override void CollideBlock(Entity e, Block b)
        {
            e.vel.Y *= -0.9f;
            e.pos += b.GetCollider().PushOut(collider);
            b.OnCollide(e);
        }

        public override void PositionSprites(Entity e, GameTime gameTime)
        {
            SpriteElement head = e.sprite;
            SpriteElement body = head.linkedSprite;
            SpriteElement pelvis = body.linkedSprite;
            SpriteElement legrt = pelvis.linkedSprite;
            SpriteElement legrc = legrt.linkedSprite;
            SpriteElement footr = legrc.linkedSprite;
            SpriteElement leglt = footr.linkedSprite;
            SpriteElement leglc = leglt.linkedSprite;
            SpriteElement footl = leglc.linkedSprite;
            SpriteElement armul = footl.linkedSprite;
            SpriteElement armll = armul.linkedSprite;
            SpriteElement armur = armll.linkedSprite;
            SpriteElement armlr = armur.linkedSprite;
            SpriteElement shoulderr = armlr.linkedSprite;
            SpriteElement shoulderl = shoulderr.linkedSprite;
            SpriteElement handr = shoulderl.linkedSprite;
            SpriteElement handl = handr.linkedSprite;

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

            footr.pos.Y = e.pos.Y + 46f;
            footr.pos.X = e.pos.X - 5f;
            footr.rotation = 3.14f;
            footr.scale = 0.4f;
            footr.col = new Color(0.0f, 1f, 1.0f);

            leglt.pos.Y = e.pos.Y + 23f;
            leglt.pos.X = e.pos.X + 5f;
            leglt.scale = 0.45f;
            leglt.rotation = -0.15f;
            leglt.col = new Color(0.0f, 1f, 1.0f);

            leglc.pos.Y = e.pos.Y + 38f;
            leglc.pos.X = e.pos.X + 6f;
            leglc.scale = 0.5f;
            leglc.rotation = -0.15f;
            leglc.col = new Color(0.0f, 1f, 1.0f);

            footl.pos.Y = e.pos.Y + 46f;
            footl.pos.X = e.pos.X + 8f;
            footl.rotation = 3.14f;
            footl.scale = 0.4f;
            footl.col = new Color(0.0f, 1f, 1.0f);

            shoulderr.pos.Y = e.pos.Y - 5f;
            shoulderr.pos.X = e.pos.X - 8f;
            shoulderr.rotation = -0.3f;
            shoulderr.scale = 0.33f;
            shoulderr.col = new Color(0.2f, 1f, .5f);

            armur.pos.Y = e.pos.Y + 2f;
            armur.pos.X = e.pos.X - 9f;
            armur.rotation = 0.1f;
            armur.scale = 0.36f;
            armur.col = new Color(0.0f, 1f, 1.0f);

            armlr.pos.Y = e.pos.Y + 13f;
            armlr.pos.X = e.pos.X - 11f;
            armlr.rotation = 0.0f;
            armlr.scale = 0.31f;
            armlr.col = new Color(0.0f, 1f, 1.0f);

            handl.pos.Y = e.pos.Y + 19f;
            handl.pos.X = e.pos.X - 12f;
            handl.rotation = 1.6f;
            handl.scale = 0.22f;
            handl.col = new Color(0.0f, 1f, 1.0f);

            shoulderl.pos.Y = e.pos.Y - 5f;
            shoulderl.pos.X = e.pos.X + 8f;
            shoulderl.rotation = 0.2f;
            shoulderl.scale = 0.33f;
            shoulderl.col = new Color(0.2f, 1f, .5f);

            armul.pos.Y = e.pos.Y + 2f;
            armul.pos.X = e.pos.X + 11f;
            armul.rotation = -0.3f;
            armul.scale = 0.36f;
            armul.col = new Color(0.0f, 1f, 1.0f);

            armll.pos.Y = e.pos.Y + 13f;
            armll.pos.X = e.pos.X + 13f;
            armll.rotation = -0.3f;
            armll.scale = 0.31f;
            armll.col = new Color(0.0f, 1f, 1.0f);

            handr.pos.Y = e.pos.Y + 19f;
            handr.pos.X = e.pos.X + 13f;
            handr.rotation = 1.3f;
            handr.scale = 0.22f;
            handr.col = new Color(0.0f, 1f, 1.0f);
        }
    }
}
