﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Virulent_dev
{
    /**
     * Graphics Manager:
     * manages split screen, sprite rendering
     */
    class GraphicsManager
    {
        GraphicsDeviceManager graphicsDeviceManager;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        RecycleArray<SpriteElement> spriteList;

        //constructor
        public GraphicsManager(GraphicsDeviceManager gdm)
        {
            graphicsDeviceManager = gdm;
            spriteList = new RecycleArray<SpriteElement>(SpriteElement.CopyMembers);
            spriteList.SetDataMode(false);
        }

        public void LoadContent(ContentManager content)
        {
            graphicsDevice = graphicsDeviceManager.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void DrawAll(GameTime gameTime)
        {
            spriteBatch.Begin();
            for (int i = 0; i < spriteList.Size(); ++i)
            {
                spriteList.ElementAt(i).Draw(graphicsDevice, spriteBatch);
            }
            spriteBatch.End();
            spriteList.EmptyAll();
        }

        public void Add(SpriteElement addedElement)
        {
            spriteList.Add(addedElement);
        }
    }
}
