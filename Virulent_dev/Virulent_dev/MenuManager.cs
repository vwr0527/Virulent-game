using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using Virulent_dev.GUIObjects;

namespace Virulent_dev
{
    class MenuManager
    {
        private bool active = false;
        private bool bringUpMenu = false;
        private SpriteElement textStatement;
        private StringBuilder[] txt;
        private Texture2D picture;
        private SpriteFont font;
        private SpriteElement pickle;
        private MenuPage mainmenu;

        public MenuManager()
        {
            mainmenu = new MenuPage();
        }

        public void LoadContent(ContentManager content)
        {
            picture = content.Load<Texture2D>("test");
            font = content.Load<SpriteFont>("SpriteFont1");
            txt = new StringBuilder[10];
            txt[0] = new StringBuilder();
            txt[0].Append("Hello");
            pickle = new SpriteElement(picture);
            textStatement = new SpriteElement(txt[0], font);
        }

        public bool IsActive()
        {
            return active;
        }

        public void Update(GameTime gameTime, InputManager r_inputMan)
        {
            bringUpMenu = r_inputMan.StartPressed();
        }

        public void Draw(GameTime gameTime, GraphicsManager r_graphMan)
        {
            pickle.pos.X = 0.5f;
            pickle.pos.Y = 0.5f;
            pickle.col.A = 200;
            pickle.col.R = 80;
            pickle.col.G = 80;
            pickle.col.B = 80;
            pickle.scale = 4f;
            pickle.rotation += gameTime.ElapsedGameTime.Milliseconds / 3000f;
            r_graphMan.Add(pickle);

            textStatement.scale = 2.5f;
            textStatement.rotation += gameTime.ElapsedGameTime.Milliseconds / -2000f;
            r_graphMan.Add(textStatement);
        }
    }
}
