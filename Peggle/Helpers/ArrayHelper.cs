using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    //For treating flat lists like 2 dimensional arrays
    static class ArrayHelper
    {
        public static int twoIndexesToOne(int row, int col, int width)
        {
            return (width * row) + col;
        }

        public static int[] oneIndexToTwo(int index, int width)
        {
            int[] indexes = new int[2];
            indexes[0] = index / width;
            indexes[1] = index * width;

            return indexes;
        }
    }
}
