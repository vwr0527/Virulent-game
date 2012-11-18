using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Virulent_dev
{
    class GraphicsDrawingManager
    {
        public void DrawAll(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            // TODO: Add your drawing code here
            graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "Hello there", Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}
