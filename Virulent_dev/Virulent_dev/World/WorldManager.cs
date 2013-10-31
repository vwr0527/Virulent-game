using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Virulent_dev.Graphics;
using Virulent_dev.Input;

namespace Virulent_dev.World
{
    class WorldManager
    {
        private Texture2D picture;
        private SpriteElement pickle;
        private bool paused = false;
        private bool save = false;

        public WorldManager()
        {

        }

        public void LoadContent(ContentManager content)
        {
            picture = content.Load<Texture2D>("test");
            pickle = new SpriteElement(picture);
            pickle.pos.X = 0.5f;
            pickle.pos.Y = 0.5f;
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

        public void Pause()
        {
            paused = true;
            pickle.col.A = 255;
            pickle.col.R = 80;
            pickle.col.G = 80;
            pickle.col.B = 80;
        }

        public void Unpause()
        {
            paused = false;
            pickle.col.A = 255;
            pickle.col.R = 160;
            pickle.col.G = 160;
            pickle.col.B = 160;
        }

        public bool SaveGame()
        {
            if (save)
            {
                save = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPaused()
        {
            return paused;
        }
    }
}
