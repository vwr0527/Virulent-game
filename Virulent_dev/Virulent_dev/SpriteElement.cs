using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Virulent_dev
{
    //sprite element contains text, font, texture, position, scale, rotation, and color information
    class SpriteElement
    {
        public Vector2 pos;
        private Vector2 orig;
        public Color col;
        public StringBuilder text;
        public Texture2D texture;
        public SpriteFont font;
        public float scale;
        public float rotation;

        public SpriteElement(Texture2D textureSource, StringBuilder textSource, SpriteFont fontSource)
        {
            texture = textureSource;
            text = textSource;
            font = fontSource;

            if (texture != null)
                orig = new Vector2((float)(texture.Width / 2), (float)(texture.Height / 2));

            scale = 1;
            col = Color.White;
        }
        public static void CopyMembers(SpriteElement subject, SpriteElement target)
        {
            subject.pos.X = target.pos.X;
            subject.pos.Y = target.pos.Y;
            subject.scale = target.scale;
            subject.rotation = target.rotation;

            subject.col.R = target.col.R;
            subject.col.G = target.col.G;
            subject.col.B = target.col.B;
            subject.col.A = target.col.A;

            subject.text = target.text;
            subject.texture = target.texture;
            subject.font = target.font;

            target.orig = new Vector2((float)(target.texture.Width / 2), (float)(target.texture.Height / 2));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, pos, null, col, rotation, orig, scale, SpriteEffects.None, 0);
            }
            //TODO: If no font specified, use a default font
            if (text != null && font != null)
            {
                spriteBatch.DrawString(font, text.ToString(), pos, col);
            }
        }
    }
}
