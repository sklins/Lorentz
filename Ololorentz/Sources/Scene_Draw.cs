using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace Ololorentz {
    public sealed partial class Scene : Game {
        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            BasicEffect basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // Setting up the camera position
            Matrix cameraRotation = Matrix.CreateRotationY(mouseData.Phi * MouseSensivity) *
                Matrix.CreateRotationX(mouseData.Theta * MouseSensivity);

            basicEffect.View = Matrix.CreateLookAt(
                Vector3.Transform(Scenario.CameraPosition, cameraRotation),
                Scenario.CameraTarget, Vector3.Up);

            // Setting up the perspective projection
            float aspectRatio = (float)graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
            basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(
                Scenario.FovAngle, aspectRatio, Scenario.NearPlane, Scenario.FarPlane);


            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes) {
                pass.Apply();
                VertexPositionColor[] vpc = Scenario.GetTriangulation(t).ToArray();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vpc, 0, vpc.Length / 3);
            }
        }
    }
}
