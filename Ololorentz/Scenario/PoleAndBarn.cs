using System;
using Microsoft.Xna.Framework;

namespace Ololorentz {
    public sealed class PoleAndBarn : ScenarioBuilder {
        public override string ScenarioTitle {
            get { return "Pole and barn"; }
        }

        [ScenarioParameter(DefaultValue = "false")]
        public bool PoleReferenceFrame { get; set; } = false;

        [ScenarioParameter(DefaultValue = "0.1")]
        public float Beta { get; set; } = 0.83f;

        public override Scenario BuildScenario() {
            Func<float, float> f = t => 4 * (float)
                Math.Exp(-Math.Pow((double) t * 3, 2));

            float
            poleLength = 9.2f,
            poleOffset = 10,
            tMin = -10,
            tMax = 10,
            poleSpeed = (2 * poleOffset + poleLength) / (tMax - tMin),
            d = 0.5f;

            Func<float, float> poleXCoord = t => poleOffset - poleSpeed * (t - tMin);

            Trajectory
            // Barn
            barnA  = t => new Vector3(-5, 0, -2),
            barnAf = t => new Vector3(-5, -3, -2),
            barnB  = t => new Vector3(-5, 0, 2),
            barnBf = t => new Vector3(-5, -3, 2),
            barnC  = t => new Vector3(5, 0, 2),
            barnCf = t => new Vector3(5, -3, 2),
            barnD  = t => new Vector3(5, 0, -2),
            barnDf = t => new Vector3(5, -3, -2),

            // Ceiling
            ceilA = t => new Vector3(-5, f(t), -2),
            ceilB = t => new Vector3(-5, f(t), 2),
            ceilC = t => new Vector3(5, f(t), 2),
            ceilD = t => new Vector3(5, f(t), -2),

            // Pole
            poleP1 = t => new Vector3(poleXCoord(t), 2 + d, d),
            poleP2 = t => new Vector3(poleXCoord(t), 2 + d, -d),
            poleP3 = t => new Vector3(poleXCoord(t), 2 - d, d),
            poleP4 = t => new Vector3(poleXCoord(t), 2 - d, -d),

            poleQ1 = t => new Vector3(poleXCoord(t) + poleLength, 2 + d, d),
            poleQ2 = t => new Vector3(poleXCoord(t) + poleLength, 2 + d, -d),
            poleQ3 = t => new Vector3(poleXCoord(t) + poleLength, 2 - d, d),
            poleQ4 = t => new Vector3(poleXCoord(t) + poleLength, 2 - d, -d);

            Polygon
            barnFloor = new Polygon(new Trajectory[] {barnA, barnB, barnC, barnD}, Color.Red),
            barnAnotherFloor = new Polygon(new Trajectory[] {barnAf, barnBf, barnCf, barnDf}, Color.Red),
            barnSideLeft = new Polygon(new Trajectory[] {barnA, barnB, barnBf, barnAf}, Color.BurlyWood),
            barnSideRight = new Polygon(new Trajectory[] {barnC, barnD, barnDf, barnCf}, Color.BurlyWood),
            barnSideFront = new Polygon(new Trajectory[] {barnA, barnD, barnDf, barnAf}, Color.BurlyWood),
            barnSideBack = new Polygon(new Trajectory[] {barnB, barnC, barnCf, barnBf}, Color.BurlyWood),

            barnLeftWall  = new Polygon(new Trajectory[] {barnA, barnB, ceilB, ceilA}, Color.Aquamarine),
            barnRightWall = new Polygon(new Trajectory[] {barnC, barnD, ceilD, ceilC}, Color.Aquamarine),
            poleLeft   = new Polygon(new Trajectory[] {poleP1, poleP2, poleP4, poleP3}, Color.Green),
            poleRight  = new Polygon(new Trajectory[] {poleQ1, poleQ2, poleQ4, poleQ3}, Color.Green),
            poleFront  = new Polygon(new Trajectory[] {poleP2, poleP4, poleQ4, poleQ2}, Color.Yellow),
            poleBack   = new Polygon(new Trajectory[] {poleP1, poleP3, poleQ3, poleQ1}, Color.Yellow),
            poleTop    = new Polygon(new Trajectory[] {poleP1, poleP2, poleQ2, poleQ1}, Color.Pink),
            poleBottom = new Polygon(new Trajectory[] {poleP3, poleP4, poleQ4, poleQ3}, Color.Pink);

            return new Scenario() {
                Objects = new IScenarioObject[] {
                    barnFloor, barnLeftWall, barnRightWall, barnAnotherFloor, barnSideLeft,
                    barnSideRight, barnSideFront, barnSideBack,
                    poleLeft, poleRight, poleFront, poleBack, poleTop, poleBottom
                },
                MinTime = tMin,
                MaxTime = tMax,
                TimeStep = (tMax - tMin) / 500,
                SpeedOfLight = poleSpeed / Beta,
                LorentzBoostSpeed = PoleReferenceFrame ? (-poleSpeed) : 0
            };
        }
    }
}
