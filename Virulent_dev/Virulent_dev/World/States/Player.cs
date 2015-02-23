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
using Virulent_dev.World.States.Animations;

namespace Virulent_dev.World.States
{
    class Player : State
    {
        Random rand;
        TimeSpan maxAge;
        Collider collider;

        private Animator anim;

        public Player()
        {
            rand = new Random();
            maxAge = new TimeSpan(0, 0, 30);
            collider = new Collider();
            collider.AddVert(0, -20);
            collider.AddVert(10, 0);
            collider.AddVert(0, 50);
            collider.AddVert(-10, 0);

            anim = new Animator();
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

            anim.CreatePose("standing");
            anim.AddSpriteInfo(-2f, -15f, 0.5f, 0, 0, 1f, 1f);//head
            anim.AddSpriteInfo(0, 0, 0.4f, 0, 0.2f, 1f, 0.5f);//body
            anim.AddSpriteInfo(1f, 12f, 0.4f, 0, 0, 1f, 1f); //pelvis
            anim.AddSpriteInfo(-3f, 23f, 0.45f, 0, 0, 1f, 1f); //leg right thigh
            anim.AddSpriteInfo(-5f, 38f, 0.5f, 0, 0, 1f, 1f); //leg right calf
            anim.AddSpriteInfo(-5f, 46f, 0.4f, 3.14f, 0, 1f, 1f); //foot right
            anim.AddSpriteInfo(5f, 23f, 0.45f, -0.15f, 0, 1f, 1f); //leg left thigh
            anim.AddSpriteInfo(6f, 38f, 0.5f, -0.15f, 0, 1f, 1f); //leg left calf
            anim.AddSpriteInfo(8f, 46f, 0.4f, 3.14f, 0, 1f, 1f); //foot left
            anim.AddSpriteInfo(-9f, 2f, 0.36f, 0.1f, 0f, 1f, 1f); //arm upper right
            anim.AddSpriteInfo(-11f, 13f, 0.31f, 0f, 0f, 1f, 1f); //arm lower right
            anim.AddSpriteInfo(11f, 2f, 0.36f, -0.3f, 0f, 1f, 1f); //arm upper left
            anim.AddSpriteInfo(13f, 13f, 0.31f, -0.3f, 0f, 1f, 1f); //arm lower left
            anim.AddSpriteInfo(-8f, -5f, 0.33f, -0.3f, 0.2f, 1f, 0.5f); //shoulder right
            anim.AddSpriteInfo(8f, -5f, 0.33f, 0.2f, 0.2f, 1f, 0.5f); //shoulder left
            anim.AddSpriteInfo(13f, 19f, 0.22f, 1.3f, 0f, 1f, 1f); //hand right
            anim.AddSpriteInfo(-12f, 19f, 0.22f, 1.6f, 0f, 1f, 1f); //hand left
        }

        public override void InitEntity(Entity e)
        {
            e.sprite.col = new Color(0, 255, 0);
            e.sprite.scale = 1f;
            e.sprite.linkedSprite.scale = 0.1f;
        }

        public override void UpdateEntity(Entity e, GameTime gameTime, InputManager inputMan)
        {
            e.vel.X += 0.005f * (float)(rand.NextDouble() - 0.5) * (float)(gameTime.ElapsedGameTime.Milliseconds);
            e.vel.Y += 0.005f * (float)(gameTime.ElapsedGameTime.Milliseconds);

            if (inputMan.MoveLeftPressed())
            {
                e.vel.X -= 0.01f * (float)(gameTime.ElapsedGameTime.Milliseconds);
            }
            if (inputMan.MoveRightPressed())
            {
                e.vel.X += 0.01f * (float)(gameTime.ElapsedGameTime.Milliseconds);
            }
            if (inputMan.MoveUpPressed())
            {
                e.vel.Y -= 0.01f * (float)(gameTime.ElapsedGameTime.Milliseconds);
            }
            if (inputMan.MoveDownPressed())
            {
                e.vel.Y += 0.01f * (float)(gameTime.ElapsedGameTime.Milliseconds);
            }

            e.pos += e.vel * (float)(gameTime.ElapsedGameTime.Milliseconds) * 0.1f;
            if (e.pos.Y > 200.0f)
            {
                e.pos.Y = 200.0f;
                e.vel.Y *= -0.5f;
            }
            e.age += gameTime.ElapsedGameTime;
            e.sprite.col = new Color(0, 255, 0);
            e.sprite.scale = 1.5f;
            collider.ppos = e.ppos;
            collider.pos = e.pos;
            if (e.age > maxAge) e.dead = true;
        }

        public override Collider GetCollider(Entity e)
        {
            collider.ppos = e.ppos;
            collider.pos = e.pos;
            return collider;
        }

        public override void CollideBlock(Entity e, Block b, float collideTime, Vector2 pushOut)
        {
            e.vel.X = 0;
            e.vel.Y = 0;
            e.pos = e.ppos + ((e.pos - e.ppos) * collideTime);
            e.pos += pushOut * 0.1f;

            collider.pos = e.pos;
            collider.ppos = e.ppos;
            b.OnCollide(e);
            Debug.WriteLine(pushOut + " " + collideTime);
        }

        public override void PositionSprites(Entity e, GameTime gameTime)
        {
            anim.DoPose(e);
        }

        public override void DrawPoly(Entity e, GraphicsManager graphMan, GameTime gameTime)
        {
            collider.Draw(graphMan);
            Camera c = graphMan.GetCamera(0);
            c.pos = e.pos;
        }
    }
}
