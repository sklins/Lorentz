using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace Ololorentz {
    public sealed partial class Scene : Game {
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

        private MouseData mouseData = new MouseData();
        private const float MouseSensivity = 0.01f;

        private void HandleMouseRotation(MouseState mouseState) {
            bool mouseActive = mouseState.LeftButton.HasFlag(ButtonState.Pressed);
            Point position = mouseState.Position;
            mouseData.Update(mouseActive, position);
        }
    }
}
