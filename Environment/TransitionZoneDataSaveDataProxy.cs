using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorld.Environment
{
    internal class TransitionZoneDataSaveDataProxy
    {

        public int ravineTransition;
        public int cinderHillsTransition;
        public int windingRiverCaveTransition;
        public int crumblingHighwayTransition;
        public int forlornMuskegTunnelTransition;
        public int brokenRailroadTunnelTransition;
        public int mountainTownCaveTransition;
        public int bleakInletCaveTransition;
        public int ashCanyonCaveTransition;
        public int blackrockCaveTransition;
        public int hushedRiverValleyCaveTransition;

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
