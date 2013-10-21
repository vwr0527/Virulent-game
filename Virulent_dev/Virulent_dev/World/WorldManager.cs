using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Virulent_dev
{
    class WorldManager
    {
        private Texture2D picture;
        private SpriteElement pickle;

        public WorldManager()
        {

        }

        public void LoadContent(ContentManager content)
        {
            picture = content.Load<Texture2D>("test");
            pickle = new SpriteElement(picture);
            pickle.pos.X = 0.5f;
            pickle.pos.Y = 0.5f;
            pickle.col.A = 200;
            pickle.col.R = 80;
            pickle.col.G = 80;
            pickle.col.B = 80;
            pickle.scale = 4f;
        }

        public void Update(GameTime gameTime, InputManager inputMan)
        {
            pickle.rotation += gameTime.ElapsedGameTime.Milliseconds / 3000f;
        }

        public void PausedUpdate(GameTime gameTime, InputManager inputMan)
        {
            pickle.rotation += gameTime.ElapsedGameTime.Milliseconds / 30000f;
        }

        public void Draw(GameTime gameTime, GraphicsManager graphMan)
        {
            graphMan.Add(pickle);
        }
    }
}
