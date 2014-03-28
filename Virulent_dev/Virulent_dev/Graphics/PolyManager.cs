using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Virulent_dev.Graphics
{
    class PolyManager
    {
        int curIndex = 0;
        int numShapes = 0;
        int numVerts = 0;
        BasicEffect basicEffect;
        VertexPositionColor[] vertices;

        public PolyManager()
        {
        }

        public void Initialize(GraphicsDevice graphicsDevice)
        {
            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
               (0, graphicsDevice.Viewport.Width,     // left, right
                graphicsDevice.Viewport.Height, 0,    // bottom, top
                0, 1);                                         // near, far plane


            vertices = new VertexPositionColor[65535]; //why this number? why not?
        }
        
        public void Draw(GameTime gameTime, GraphicsDevice gd)
        {
            //gd.Clear(Color.CornflowerBlue);

            if (numVerts > 0)
            {
                basicEffect.CurrentTechnique.Passes[0].Apply();
                gd.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertices, 0, numVerts);
            }

            Debug.WriteLine("numverts = " + numVerts);
        }

        public void Update(GameTime gameTime)
        {
            numVerts = 0;
        }

        public void StartShape()
        {
        }

        public void AddPoint(float x, float y, Color col)
        {
            vertices[numVerts].Position.X = x;
            vertices[numVerts].Position.Y = y;
            vertices[numVerts].Color = col;
            ++numVerts;
        }

        public void EndShape()
        {
        }

        public void RemoveAllShapes()
        {
        }
    }
}
