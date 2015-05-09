using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace Ololorentz {
    public sealed partial class Scene : Game {
        protected override void Update(GameTime gameTime) {
            float timePercentage = 100.0f * (t - Scenario.MinTime) / (Scenario.MaxTime - Scenario.MinTime);

            HandleMouseRotation(Mouse.GetState());
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                mouseData.DropAllRotation();

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
