using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Virulent_dev.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Virulent_dev.Input;

namespace Virulent_dev.World.States
{
    class Particle : State
    {
        Random rand;
        TimeSpan maxAge;

        public Particle()
        {
            rand = new Random();
            maxAge = new TimeSpan(0, 0, 10);
        }

        public override void Update(Entity e, GameTime gameTime, InputManager inputMan)
        {
            e.pos.X += 0.01f * (float)(rand.NextDouble() - 0.5);
            e.pos.Y += 0.01f * (float)(rand.NextDouble() - 0.5);
            e.age += gameTime.ElapsedGameTime;
            if (e.age > maxAge) e.dead = true;
        }
    }
}
