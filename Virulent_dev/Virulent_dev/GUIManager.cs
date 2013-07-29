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
    class GUIManager
    {
        InputManager r_inputMan;
        GraphicsManager r_graphMan;
        private bool exit = false;
        private bool savegame = false;
        private bool paused = false;
        private SpriteElement textStatement;
        private StringBuilder[] txt;
        private Texture2D picture;
        private SpriteElement pickle;
        private MenuPage mainmenu;

        public GUIManager(InputManager inputMan, GraphicsManager graphMan)
        {
            r_inputMan = inputMan;
            r_graphMan = graphMan;
            txt = new StringBuilder[10];
            txt[0] = new StringBuilder();
            txt[0].Append("Hello");
            mainmenu = new MenuPage();
        }

        public void LoadContent(ContentManager content)
        {
            picture = content.Load<Texture2D>("test");
        }

        public bool ExitRequested()
        {
            return exit;
        }

        public bool SaveRequested()
        {
            return savegame;
        }

        public void BringUpMenu()
        {
            if (r_inputMan.StartPressed())
            {

            }
        }

        public bool IsPaused()
        {
            return paused;
        }

        public void Update(GameTime gameTime)
        {
            exit = r_inputMan.IsBackPressed();
            savegame = r_inputMan.APressed();
        }

        public void Draw(GameTime gameTime)
        {
            if (pickle == null) pickle = r_graphMan.AddSprite(picture);
            else
            {
                pickle.pos.X = 0.5f;
                pickle.pos.Y = 0.5f;
                pickle.col.A = 200;
                pickle.col.R = 80;
                pickle.col.G = 80;
                pickle.col.B = 80;
                pickle.scale = 3.2f;
            }
            if (textStatement == null) textStatement = r_graphMan.AddText(txt[0], null);
        }
    }
}
