using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Virulent_dev.VectorObjects
{
    class VectorBankEntry
    {
        public VertexPositionColorTexture[] verticies;
        protected int size;
        public int GetSize()
        {
            return size;
        }
    }
}
