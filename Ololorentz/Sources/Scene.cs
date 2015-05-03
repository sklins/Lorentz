using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace Ololorentz {
    public sealed class Scene : Game {
        public Scenario Scenario { get; private set; }

        private float t;

        private enum SceneState {
            Before, Running, Over
        }

        private SceneState state = SceneState.Before;

        #pragma warning disable
        private GraphicsDeviceManager graphics;
        #pragma warning restore

        public const string WindowTitle = "Ololorentz 3D";

        public Scene(Scenario scenario) {
            this.graphics = new GraphicsDeviceManager(this);
            this.Scenario = scenario;
            this.t = Scenario.MinTime;
            Window.Title = String.Format("{0} (press space to start)", WindowTitle);
        }

        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            BasicEffect basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // Setting up the camera position
            basicEffect.View = Matrix.CreateLookAt(
                Scenario.CameraPosition, Scenario.CameraTarget, Vector3.Up);

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

        protected override void Update(GameTime gameTime) {
            float timePercentage = 100.0f * (t - Scenario.MinTime) / (Scenario.MaxTime - Scenario.MinTime);

            switch (state) {
            case SceneState.Before:
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    state = SceneState.Running;
                break;

            case SceneState.Running:
                t += Scenario.TimeStep;
                Window.Title = String.Format("{0} ({1}%)", WindowTitle, Math.Round(timePercentage));
                if (t >= Scenario.MaxTime)
                    state = SceneState.Over;
                break;

            case SceneState.Over:
                Window.Title = String.Format("{0} (over, press space to restart)", WindowTitle);
                if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                    state = SceneState.Running;
                    t = Scenario.MinTime;
                }
                break;
            }
        }
    }
}
