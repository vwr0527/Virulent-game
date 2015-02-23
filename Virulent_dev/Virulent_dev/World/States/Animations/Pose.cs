using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Virulent_dev.Graphics;
using Microsoft.Xna.Framework;

namespace Virulent_dev.World.States.Animations
{
    class Pose
    {
        struct SEInfo
        {
            public float x;
            public float y;
            public float scale;
            public float rot;
            public float r;
            public float g;
            public float b;
        }

        List<SEInfo> sprites;

        public Pose()
        {
            sprites = new List<SEInfo>();
        }

        public void Add(float x, float y, float scale, float rot, float r, float g, float b)
        {
            SEInfo spriteinfo = new SEInfo();
            spriteinfo.x = x;
            spriteinfo.y = y;
            spriteinfo.scale = scale;
            spriteinfo.rot = rot;
            spriteinfo.r = r;
            spriteinfo.g = g;
            spriteinfo.b = b;
            sprites.Add(spriteinfo);
        }

        public void Add(float x, float y, float scale, float rot)
        {
            SEInfo spriteinfo = new SEInfo();
            spriteinfo.x = x;
            spriteinfo.y = y;
            spriteinfo.scale = scale;
            spriteinfo.rot = rot;
            spriteinfo.r = 1;
            spriteinfo.g = 1;
            spriteinfo.b = 1;
            sprites.Add(spriteinfo);
        }

        public void Add(float x, float y)
        {
            SEInfo spriteinfo = new SEInfo();
            spriteinfo.x = x;
            spriteinfo.y = y;
            spriteinfo.scale = 1;
            spriteinfo.rot = 0;
            spriteinfo.r = 1;
            spriteinfo.g = 1;
            spriteinfo.b = 1;
            sprites.Add(spriteinfo);
        }

        public void PoseSpriteElement(SpriteElement s, int spriteNum, float x, float y, float rot)
        {
            SEInfo spriteinfo = sprites[spriteNum];

            s.pos.X = x + spriteinfo.x;
            s.pos.Y = y + spriteinfo.y;
            s.scale = spriteinfo.scale;
            s.rotation = rot + spriteinfo.rot;
            s.col = new Color(spriteinfo.r, spriteinfo.g, spriteinfo.b);
        }
    }
}
