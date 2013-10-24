using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Virulent_dev.Graphics;
using Virulent_dev.Input;

namespace Virulent_dev.Menu
{
    class MainMenu
    {
        private SpriteElement textStatement;
        private StringBuilder[] txt;
        private SpriteFont font;

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("SpriteFont1");
            txt = new StringBuilder[10];
            txt[0] = new StringBuilder();
            txt[0].Append("Hello");
            textStatement = new SpriteElement(txt[0], font);
            textStatement.pos.X = 0.5f;
            textStatement.scale = 2.5f;
        }

        public void Update(GameTime gameTime, InputManager inputMan)
        {
            textStatement.pos.Y = 0.1f + ((float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 300.0) * 0.01f);
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            graphMan.Add(textStatement);
        }
    }
}
