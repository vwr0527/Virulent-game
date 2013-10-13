using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Virulent_dev.VectorObjects;

namespace Virulent_dev
{
    class VectorBank
    {
        private Dictionary<String, VectorBankEntry> listing;
        public VectorBank()
        {
            listing = new Dictionary<string, VectorBankEntry>();
            listing.Add("blue square", new BlueSquare());
        }

        public VectorBankEntry getVector(String entryName)
        {
            return listing[entryName];
        }
    }
}
