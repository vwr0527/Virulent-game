using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Virulent_dev
{
    class GUIManager
    {
        InputManager r_inputMan;

        public GUIManager(InputManager inputMan)
        {
            r_inputMan = inputMan;
        }

        public bool ExitRequested()
        {
            return r_inputMan.IsBackPressed();
        }

        public bool SaveRequested()
        {
            return r_inputMan.APressed();
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
