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
        private MainMenu rootMenu;
        private MenuPage currentMenu;
        private bool active = true;
        private bool quit = false;
        private bool save = false;

        public MenuManager()
        {
            rootMenu = new MainMenu();
            currentMenu = rootMenu;
        }

        public void LoadContent(ContentManager content)
        {
            rootMenu.LoadContent(content);
        }

        public void Update(GameTime gameTime, InputManager inputMan)
        {
            currentMenu.Update(gameTime, inputMan);

            if (currentMenu.ExitMenu())
            {
                active = false;
            }

            if (currentMenu.SwitchingPages())
            {
                currentMenu = currentMenu.GetNextPage();
            }

            if (currentMenu.SaveGame())
            {
                save = true;
            }

            if (currentMenu.ExitGame())
            {
                quit = true;
            }
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            currentMenu.Draw(gameTime, graphMan);
        }

        public bool IsActive()
        {
            return active;
        }

        public void Activate()
        {
            active = true;
        }

        public bool SaveGame()
        {
            if (save)
            {
                save = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool QuitGame()
        {
            return quit;
        }
    }
}
