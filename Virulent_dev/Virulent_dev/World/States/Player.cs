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
            base.PositionSprites(e, gameTime);

            SpriteElement head = e.sprite;
            SpriteElement body = head.linkedSprite;
            SpriteElement pelvis = body.linkedSprite;
            SpriteElement legrt = pelvis.linkedSprite;
            head.pos.Y -= 15;
            head.pos.X -= 2;
            head.scale = 0.5f;

            body.scale = 0.4f;
            body.col = Color.Green;

            pelvis.pos.Y += 12f;
            pelvis.pos.X += 1f;
            pelvis.scale = 0.4f;
            pelvis.col = Color.Pink;

            legrt.pos.Y += 23f;
            legrt.pos.X -= 3f;
            legrt.scale = 0.4f;
            legrt.col = Color.Red;
        }
    }
}
