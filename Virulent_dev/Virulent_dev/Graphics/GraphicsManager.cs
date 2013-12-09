using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Virulent_dev.Graphics
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
        RecycleArray<SpriteElement> guiSprites;
        RecycleArray<SpriteElement> worldSprites;

        Camera cam1;
        Viewport test;
        Viewport test2;

        public GraphicsManager(GraphicsDeviceManager gdm)
        {
            graphicsDeviceManager = gdm;
            guiSprites = new RecycleArray<SpriteElement>(SpriteElement.CopyMembers, SpriteElement.CreateNewCopy);
            guiSprites.SetDataMode(true);
            worldSprites = new RecycleArray<SpriteElement>(SpriteElement.CopyMembers, SpriteElement.CreateNewCopy);
            worldSprites.SetDataMode(true);

            cam1 = new Camera();
        }
        public void LoadContent(ContentManager content)
        {
            test = new Viewport(0, 0, 800, 240);
            test2 = new Viewport(0, 0, 400, 480);
            graphicsDevice = graphicsDeviceManager.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            SpriteElement.LoadDefaultFont(content);
        }

        public void DrawAll(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Black);
            //TODO: Multiple cameras
            int numCameras = 1;
            //graphicsDevice.Viewport = test2;
            cam1.CalcMatrix(graphicsDevice.Viewport);
            for (int j = 0; j < numCameras; ++j)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, cam1.matrix);
                for (int i = 0; i < worldSprites.Size(); ++i)
                {
                    worldSprites.ElementAt(i).DrawWorld(graphicsDevice, spriteBatch);
                }
                spriteBatch.End();
            }

            spriteBatch.Begin();
            for (int i = 0; i < guiSprites.Size(); ++i)
            {
                guiSprites.ElementAt(i).DrawGUIStretched(graphicsDevice, spriteBatch);
            }
            spriteBatch.End();

            guiSprites.EmptyAll();
            worldSprites.EmptyAll();
        }

        public void DrawUISprite(SpriteElement addedElement)
        {
            guiSprites.Add(addedElement);
        }

        public void DrawWorldSprite(SpriteElement addedElement)
        {
            worldSprites.Add(addedElement);
        }

        public Camera GetCamera(int whichCamera)
        {
            return cam1;
        }
    }
}
