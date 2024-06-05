using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = System.Random;

namespace DynamicWorld.Utilities
{
    internal class FloatUtilities
    {

        private static Random random = new Random();
        public static float GetRandomFloat(float minValue, float maxValue)
        {
            // Ensure minValue is less than maxValue
            if (minValue > maxValue)
            {
                throw new ArgumentException("minValue should be less than maxValue");
            }

            double range = maxValue - minValue;
            double sample = random.NextDouble(); 
            double scaled = (sample * range) + minValue;

            return (float)scaled;
        }

    }
}
