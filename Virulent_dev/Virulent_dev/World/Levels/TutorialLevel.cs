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
    class TutorialLevel : Level
    {
        SpriteElement bg;
        Block brick;
        Entity e;
        TimeSpan respawnTime;
        TimeSpan prevSpawnTime;
        Random rand = new Random();

        int numPendingEntities = 0;
        int numPendingBlocks = 1;

        public override void Init(GameTime gameTime)
        {
            prevSpawnTime = gameTime.TotalGameTime - respawnTime;
            numPendingBlocks = 1;
        }

        public override void LoadContent(ContentManager content)
        {
            brick = new Block("platforms/platform1");
            brick.LoadContent(content);
            brick.SetPosition(new Vector2(0, 150));
            brick.SetScale(0.4f);
            brick.SetColor(new Color(1.0f, 0.1f, 0.0f));

            bg = new SpriteElement(content.Load<Texture2D>("gradient"));
            bg.scale = 5;
            bg.pos.X = 0.5f;
            bg.pos.Y = 0.5f;

            respawnTime = new TimeSpan(0, 0, 0, 0, 1000);
            prevSpawnTime = new TimeSpan();

            e = new Entity();
            e.state = new Player();
            e.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            graphMan.GetCamera(1).rot = 0;
            graphMan.DrawWorldSprite(bg);
        }

        public override void Update(GameTime gameTime, InputManager inputMan)
        {
            //bg.rotation += gameTime.ElapsedGameTime.Milliseconds / 3000f;

            if (gameTime.TotalGameTime - prevSpawnTime >= respawnTime)
            {
                prevSpawnTime = gameTime.TotalGameTime;
                numPendingEntities += 1;
            }
        }

        public override bool EntityPending()
        {
            return numPendingEntities > 0;
        }

        public override Entity GetNextEntity()
        {
            --numPendingEntities;
            return e;
        }

        public override bool BlockPending()
        {
            return numPendingBlocks > 0;
        }

        public override Block GetNextBlock()
        {
            --numPendingBlocks;
            return brick;
        }
    }
}
