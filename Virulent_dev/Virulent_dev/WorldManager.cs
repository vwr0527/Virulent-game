using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Virulent_dev
{
    class WorldManager
    {
        InputManager r_inputMan;
        GraphicsManager r_graphMan;

        public WorldManager(InputManager inputMan, GraphicsManager graphMan)
        {
            r_inputMan = inputMan;
            r_graphMan = graphMan;
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

        public void Draw(GameTime gameTime)
        {
            /*
            double vari = Math.Sin((double)gameTime.TotalGameTime.TotalMilliseconds / (double)1000);
            //Debug.WriteLine(vari);
            for (int i = 0; i < 10; ++i)
            {
                r_graphMan.X(new Vector2(i * 10.0f, i * i * (float)vari));
            }
             */
        }
    }
}
