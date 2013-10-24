using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

using Virulent_dev.Graphics;
using Virulent_dev.Input;

namespace Virulent_dev.Menu
{
    class MenuManager
    {
        private MainMenu mainmenu;

        public MenuManager()
        {
            mainmenu = new MainMenu();
        }

        public void LoadContent(ContentManager content)
        {
            mainmenu.LoadContent(content);
        }

        public void Update(GameTime gameTime, InputManager inputMan)
        {
            mainmenu.Update(gameTime, inputMan);
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            mainmenu.Draw(gameTime, graphMan);
        }
    }
}
