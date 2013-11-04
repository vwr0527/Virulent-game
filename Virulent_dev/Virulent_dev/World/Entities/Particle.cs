using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Virulent_dev.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Virulent_dev.Input;

namespace Virulent_dev.World.Entities
{
    class Particle : Entity
    {
        SpriteElement particleElement;
        Random rand;
        TimeSpan age;
        TimeSpan maxAge;

        public override void LoadContent(ContentManager content)
        {
            particleElement = new SpriteElement(content.Load<Texture2D>("dot"));
            rand = Entity.StaticRandom;
            age = new TimeSpan();
            maxAge = new TimeSpan(0, 0, 10);
        }

        public override void Update(GameTime gameTime, InputManager inputMan)
        {
            pos.X += 0.01f * (float)(rand.NextDouble() - 0.5);
            pos.Y += 0.01f * (float)(rand.NextDouble() - 0.5);
            age += gameTime.ElapsedGameTime;
            if (age > maxAge) dead = true;
        }

        public override void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            particleElement.pos = pos;
            graphMan.Add(particleElement);
        }
    }
}
