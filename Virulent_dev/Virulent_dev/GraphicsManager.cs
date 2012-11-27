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
    /**
     * Graphics Manager:
     * manages split screen, sprite rendering, geometry rendering,
     * 
     */
    class GraphicsManager
    {
        GraphicsDeviceManager graphicsDeviceManager;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        SpriteFont currentFont;

        //basiceffect
        Matrix worldMatrix;
        Matrix viewMatrix;
        Matrix projectionMatrix;
        VertexPositionNormalTexture[] cubeVertices;
        VertexDeclaration basicEffectVertexDeclaration;
        VertexBuffer vertexBuffer;
        BasicEffect basicEffect;

        //my stuff
        VRArray<GraphicElement> drawList;

        public GraphicsManager(Game game)
        {
            graphicsDeviceManager = new GraphicsDeviceManager(game);
            drawList = new VRArray<GraphicElement>(GraphicElement.CopyMembers);

            worldMatrix = new Matrix();
            viewMatrix = new Matrix();
            projectionMatrix = new Matrix();

        }

        public void LoadContent(ContentManager content)
        {
            graphicsDevice = graphicsDeviceManager.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            currentFont = content.Load<SpriteFont>("SpriteFont1");
        }

        public void X(Vector2 position)
        {
            GraphicElement g = drawList.GetEmptyElement();
            if (g == null)
            {
                g = new GraphicElement();
                //Debug.WriteLine("made a new GE");
            }
            g.pos = position;
            drawList.Add(g);
        }
        
        public void DrawAll()
        {
            // TODO: Add your drawing code here
            graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.DrawString(currentFont, "Hello there", Vector2.Zero, Color.White);
            for (int i = 0; i < drawList.Size(); ++i)
            {
                //Debug.WriteLine("drawing [" + i + "] with position " + drawList.ElementAt(i).pos);
                spriteBatch.DrawString(currentFont, "X", drawList.ElementAt(i).pos, Color.Black);
            }
            spriteBatch.End();
            drawList.EmptyAll();
        }
    }
}
