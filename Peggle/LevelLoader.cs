using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Helper;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    static class LevelLoader
    {
        
        public static Level loadXML(String path)
        {
            XDocument doc = XDocument.Load(path);
            Level level = new Level();

            IEnumerable<XElement> root = doc.Root.Elements();

            foreach (XElement element in root)
            {
                String parentName = Convert.ToString(element.Name).ToLower();
                switch (parentName)
                {
                    case "circletarget":
                        level.addElement(loadCircularTarget(element));
                        break;
                    case "curvetarget":
                        level.addElement(loadCurveTarget(element));
                        break;
                }
            }

            return level;

        }

        private static CircularTarget loadCircularTarget(XElement element)
        {
            String positionString = element.Element(XName.Get("Position")).Value;
            String[] positionStringSplit = positionString.Split(',');

            Circle location = new Circle(new Vector2(positionStringSplit[0].toFloat(), positionStringSplit[1].toFloat()), positionStringSplit[2].toFloat());

            return new CircularTarget(location, Target.defaultColor);

        }

        private static CurveTarget loadCurveTarget(XElement element)
        {
            XElement upperCurve = element.Element(XName.Get("UpperCurve"));
            XElement lowerCurve = element.Element(XName.Get("LowerCurve"));

            return new CurveTarget(new CurvedBrick(loadCurve(upperCurve), loadCurve(lowerCurve)), Target.defaultColor);
        }

        private static Curve loadCurve(XElement element)
        {
            String p0 = element.Element(XName.Get("p0")).Value;
            String p1 = element.Element(XName.Get("p1")).Value;
            String p2 = element.Element(XName.Get("p2")).Value;

            return new Curve(p0.toVector(), p1.toVector(), p2.toVector());
        }
    }
}
