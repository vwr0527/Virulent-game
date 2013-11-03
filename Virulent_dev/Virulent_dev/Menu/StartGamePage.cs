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
    class StartGamePage : MenuPage
    {
        private SpriteElement el_page1;
        private SpriteElement el_page2;
        private SpriteElement el_page3;
        private SpriteElement el_page4;
        private SpriteElement el_page5;
        private SpriteElement cursor;
        private int cursorpos;

        private MenuPage mainMenu;

        public StartGamePage(MainMenu putMainMenuHere)
        {
            mainMenu = putMainMenuHere;
        }

        public override void LoadContent(ContentManager content)
        {
            SpriteFont font = content.Load<SpriteFont>("SquaredDisplay");
            el_page1 = new SpriteElement(new StringBuilder("Tutorial"), font);
            el_page2 = new SpriteElement(new StringBuilder("New Game"), font);
            el_page3 = new SpriteElement(new StringBuilder("Load Game"), font);
            el_page4 = new SpriteElement(new StringBuilder("Multiplayer"), font);
            el_page5 = new SpriteElement(new StringBuilder("Back"), font);
            cursor = new SpriteElement(content.Load<Texture2D>("cursor"));
            el_page1.pos.X = 0.5f;
            el_page1.pos.Y = 0.3f;
            el_page2.pos.X = 0.5f;
            el_page2.pos.Y = 0.425f;
            el_page3.pos.X = 0.5f;
            el_page3.pos.Y = 0.55f;
            el_page4.pos.X = 0.5f;
            el_page4.pos.Y = 0.675f;
            el_page5.pos.X = 0.5f;
            el_page5.pos.Y = 0.8f;
            cursor.pos.X = 0.35f;
            cursor.pos.Y = 0.4f;
            cursor.scale = 0.5f;
            cursorpos = 0;
        }

        public override void Update(GameTime gameTime, InputManager inputMan)
        {
            if (inputMan.StartPressed()) exitmenu = true;
            if (inputMan.DownPressed()) cursorpos += 1;
            if (inputMan.UpPressed()) cursorpos -= 1;
            if (cursorpos > 4) cursorpos = 4;
            if (cursorpos < 0) cursorpos = 0;

            cursor.pos.Y = 0.3f + ((float)cursorpos) * 0.125f;
            cursor.pos.X = 0.35f + ((float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 100.0) * 0.005f);

            el_page1.scale = 1f;
            el_page2.scale = 1f;
            el_page3.scale = 1f;
            el_page4.scale = 1f;
            el_page5.scale = 1f;

            if (cursorpos == 0)
            {
                el_page1.scale = 1.1f;
                if (inputMan.EnterPressed())
                {
                }
            }
            else if (cursorpos == 1)
            {
                el_page2.scale = 1.1f;
                if (inputMan.EnterPressed())
                {
                }
            }
            else if (cursorpos == 2)
            {
                el_page3.scale = 1.1f;
                if (inputMan.EnterPressed())
                {
                }
            }
            else if (cursorpos == 3)
            {
                el_page4.scale = 1.1f;
                if (inputMan.EnterPressed())
                {
                }
            }
            else if (cursorpos == 4)
            {
                el_page5.scale = 1.1f;
                if (inputMan.EnterPressed())
                {
                    switching = true;
                }
            }
        }

        public override void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            graphMan.Add(el_page1);
            graphMan.Add(el_page2);
            graphMan.Add(el_page3);
            graphMan.Add(el_page4);
            graphMan.Add(el_page5);
            graphMan.Add(cursor);
        }

        public override MenuPage GetNextPage()
        {
            return mainMenu;
        }
    }
}
