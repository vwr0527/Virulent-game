using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using Virulent_dev.Input;
using Virulent_dev.Graphics;

namespace Virulent_dev.Menu
{
    interface IMenuPage
    {
        void LoadContent(ContentManager content);
        void Update(GameTime gameTime, InputManager inputMan);
        void Draw(GameTime gameTime, GraphicsManager graphMan);
        bool SwitchingPages();
        IMenuPage GetNextPage();
        bool ExitMenu();
        bool SaveGame();
        bool ExitGame();
    }
}
