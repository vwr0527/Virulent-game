using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Virulent_dev.Graphics;

namespace Virulent_dev.World
{
    class Block
    {
        private SpriteElement sprite;
        private string textureName;
        private Collider collider;
        private Rectangle collisionBox;
        public Block(string nameOfTexture)
        {
            textureName = nameOfTexture;
            collider = new Collider();
            collisionBox.Location = new Point(-160, -50);
            collisionBox.Width = 320;
            collisionBox.Height = 100;
        }

        public void LoadContent(ContentManager content)
        {
            sprite = new SpriteElement(content.Load<Texture2D>(textureName));
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            sprite.pos.X = collisionBox.Center.X;
            sprite.pos.Y = collisionBox.Center.Y;
            graphMan.DrawWorldSprite(sprite);
        }

        public void SetPosition(Vector2 pos)
        {
            collisionBox.Location = new Point((int)pos.X - 160, (int)pos.Y - 50);
        }
        public void SetScale(float scale)
        {
            sprite.scale = scale;
            Point oldPos = collisionBox.Center;
            collisionBox.Width = (int)((float)collisionBox.Width * scale);
            collisionBox.Height = (int)((float)collisionBox.Height * scale);
            collisionBox.Location = new Point(oldPos.X - (collisionBox.Width / 2), oldPos.Y - (collisionBox.Height / 2));

        }
        public void SetColor(Color col)
        {
            sprite.col = col;
        }

        public bool DidCollide(Vector2 pos, Vector2 vel)
        {
            return collisionBox.Contains(new Point((int)pos.X, (int)pos.Y));
        }

        public Vector2 PushOut(Vector2 pos, Vector2 vel)
        {
            return new Vector2(pos.X, collisionBox.Top);
        }
    }
}
