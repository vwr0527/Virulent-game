using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Virulent_dev.Graphics;
using Virulent_dev.Input;

namespace Virulent_dev.Menu
{
    class MainMenu : MenuPage
    {
        private SpriteElement title;
        private SpriteElement el_page1;
        private SpriteElement el_page2;
        private SpriteElement el_page3;
        private SpriteElement cursor;

        public override void LoadContent(ContentManager content)
        {
            SpriteFont font = content.Load<SpriteFont>("SquaredDisplay");
            SpriteFont titlefont = content.Load<SpriteFont>("Hyperspace");
            title = new SpriteElement(new StringBuilder("Virulent"), titlefont);
            el_page1 = new SpriteElement(new StringBuilder("Page 1"), font);
            el_page2 = new SpriteElement(new StringBuilder("Page 2"), font);
            el_page3 = new SpriteElement(new StringBuilder("Page 3"), font);
            title.pos.X = 0.5f;
            el_page1.pos.X = 0.5f;
            el_page1.pos.Y = 0.4f;
            el_page2.pos.X = 0.5f;
            el_page2.pos.Y = 0.6f;
            el_page3.pos.X = 0.5f;
            el_page3.pos.Y = 0.8f;
        }

        public override void Update(GameTime gameTime, InputManager inputMan)
        {
            title.pos.Y = 0.15f + ((float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 300.0) * 0.01f);

            if (inputMan.StartPressed()) exitmenu = true;
        }

        public override void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            graphMan.Add(title);
            graphMan.Add(el_page1);
            graphMan.Add(el_page2);
            graphMan.Add(el_page3);
        }
    }
}
