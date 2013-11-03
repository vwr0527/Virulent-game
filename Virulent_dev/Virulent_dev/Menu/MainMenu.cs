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
        private SpriteElement el_newgame;
        private SpriteElement el_options;
        private SpriteElement el_quit;
        private SpriteElement cursor;
        private int cursorpos;

        private NewGamePage newGamePage;
        private MenuPage nextPage;

        public override void LoadContent(ContentManager content)
        {
            SpriteFont font = content.Load<SpriteFont>("SquaredDisplay");
            SpriteFont titlefont = content.Load<SpriteFont>("Hyperspace");
            title = new SpriteElement(new StringBuilder("Virulent"), titlefont);
            el_newgame = new SpriteElement(new StringBuilder("New Game"), font);
            el_options = new SpriteElement(new StringBuilder("Options"), font);
            el_quit = new SpriteElement(new StringBuilder("Quit Game"), font);
            cursor = new SpriteElement(content.Load<Texture2D>("cursor"));
            title.pos.X = 0.5f;
            el_newgame.pos.X = 0.5f;
            el_newgame.pos.Y = 0.4f;
            el_options.pos.X = 0.5f;
            el_options.pos.Y = 0.6f;
            el_quit.pos.X = 0.5f;
            el_quit.pos.Y = 0.8f;
            cursor.pos.X = 0.35f;
            cursor.pos.Y = 0.4f;
            cursor.scale = 0.5f;
            cursorpos = 0;

            newGamePage = new NewGamePage(this);
            newGamePage.LoadContent(content);
        }

        public override void Update(GameTime gameTime, InputManager inputMan)
        {
            title.pos.Y = 0.15f + ((float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 300.0) * 0.01f);

            if (inputMan.StartPressed()) exitmenu = true;
            if (inputMan.DownPressed()) cursorpos += 1;
            if (inputMan.UpPressed()) cursorpos -= 1;
            if (cursorpos > 2) cursorpos = 2;
            if (cursorpos < 0) cursorpos = 0;

            cursor.pos.Y = 0.4f + ((float)cursorpos) * 0.2f;
            cursor.pos.X = 0.35f + ((float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 100.0) * 0.005f);

            el_newgame.scale = 1f;
            el_options.scale = 1f;
            el_quit.scale = 1f;

            if (cursorpos == 0)
            {
                el_newgame.scale = 1.1f;
                if (inputMan.EnterPressed())
                {
                    switching = true;
                    nextPage = newGamePage;
                }
            }
            else if (cursorpos == 1)
            {
                el_options.scale = 1.1f;
                if (inputMan.EnterPressed())
                {
                    //switching = true;
                }
            }
            else if (cursorpos == 2)
            {
                el_quit.scale = 1.1f;
                if (inputMan.EnterPressed())
                {
                    exit = true;
                }
            }
        }

        public override void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            graphMan.Add(title);
            graphMan.Add(el_newgame);
            graphMan.Add(el_options);
            graphMan.Add(el_quit);
            graphMan.Add(cursor);
            Debug.WriteLine(cursor.texture);
        }

        public override MenuPage GetNextPage()
        {
            return nextPage;
        }
    }
}
