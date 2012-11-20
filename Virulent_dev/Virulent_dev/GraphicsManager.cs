using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Virulent_dev
{
    class GraphicsManager
    {
        GraphicsDeviceManager graphicsDeviceManager;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        SpriteFont currentFont;

        public GraphicsManager(Game game)
        {
            graphicsDeviceManager = new GraphicsDeviceManager(game);
        }

        public void LoadContent(ContentManager content)
        {
            graphicsDevice = graphicsDeviceManager.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            currentFont = content.Load<SpriteFont>("SpriteFont1");
        }

        public void DrawAll()
        {
            // TODO: Add your drawing code here
            graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.DrawString(currentFont, "Hello there", Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}
