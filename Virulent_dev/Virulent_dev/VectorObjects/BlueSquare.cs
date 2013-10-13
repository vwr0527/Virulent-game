using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Virulent_dev.VectorObjects
{
    class BlueSquare : VectorBankEntry
    {
        public BlueSquare()
        {
            verticies = new VertexPositionColorTexture[4];
            verticies[0] = new VertexPositionColorTexture(new Vector3(-1, -1, 0), new Color(0, 0, 255), Vector2.Zero);
            verticies[1] = new VertexPositionColorTexture(new Vector3(1, -1, 0), new Color(0, 0, 255), Vector2.Zero);
            verticies[2] = new VertexPositionColorTexture(new Vector3(1, 1, 0), new Color(0, 0, 255), Vector2.Zero);
            verticies[3] = new VertexPositionColorTexture(new Vector3(-1, 1, 0), new Color(0, 0, 255), Vector2.Zero);
            size = 4;
        }
    }
}
