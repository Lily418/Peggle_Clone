﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Helper;

namespace Peggle
{
    public class CurvedBrick
    {
        public Curve upperCurve { private set; get; }
        public Curve lowerCurve { private set; get; }
        public QuadCollection quads { private set; get; }

        public CurvedBrick(Curve upperCurve, Curve lowerCurve)
        {
            quads = toQuads(upperCurve, lowerCurve);
            this.upperCurve = upperCurve;
            this.lowerCurve = lowerCurve;
        }

        private QuadCollection toQuads(Curve upperCurve, Curve lowerCurve)
        {

            QuadCollection curveQuads = new QuadCollection();

            float usedInterval = 0.15f;

            for (float i = 0.0f; i <= 1.0f; i += usedInterval)
            {
                Vector2 p0   = upperCurve.getPoint(i);
                Vector2 p1   = upperCurve.getPoint(i + usedInterval);
                Vector2 p2   = lowerCurve.getPoint(i);
                Vector2 p3   = lowerCurve.getPoint(i + usedInterval);

                curveQuads.addQuad(Quad.organiseQuadPoints(new Vector2[]{p0, p1, p2, p3}));
            }

            return curveQuads;
        }
    }
}
