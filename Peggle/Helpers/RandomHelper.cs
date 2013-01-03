using System;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Helper
{
    static class RandomHelper
    {
        readonly static Random random       = new Random();

        public static Random getRandom()
        {
            return random;
        }

        public static Color randomBasicColor()
        {
            switch (random.Next(4))
            {
                case 0:  return Color.Red;
                case 1:  return Color.Blue;
                case 2:  return Color.Green;
                default: return Color.Yellow;
            }
        }

        public static byte randomByte()
        {
            return (byte)random.Next(256);
        }

        public static float randomNormalDistributedFloat()
        {
            int distributionSubset = random.Next(100);

            if (distributionSubset < 2)                  
            {
                return randomFloat(0.0f, 0.1f);
            }
            else if (distributionSubset < 7)             
            {
                return randomFloat(0.1f, 0.2f);
            }
            else if (distributionSubset < 15)            
            {
                return randomFloat(0.2f, 0.3f);
            }
            else if (distributionSubset < 29)            
            {
                return randomFloat(0.3f, 0.4f);
            }
            else if (distributionSubset < 69)           
            {
                return randomFloat(0.4f, 0.6f);
            }
            else if(distributionSubset < 83)               
            {
                return randomFloat(0.6f, 0.7f);
            }
            else if (distributionSubset < 91)         
            {
                return randomFloat(0.7f, 0.8f);
            }
            else if (distributionSubset < 98)          
            {
                return randomFloat(0.8f, 0.9f);
            }
            else if (distributionSubset < 100)         
            {
                return randomFloat(0.9f, 1f);
            }
            else
            {
                Debug.Assert(true, "Distribution subset did not fall in expected range");
                return randomFloat(0f, 1f);
            }
            

        }

        public static float randomFloat(float min, float max)
        {
            return (float)random.NextDouble() * (max - min) + min;
        }

        


    }
}
