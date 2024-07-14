using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorld.Earthquake
{
    internal class EarthquakeSaveDataProxy
    {
        public float nextEarthquakeTime { get; set; }
        public float lastEarthquakeTime { get; set; }

        public EarthquakeSaveDataProxy(float nextEarthquakeTime, float lastEarthquakeTime)
        {
            this.nextEarthquakeTime = nextEarthquakeTime;
            this.lastEarthquakeTime = lastEarthquakeTime;
        }

        public EarthquakeSaveDataProxy()
        {
          
        }
    }
}
