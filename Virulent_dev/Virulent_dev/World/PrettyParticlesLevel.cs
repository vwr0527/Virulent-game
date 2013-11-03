using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Virulent_dev.Graphics;
using Virulent_dev.Input;

namespace Virulent_dev.World
{
    class PrettyParticlesLevel : Level
    {
        SpriteElement bg;

        public override void LoadContent(ContentManager content)
        {
            bg = new SpriteElement(content.Load<Texture2D>("test"));
            bg.scale = 4;
            bg.pos.X = 0.5f;
            bg.pos.Y = 0.5f;
        }

        public override void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            graphMan.Add(bg);
        }

        public override void Update(GameTime gameTime, InputManager inputMan)
        {
            bg.rotation += gameTime.ElapsedGameTime.Milliseconds / 3000f;
        }
    }
}
