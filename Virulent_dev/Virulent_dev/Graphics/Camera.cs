using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Virulent_dev.Graphics
{
    class Camera
    {
        public Matrix matrix = new Matrix(1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1);
        public float rot = 0;
        public Vector2 pos = new Vector2();
        public float scale = 1;

        int asdf = 0;
        public void CalcMatrix(Viewport view)
        {
            int viewwidth = view.Width / 2;
            int viewheight = view.Height / 2;

            //asdf++;
            /*
            matrix = Matrix.Identity;
            matrix = Matrix.CreateTranslation(pos.X + viewwidth, pos.Y + viewheight, 0) * matrix;
            matrix = Matrix.CreateRotationZ(rot) * matrix;
            matrix = Matrix.CreateScale(scale) * matrix;
             * */
            if (asdf > 100) asdf = 0;
            matrix = Matrix.Identity;
            matrix = Matrix.CreateScale(scale) * matrix;
            matrix = Matrix.CreateTranslation(-pos.X + (viewwidth / scale), -pos.Y + (viewheight / scale), 0) * matrix;
            matrix = Matrix.CreateRotationZ(rot) * matrix;
        }
    }
}
