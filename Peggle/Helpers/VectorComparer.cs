using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Helper
{
    class VectorComparer : IComparer<Vector2>
    {
        public enum Axis{x,y}

        Axis sort;

        public VectorComparer(Axis sort)
        {
            this.sort = sort;
        }



        public int Compare(Vector2 a, Vector2 b)
        {
            float aValue;
            float bValue;

            if(sort == Axis.x)
            {
                aValue = a.X;
                bValue = b.X;
            }
            else
            {
                aValue = a.Y;
                bValue = b.Y;
            }

            if(aValue > bValue)
            {
                return 1;
            }
            else if(aValue == bValue)
            {
                return 0;
            }
            else
            {
                return -1;
            }
            
        }
    }
}
