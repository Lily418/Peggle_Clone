using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Helper;
using Microsoft.Xna.Framework;

namespace Peggle
{
    static class LevelLoader
    {
        
        public static Level loadXML(Game game, String path)
        {
            XDocument doc = XDocument.Load(path);
            Level level = new Level((Game1)game);

            IEnumerable<XElement> root = doc.Root.Elements();

            foreach (XElement element in root)
            {
                String parentName = Convert.ToString(element.Name).ToLower();
                switch (parentName)
                {
                    case "circletarget":
                        level.addElement(loadCircularTarget(element, game));
                        break;
                    case "curvetarget":
                        level.addElement(loadCurveTarget(element, game));
                        break;
                }
            }

            return level;

        }

        private static CircularTarget loadCircularTarget(XElement element, Game game)
        {
            String positionString = element.Element(XName.Get("Position")).Value;
            String[] positionStringSplit = positionString.Split(',');
            Location location = new Location(new Vector2(positionStringSplit[0].toFloat(), positionStringSplit[1].toFloat()), positionStringSplit[2].toFloat(), positionStringSplit[3].toFloat());

            bool countsTowardsLevelComplete = Convert.ToBoolean(element.Element(XName.Get("LevelCompleteTarget")).Value);


            return new CircularTarget(game, location, countsTowardsLevelComplete);

        }

        private static CurveTarget loadCurveTarget(XElement element, Game game)
        {
            XElement upperCurve = element.Element(XName.Get("UpperCurve"));
            XElement lowerCurve = element.Element(XName.Get("LowerCurve"));

            return new CurveTarget(new CurvedBrick(loadCurve(upperCurve), loadCurve(lowerCurve)), game);
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
