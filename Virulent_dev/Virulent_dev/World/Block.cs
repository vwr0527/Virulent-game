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
        public Block(string nameOfTexture)
        {
            textureName = nameOfTexture;
            collider = new Collider();
            collider.rect.Location = new Point(-160, -50);
            collider.rect.Width = 320;
            collider.rect.Height = 100;
        }

        public void LoadContent(ContentManager content)
        {
            sprite = new SpriteElement(content.Load<Texture2D>(textureName));
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            sprite.pos.X = collider.rect.Center.X;
            sprite.pos.Y = collider.rect.Center.Y;
            graphMan.DrawWorldSprite(sprite);
        }

        public void SetPosition(Vector2 pos)
        {
            collider.rect.Location = new Point((int)pos.X - 160, (int)pos.Y - 50);
        }
        public void SetScale(float scale)
        {
            sprite.scale = scale;
            Point oldPos = collider.rect.Center;
            collider.rect.Width = (int)((float)collider.rect.Width * scale);
            collider.rect.Height = (int)((float)collider.rect.Height * scale);
            collider.rect.Location = new Point(oldPos.X - (collider.rect.Width / 2), oldPos.Y - (collider.rect.Height / 2));
        }
        public void SetColor(Color col)
        {
            sprite.col = col;
        }

        public void OnCollide(Entity e)
        {
        }

        public Collider GetCollider()
        {
            return collider;
        }
    }
}
