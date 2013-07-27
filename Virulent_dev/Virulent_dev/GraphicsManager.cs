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
        SpriteFont currentFont;

        //basiceffect
        Matrix worldMatrix;
        Matrix viewMatrix;
        Matrix projectionMatrix;
        VertexPositionColorTexture[] pointList;
        short[] lineListIndices;
        int numPoints;
        VertexPositionNormalTexture[] cubeVertices;
        VertexDeclaration basicEffectVertexDeclaration;
        VertexBuffer vertexBuffer;
        BasicEffect basicEffect;
        RasterizerState rasterizerState1;

        //my stuff
        VRArray<GraphicElement> drawList;
        Texture2D texture;

        //constructor
        public GraphicsManager(Game game)
        {
            graphicsDeviceManager = new GraphicsDeviceManager(game);

            worldMatrix = new Matrix();
            viewMatrix = new Matrix();
            projectionMatrix = new Matrix();

            drawList = new VRArray<GraphicElement>(GraphicElement.CopyMembers);
        }

        private void InitializeBasicEffect()
        {
            basicEffect = new BasicEffect(graphicsDevice);

            basicEffect.World = worldMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;
        }
        private void TurnOnBasicEffectLighting()
        {
            // primitive color
            basicEffect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
            basicEffect.DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);
            basicEffect.SpecularColor = new Vector3(0.25f, 0.25f, 0.25f);
            basicEffect.SpecularPower = 5.0f;
            basicEffect.Alpha = 1.0f;
            basicEffect.VertexColorEnabled = false;
            basicEffect.LightingEnabled = true;
            if (basicEffect.LightingEnabled)
            {
                basicEffect.DirectionalLight0.Enabled = true; // enable each light individually
                if (basicEffect.DirectionalLight0.Enabled)
                {
                    // x direction
                    basicEffect.DirectionalLight0.DiffuseColor = new Vector3(1, 0, 0); // range is 0 to 1
                    basicEffect.DirectionalLight0.Direction = Vector3.Normalize(new Vector3(-1, 0, 0));
                    // points from the light to the origin of the scene
                    basicEffect.DirectionalLight0.SpecularColor = Vector3.One;
                }

                basicEffect.DirectionalLight1.Enabled = true;
                if (basicEffect.DirectionalLight1.Enabled)
                {
                    // y direction
                    basicEffect.DirectionalLight1.DiffuseColor = new Vector3(0, 0.75f, 0);
                    basicEffect.DirectionalLight1.Direction = Vector3.Normalize(new Vector3(0, -1, 0));
                    basicEffect.DirectionalLight1.SpecularColor = Vector3.One;
                }

                basicEffect.DirectionalLight2.Enabled = true;
                if (basicEffect.DirectionalLight2.Enabled)
                {
                    // z direction
                    basicEffect.DirectionalLight2.DiffuseColor = new Vector3(0, 0, 0.5f);
                    basicEffect.DirectionalLight2.Direction = Vector3.Normalize(new Vector3(0, 0, -1));
                    basicEffect.DirectionalLight2.SpecularColor = Vector3.One;
                }
            }
        }
        private void InitRasterizerState()
        {
            rasterizerState1 = new RasterizerState();

            rasterizerState1.FillMode = FillMode.WireFrame;
            rasterizerState1.CullMode = CullMode.None;
        }
        private void SetupGraphicsDevice()
        {
            graphicsDevice.RasterizerState = rasterizerState1;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphicsDevice.SetVertexBuffer(vertexBuffer);
        }
        private void InitVertexDeclaration()
        {
            basicEffectVertexDeclaration = new VertexDeclaration(new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                    new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                    new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
                }
            );
        }
        private void SetupCubeVertexBuffer()
        {
            vertexBuffer = new VertexBuffer(
                            graphicsDevice,
                            VertexPositionNormalTexture.VertexDeclaration,
                            cubeVertices.Length,
                            BufferUsage.None);

            vertexBuffer.SetData<VertexPositionNormalTexture>(cubeVertices);
        }
        private void CreateCube()
        {
            //to draw: 
            /*graphicsDevice.DrawPrimitives(
                PrimitiveType.TriangleList,
                0,
                12
            );*/

            Vector3 topLeftFront = new Vector3(-1.0f, 1.0f, 1.0f);
            Vector3 bottomLeftFront = new Vector3(-1.0f, -1.0f, 1.0f);
            Vector3 topRightFront = new Vector3(1.0f, 1.0f, 1.0f);
            Vector3 bottomRightFront = new Vector3(1.0f, -1.0f, 1.0f);
            Vector3 topLeftBack = new Vector3(-1.0f, 1.0f, -1.0f);
            Vector3 topRightBack = new Vector3(1.0f, 1.0f, -1.0f);
            Vector3 bottomLeftBack = new Vector3(-1.0f, -1.0f, -1.0f);
            Vector3 bottomRightBack = new Vector3(1.0f, -1.0f, -1.0f);

            Vector2 textureTopLeft = new Vector2(0.0f, 0.0f);
            Vector2 textureTopRight = new Vector2(1.0f, 0.0f);
            Vector2 textureBottomLeft = new Vector2(0.0f, 1.0f);
            Vector2 textureBottomRight = new Vector2(1.0f, 1.0f);

            Vector3 frontNormal = new Vector3(0.0f, 0.0f, 1.0f);
            Vector3 backNormal = new Vector3(0.0f, 0.0f, -1.0f);
            Vector3 topNormal = new Vector3(0.0f, 1.0f, 0.0f);
            Vector3 bottomNormal = new Vector3(0.0f, -1.0f, 0.0f);
            Vector3 leftNormal = new Vector3(-1.0f, 0.0f, 0.0f);
            Vector3 rightNormal = new Vector3(1.0f, 0.0f, 0.0f);

            cubeVertices = new VertexPositionNormalTexture[36];
            // Front face.
            cubeVertices[0] =
                new VertexPositionNormalTexture(
                topLeftFront, frontNormal, textureTopLeft);
            cubeVertices[1] =
                new VertexPositionNormalTexture(
                bottomLeftFront, frontNormal, textureBottomLeft);
            cubeVertices[2] =
                new VertexPositionNormalTexture(
                topRightFront, frontNormal, textureTopRight);
            cubeVertices[3] =
                new VertexPositionNormalTexture(
                bottomLeftFront, frontNormal, textureBottomLeft);
            cubeVertices[4] =
                new VertexPositionNormalTexture(
                bottomRightFront, frontNormal, textureBottomRight);
            cubeVertices[5] =
                new VertexPositionNormalTexture(
                topRightFront, frontNormal, textureTopRight);

            // Back face.
            cubeVertices[6] =
                new VertexPositionNormalTexture(
                topLeftBack, backNormal, textureTopLeft);
            cubeVertices[7] =
                new VertexPositionNormalTexture(
                bottomLeftBack, backNormal, textureBottomLeft);
            cubeVertices[8] =
                new VertexPositionNormalTexture(
                topRightBack, backNormal, textureTopRight);
            cubeVertices[9] =
                new VertexPositionNormalTexture(
                bottomLeftBack, backNormal, textureBottomLeft);
            cubeVertices[10] =
                new VertexPositionNormalTexture(
                bottomRightBack, backNormal, textureBottomRight);
            cubeVertices[11] =
                new VertexPositionNormalTexture(
                topRightBack, backNormal, textureTopRight);

            // Left face.
            cubeVertices[12] =
                new VertexPositionNormalTexture(
                topLeftFront, leftNormal, textureTopLeft);
            cubeVertices[13] =
                new VertexPositionNormalTexture(
                bottomLeftFront, leftNormal, textureBottomLeft);
            cubeVertices[14] =
                new VertexPositionNormalTexture(
                topLeftBack, leftNormal, textureTopRight);
            cubeVertices[15] =
                new VertexPositionNormalTexture(
                bottomLeftFront, leftNormal, textureBottomLeft);
            cubeVertices[16] =
                new VertexPositionNormalTexture(
                bottomLeftBack, leftNormal, textureBottomRight);
            cubeVertices[17] =
                new VertexPositionNormalTexture(
                topLeftBack, leftNormal, textureTopRight);

            // Right face.
            cubeVertices[18] =
                new VertexPositionNormalTexture(
                topRightFront, rightNormal, textureTopLeft);
            cubeVertices[19] =
                new VertexPositionNormalTexture(
                bottomRightFront, rightNormal, textureBottomLeft);
            cubeVertices[20] =
                new VertexPositionNormalTexture(
                topRightBack, rightNormal, textureTopRight);
            cubeVertices[21] =
                new VertexPositionNormalTexture(
                bottomRightFront, rightNormal, textureBottomLeft);
            cubeVertices[22] =
                new VertexPositionNormalTexture(
                bottomRightBack, rightNormal, textureBottomRight);
            cubeVertices[23] =
                new VertexPositionNormalTexture(
                topRightBack, rightNormal, textureTopRight);

            // Top face.
            cubeVertices[24] =
                new VertexPositionNormalTexture(
                topLeftFront, topNormal, textureTopLeft);
            cubeVertices[25] =
                new VertexPositionNormalTexture(
                topRightFront, topNormal, textureBottomLeft);
            cubeVertices[26] =
                new VertexPositionNormalTexture(
                topRightBack, topNormal, textureTopRight);
            cubeVertices[27] =
                new VertexPositionNormalTexture(
                topLeftFront, topNormal, textureBottomLeft);
            cubeVertices[28] =
                new VertexPositionNormalTexture(
                topRightBack, topNormal, textureBottomRight);
            cubeVertices[29] =
                new VertexPositionNormalTexture(
                topLeftBack, topNormal, textureTopRight);

            // Bottom face.
            cubeVertices[30] =
                new VertexPositionNormalTexture(
                bottomLeftFront, bottomNormal, textureTopLeft);
            cubeVertices[31] =
                new VertexPositionNormalTexture(
                bottomRightFront, bottomNormal, textureBottomLeft);
            cubeVertices[32] =
                new VertexPositionNormalTexture(
                bottomRightBack, bottomNormal, textureTopRight);
            cubeVertices[33] =
                new VertexPositionNormalTexture(
                bottomLeftFront, bottomNormal, textureBottomLeft);
            cubeVertices[34] =
                new VertexPositionNormalTexture(
                bottomRightBack, bottomNormal, textureBottomRight);
            cubeVertices[35] =
                new VertexPositionNormalTexture(
                bottomLeftBack, bottomNormal, textureTopRight);
        }
        private void CreateVerts()
        {
            numPoints = 65536;
            pointList = new VertexPositionColorTexture[numPoints];

            //zig zag up
            for (int x = 0; x < numPoints / 2; ++x)
            {
                for (int y = 0; y < 2; ++y)
                {
                    pointList[(x * 2) + y] = new VertexPositionColorTexture(
                        new Vector3(x, y, (float)Math.Sin(((double)x) / 2f)*2.0f), Color.White, new Vector2(0, 0));
                }
            }
        }
        private void CreateVertIndicies()
        {
            // Initialize an array of indices of type short.
            /*lineListIndices = new short[(numPoints * 2) - 2];

            // Populate the array with references to indices in the vertex buffer
            for (int i = 0; i < numPoints - 1; ++i)
            {
                lineListIndices[i * 2] = (short)(i);
                lineListIndices[(i * 2) + 1] = (short)(i + 1);
            }*/

            lineListIndices = new short[numPoints];

            // Populate the array with references to indices in the vertex buffer
            for (int i = 0; i < numPoints; ++i)
            {
                lineListIndices[i] = (short)(i);
            }
        }
        private void SetupVertexBuffer()
        {
            vertexBuffer = new VertexBuffer(
                            graphicsDevice,
                            VertexPositionColorTexture.VertexDeclaration,
                            pointList.Length,
                            BufferUsage.None);

            vertexBuffer.SetData<VertexPositionColorTexture>(pointList);
        }

        //2D camera
        private void Setup2DView()
        {
            worldMatrix = Matrix.Identity;
            viewMatrix = Matrix.CreateLookAt(new Vector3(5f, 5f, 5f), Vector3.Zero, Vector3.Up);
            projectionMatrix = Matrix.CreateOrthographic(10, 10 * ((float)graphicsDevice.Viewport.Height / (float)graphicsDevice.Viewport.Width), 0.1f, 1000f);
        }
        private void Setup2DView(float tiltAngle)
        {
            float tilt = MathHelper.ToRadians(tiltAngle);  // 0 degree angle
            // Use the world matrix to tilt the cube along x and y axes.
            worldMatrix = Matrix.CreateRotationX(tilt) * Matrix.CreateRotationY(tilt);
            viewMatrix = Matrix.CreateLookAt(new Vector3(5, 5, 5), Vector3.Zero, Vector3.Up);

            projectionMatrix = Matrix.CreateOrthographic(10, 10 * ((float)graphicsDevice.Viewport.Height / (float)graphicsDevice.Viewport.Width), 0.1f, 1000f);
        }
        //3D camera
        private void SetupView()
        {
            worldMatrix = Matrix.Identity;
            viewMatrix = Matrix.CreateLookAt(new Vector3(5, 5, 5), Vector3.Zero, Vector3.Up);

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45),  // 45 degree angle
                (float)graphicsDevice.Viewport.Width /
                (float)graphicsDevice.Viewport.Height,
                1.0f, 100.0f);
        }
        private void SetupView(float tiltAngle)
        {
            float tilt = MathHelper.ToRadians(tiltAngle);
            // Use the world matrix to tilt the cube along x and y axes.
            worldMatrix = Matrix.CreateRotationX(tilt) * Matrix.CreateRotationY(tilt);
            viewMatrix = Matrix.CreateLookAt(new Vector3(5, 3, 5), Vector3.Zero, Vector3.Up);

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45),  // 45 degree angle
                (float)graphicsDevice.Viewport.Width /
                (float)graphicsDevice.Viewport.Height,
                1.0f, 100.0f);
        }


        public void LoadContent(ContentManager content)
        {
            graphicsDevice = graphicsDeviceManager.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            currentFont = content.Load<SpriteFont>("SpriteFont1");
            texture = content.Load<Texture2D>("test");

            CreateVerts();
            CreateVertIndicies();

            SetupView();
            InitializeBasicEffect();
            SetupVertexBuffer();
            InitRasterizerState();
        }
        /*
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
        */

        float value = 1f;
        public void DrawAll()
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(currentFont, "Hello there", Vector2.Zero, Color.White);
            for (int i = 0, j = drawList.Size(); i < j; ++i)
            {
                drawList.ElementAt(i).Draw(spriteBatch, graphicsDevice);
            }
            spriteBatch.End();

            SetupGraphicsDevice();
            value += 1.0f;
            SetupView(value);
            //Debug.WriteLine(value);

            basicEffect.World = worldMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;
            basicEffect.TextureEnabled = true;
            basicEffect.Texture = texture;

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

            drawList.EmptyAll();
        }
    }
}
