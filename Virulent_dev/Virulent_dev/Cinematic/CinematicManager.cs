using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Virulent_dev
{
    class CinematicManager
    {
        SpriteElement bg;
        SpriteElement thing;

        public void LoadContent(ContentManager content)
        {
            bg = new SpriteElement(content.Load<Texture2D>("placeholder_cinematic_background"));
            bg.pos.X = 0.5f;
            bg.pos.Y = 0.5f;
            thing = new SpriteElement(content.Load<Texture2D>("placeholder_button_test"));
            thing.pos.X = 0.5f;
            thing.pos.Y = 0.5f;
        }

        public void Update(GameTime gameTime, InputManager input)
        {
        }

        public void Draw(GameTime gameTime, GraphicsManager graphics)
        {
            graphics.Add(bg);
            graphics.Add(thing);
        }
    }
}
