using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Virulent_dev
{
    class WorldManager
    {
        InputManager r_inputMan;

        public WorldManager(InputManager inputMan)
        {
            r_inputMan = inputMan;
        }

        public bool ExitRequested()
        {
            return false;
        }

        public bool SaveRequested()
        {
            return false;
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
