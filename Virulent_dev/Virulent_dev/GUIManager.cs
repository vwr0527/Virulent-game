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
        GraphicsManager r_graphMan;
        private bool exit = false;
        private bool savegame = false;
        private SpriteElement textStatement;
        private StringBuilder[] txt;

        public GUIManager(InputManager inputMan, GraphicsManager graphMan)
        {
            r_inputMan = inputMan;
            r_graphMan = graphMan;
            txt = new StringBuilder[10];
            txt[0] = new StringBuilder();
            txt[0].Append("Hello");
        }

        public bool ExitRequested()
        {
            return exit;
        }

        public bool SaveRequested()
        {
            return savegame;
        }

        public void Update(GameTime gameTime)
        {
            exit = r_inputMan.IsBackPressed();
            savegame = r_inputMan.APressed();

        }

        public void Draw(GameTime gameTime)
        {
            if (textStatement == null)
            {
                textStatement = r_graphMan.AddText(txt[0], null);
            }

            //spriteBatch.Draw(texture, Vector2.Zero, Color.White);
        }
    }
}
