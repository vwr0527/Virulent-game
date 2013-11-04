using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Virulent_dev.Graphics;
using Virulent_dev.Input;
using Virulent_dev.World.Entities;

namespace Virulent_dev.World
{
    class PrettyParticlesLevel : Level
    {
        SpriteElement bg;

        TimeSpan respawnTime;
        TimeSpan prevSpawnTime;

        public override void LoadContent(ContentManager content)
        {
            bg = new SpriteElement(content.Load<Texture2D>("test"));
            bg.scale = 4;
            bg.pos.X = 0.5f;
            bg.pos.Y = 0.5f;

            respawnTime = new TimeSpan(0, 0, 3);
            prevSpawnTime = new TimeSpan();
        }

        public override void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            graphMan.Add(bg);
        }

        public override void Update(GameTime gameTime, InputManager inputMan)
        {
            bg.rotation += gameTime.ElapsedGameTime.Milliseconds / 3000f;

            if (gameTime.TotalGameTime - prevSpawnTime >= respawnTime)
            {
                prevSpawnTime += respawnTime;
                SpawnThings();
            }
        }

        private void SpawnThings()
        {
            numPendingSpawns += 4;
        }

        public override Entity SpawnNext()
        {
            if (numPendingSpawns == 0) return null;

            Particle p = new Particle();
            if (numPendingSpawns == 4)
            {
                p.pos.X = 0.1f;
                p.pos.Y = 0.1f;
            }
            if (numPendingSpawns == 3)
            {
                p.pos.X = 0.9f;
                p.pos.Y = 0.1f;
            }
            if (numPendingSpawns == 2)
            {
                p.pos.X = 0.1f;
                p.pos.Y = 0.9f;
            }
            if (numPendingSpawns == 1)
            {
                p.pos.X = 0.9f;
                p.pos.Y = 0.9f;
            }
            --numPendingSpawns;
            return p;
        }
    }
}
