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
     * uses graphic elements to draw anything.
     * needs connection to resourcemanager
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
        }

        //move these to some kind of graphics element factory class
        public SpriteElement AddText(StringBuilder txt, SpriteFont fontChoice)
        {
            if (fontChoice == null)
            {
                return null;
            }
            SpriteElement addedElement = new SpriteElement(null, txt, fontChoice);
            spriteList.Add(addedElement);

            return addedElement;
        }
        public SpriteElement AddSprite(Texture2D textureChoice)
        {
            SpriteElement addedElement = new SpriteElement(textureChoice, null, null);
            spriteList.Add(addedElement);

            return addedElement;
        }
    }
}
/*
 *         

        //basiceffect
        Matrix worldMatrix;
        Matrix viewMatrix;
        Matrix projectionMatrix;
        BasicEffect basicEffect;
        RasterizerState rasterizerState;

        //vertecies
        VertexPositionColorTexture[] pointList;
        short[] lineListIndices;
        int numPoints;
        VertexBuffer vertexBuffer;

        //my stuff
        RecycleArray<GraphicElement> drawList;
        RecycleArray<SpriteElement> spriteList;
        
        public GraphicsManager(GraphicsDeviceManager gdm)
        {
            graphicsDeviceManager = gdm;
            worldMatrix = new Matrix();
            viewMatrix = new Matrix();
            projectionMatrix = new Matrix();

            drawList = new RecycleArray<GraphicElement>(GraphicElement.CopyMembers);
            spriteList = new RecycleArray<SpriteElement>(SpriteElement.CopyMembers);
        }
        private void SetupView(float tiltAngle)
        {
            float tilt = MathHelper.ToRadians(tiltAngle);
            // Use the world matrix to tilt the cube along x and y axes.
            worldMatrix = Matrix.CreateRotationZ(tilt);// Matrix.CreateRotationX(tilt) * Matrix.CreateRotationY(tilt);
            //worldMatrix = Matrix.Identity;
            viewMatrix = Matrix.CreateLookAt(new Vector3(-5, 0, 0), Vector3.Zero, Vector3.Up);
            
            //projectionMatrix = Matrix.CreateOrthographic(10, 10 * ((float)graphicsDevice.Viewport.Height / (float)graphicsDevice.Viewport.Width), 0.1f, 1000f);

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45),  // 45 degree angle
                (float)graphicsDevice.Viewport.Width /
                (float)graphicsDevice.Viewport.Height,
                1.0f, 10000.0f);

            basicEffect.World = worldMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;
        }
        Random rand = new Random();
        Color randomColor()
        {
            return new Color((float)rand.NextDouble(),
                (float)rand.NextDouble(),
                (float)rand.NextDouble());
        }
        public void LoadContent(ContentManager content)
        {
            graphicsDevice = graphicsDeviceManager.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);

            #region Create verticies
            numPoints = 65536;
            pointList = new VertexPositionColorTexture[numPoints];
            for (int x = 0; x < numPoints / 2; ++x)
            {
                for (int y = 0; y < 2; ++y)
                {
                    pointList[(x * 2) + y] = new VertexPositionColorTexture(
                        new Vector3(0.1f * x * (float)Math.Cos((double)x / 4.0f), ((x / 8.0f) + y), 0.1f * x * (float)Math.Sin((double)x / 4.0f)), randomColor(), new Vector2(0, 0));
                }
            }
            #endregion
            #region Create vertex index arrays
            lineListIndices = new short[numPoints];
            for (int i = 0; i < numPoints; ++i)
            {
                lineListIndices[i] = (short)(i);
            }
            #endregion
            #region Setup vertex buffer
            vertexBuffer = new VertexBuffer(
                            graphicsDevice,
                            VertexPositionColorTexture.VertexDeclaration,
                            pointList.Length,
                            BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColorTexture>(pointList);
            #endregion

            basicEffect = new BasicEffect(graphicsDevice);
            SetupView(0);
            #region Initialize rasterizer state
            rasterizerState = new RasterizerState();
            rasterizerState.FillMode = FillMode.WireFrame;
            rasterizerState.CullMode = CullMode.None;
            #endregion
        }
        public void DrawAll(GameTime gameTime)
        {
            #region Update Verticies
            for (int i = 0, j = drawList.Size(); i < j; ++i)
            {
                drawList.ElementAt(i).Update(gameTime);
            }
            #endregion

            graphicsDevice.Clear(Color.Black);

            SetupView((float)gameTime.TotalGameTime.TotalMilliseconds / 100.0f);

            graphicsDevice.RasterizerState = rasterizerState;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphicsDevice.SetVertexBuffer(vertexBuffer);

            basicEffect.VertexColorEnabled = true;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                    PrimitiveType.TriangleStrip,
                    pointList,
                    0,  // vertex buffer offset to add to each element of the index buffer
                    numPoints,  // number of vertices in pointList
                    lineListIndices,  // the index buffer
                    0,  // first index element to read
                    numPoints-2   // number of primitives to draw
                );
            }
            #region Draw gui and text
            spriteBatch.Begin();
            for (int i = 0; i < spriteList.Size(); ++i)
            {
                spriteList.ElementAt(i).Draw(graphicsDevice, spriteBatch);
            }
            spriteBatch.End();
            #endregion
        }
        public GraphicElement AddGeom()
        {
            GraphicElement addedElement = new GraphicElement();
            drawList.Add(addedElement);

            return addedElement;
        }
*/