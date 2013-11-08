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
using Virulent_dev.World.States;

namespace Virulent_dev.World.Levels
{
    class PrettyParticlesLevel : Level
    {
        SpriteElement bg;
        Entity e;
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

            e = new Entity();
            e.state = new Particle();
            e.sprite = new SpriteElement(content.Load<Texture2D>("dot"));
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

            if (numPendingSpawns == 4)
            {
                e.pos.X = 0.1f;
                e.pos.Y = 0.1f;
            }
            if (numPendingSpawns == 3)
            {
                e.pos.X = 0.9f;
                e.pos.Y = 0.1f;
            }
            if (numPendingSpawns == 2)
            {
                e.pos.X = 0.1f;
                e.pos.Y = 0.9f;
            }
            if (numPendingSpawns == 1)
            {
                e.pos.X = 0.9f;
                e.pos.Y = 0.9f;
            }
            --numPendingSpawns;
            return e;
        }
    }
}
