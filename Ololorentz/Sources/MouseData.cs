using System;
using Microsoft.Xna.Framework;

namespace Ololorentz {
    public sealed class MouseData {
        private float phi = 0;
        private float theta = 0;

        private float phiPart = 0;
        private float thetaPart = 0;

        public float Phi {
            get { return phi + phiPart; }
        }

        public float Theta {
            get { return theta + thetaPart; }
        }

        private bool prevMouseActive = false; // If the mouse was active before
        private Point originalPosition;
        private Point currentPosition;

        private void ReCalculateCameraAngles() {
            phiPart = currentPosition.X - originalPosition.X;
            thetaPart = currentPosition.Y - originalPosition.Y;
        }

        private void HandleMouseDown(Point position) {
            originalPosition = position;
        }

        private void HandleMouseUp() {
            phi += phiPart;
            theta += thetaPart;
            phiPart = thetaPart = 0;
        }

        public void Update(bool mouseActive, Point position) {
            currentPosition = position;

            if (mouseActive) {
                if (prevMouseActive)
                    ReCalculateCameraAngles();
                else
                    HandleMouseDown(position);
            } else if (prevMouseActive) {
                HandleMouseUp();
            }

            prevMouseActive = mouseActive;
        }

        public void DropAllRotation() {
            phi = theta = phiPart = thetaPart = 0;
        }
    }
}
