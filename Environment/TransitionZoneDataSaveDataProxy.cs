using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorld.Environment
{
    internal class TransitionZoneDataSaveDataProxy
    {

        public int ravineTransition { get; set; }
        public int cinderHillsTransition { get; set; }
        public int windingRiverCaveTransition { get; set; }
        public int crumblingHighwayTransition { get; set; }
        public int forlornMuskegTunnelTransition { get; set; }
        public int brokenRailroadTunnelTransition { get; set; }
        public int mountainTownCaveTransition { get; set; }
        public int bleakInletCaveTransition { get; set; }
        public int ashCanyonCaveTransition { get; set; }
        public int blackrockCaveTransition { get; set; }
        public int hushedRiverValleyCaveTransition { get; set; }

        public TransitionZoneDataSaveDataProxy(int ravineTransition, int cinderHillsTransition, int windingRiverCaveTransition, int crumblingHighwayTransition, int forlornMuskegTunnelTransition, int brokenRailroadTunnelTransition, int mountainTownCaveTransition, int bleakInletCaveTransition, int ashCanyonCaveTransition, int blackrockCaveTransition, int hushedRiverValleyCaveTransition)
        {
            this.ravineTransition = ravineTransition;
            this.cinderHillsTransition = cinderHillsTransition;
            this.windingRiverCaveTransition = windingRiverCaveTransition;
            this.crumblingHighwayTransition = crumblingHighwayTransition;
            this.forlornMuskegTunnelTransition = forlornMuskegTunnelTransition;
            this.brokenRailroadTunnelTransition = brokenRailroadTunnelTransition;
            this.mountainTownCaveTransition = mountainTownCaveTransition;
            this.bleakInletCaveTransition = bleakInletCaveTransition;
            this.ashCanyonCaveTransition = ashCanyonCaveTransition;
            this.blackrockCaveTransition = blackrockCaveTransition;
            this.hushedRiverValleyCaveTransition = hushedRiverValleyCaveTransition;
        }
    }
}
