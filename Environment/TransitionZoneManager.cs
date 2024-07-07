using MelonLoader.Utils;
using DynamicWorld.Utilities.JSON;

namespace DynamicWorld.Environment
{
    internal class TransitionZoneManager
    {

        int ravineTransition;
        int cinderHillsTransition;
        int windingRiverCaveTransition;
        int crumblingHighwayTransition;
        int forlornMuskegTunnelTransition;
        int brokenRailroadTunnelTransition;
        int mountainTownCaveTransition;
        int bleakInletCaveTransition;
        int ashCanyonCaveTransition;
        int blackrockCaveTransition;
        int hushedRiverValleyCaveTransition;

        bool noMercy = Settings.Instance.hardLock == Settings.Active.Enabled ? true : false;

        public TransitionZoneManager()
        {
            LoadOrInitData();
        }

        private void LoadOrInitData()
        {

            TransitionZoneDataSaveDataProxy? ldp = JsonFile.Load<TransitionZoneDataSaveDataProxy>($"{MelonEnvironment.ModsDirectory}/DynamicWorld.json", true);

            //if data from load is null
            if(ldp == null)
            {
                ravineTransition = 0; //three way
                cinderHillsTransition = 0; //three way
                windingRiverCaveTransition = 0; //three way
                crumblingHighwayTransition = 0; //one way
                forlornMuskegTunnelTransition = 0; //two way
                brokenRailroadTunnelTransition = 0; //one way
                mountainTownCaveTransition = 0; //two way
                bleakInletCaveTransition = 0;
                ashCanyonCaveTransition = 0;
                blackrockCaveTransition = 0;
                hushedRiverValleyCaveTransition = 0; //one way
            }
            else
            {
                ravineTransition = ldp.ravineTransition;
                cinderHillsTransition = ldp.cinderHillsTransition;
                windingRiverCaveTransition = ldp.windingRiverCaveTransition;
                crumblingHighwayTransition = ldp.crumblingHighwayTransition;
                forlornMuskegTunnelTransition = ldp.forlornMuskegTunnelTransition;
                brokenRailroadTunnelTransition = ldp.brokenRailroadTunnelTransition;
                mountainTownCaveTransition = ldp.mountainTownCaveTransition;
                bleakInletCaveTransition = ldp.bleakInletCaveTransition;
                ashCanyonCaveTransition = ldp.ashCanyonCaveTransition;
                blackrockCaveTransition = ldp.blackrockCaveTransition;
                hushedRiverValleyCaveTransition = ldp.hushedRiverValleyCaveTransition;
            }
        }

        private void SaveData()
        {

            Main.Logger.Log($"Saving Ravine: {ravineTransition}", ComplexLogger.FlaggedLoggingLevel.Debug);
            Main.Logger.Log($"Saving Cinder Hills: {cinderHillsTransition}", ComplexLogger.FlaggedLoggingLevel.Debug);
            Main.Logger.Log($"Saving Forlorn Muskeg Tunnel: {forlornMuskegTunnelTransition}", ComplexLogger.FlaggedLoggingLevel.Debug);
            Main.Logger.Log($"Saving Winding River Cave: {windingRiverCaveTransition}", ComplexLogger.FlaggedLoggingLevel.Debug);
            Main.Logger.Log($"Saving Crumbling Highway Mine: {crumblingHighwayTransition}", ComplexLogger.FlaggedLoggingLevel.Debug);
            Main.Logger.Log($"Saving Mountain Town Cave: {mountainTownCaveTransition}", ComplexLogger.FlaggedLoggingLevel.Debug);
            Main.Logger.Log($"Saving Broken Railroad Tunnel: {brokenRailroadTunnelTransition}", ComplexLogger.FlaggedLoggingLevel.Debug);
            Main.Logger.Log($"Saving Blackrock Cave: {blackrockCaveTransition}", ComplexLogger.FlaggedLoggingLevel.Debug);
            Main.Logger.Log($"Saving Ash Canyon Cave: {ashCanyonCaveTransition}", ComplexLogger.FlaggedLoggingLevel.Debug);

            Main.Logger.Log($"Saving data!", ComplexLogger.FlaggedLoggingLevel.Debug);
            TransitionZoneDataSaveDataProxy sdp = new TransitionZoneDataSaveDataProxy(ravineTransition, cinderHillsTransition, windingRiverCaveTransition, crumblingHighwayTransition, forlornMuskegTunnelTransition, brokenRailroadTunnelTransition, mountainTownCaveTransition, bleakInletCaveTransition, ashCanyonCaveTransition, blackrockCaveTransition, hushedRiverValleyCaveTransition);
            JsonFile.Save<TransitionZoneDataSaveDataProxy>($"{MelonEnvironment.ModsDirectory}/DynamicWorld.json", sdp);
        }

        public void ResetSaveData()
        {
            ravineTransition = 0; 
            cinderHillsTransition = 0; 
            windingRiverCaveTransition = 0;
            crumblingHighwayTransition = 0;
            forlornMuskegTunnelTransition = 0;
            brokenRailroadTunnelTransition = 0; 
            mountainTownCaveTransition = 0;
            bleakInletCaveTransition = 0;
            ashCanyonCaveTransition = 0;
            blackrockCaveTransition = 0;
            hushedRiverValleyCaveTransition = 0;

            SaveData();
        }

        public void RollTransitionZones()
        {
             
            //check if they're zero first
            ashCanyonCaveTransition = ashCanyonCaveTransition == 0 && Utils.RollChance(40) ? 1 : ashCanyonCaveTransition;
            bleakInletCaveTransition = bleakInletCaveTransition == 0 && Utils.RollChance(40) ? 1 : bleakInletCaveTransition;
            blackrockCaveTransition = blackrockCaveTransition == 0 && Utils.RollChance(40) ? 1 : blackrockCaveTransition;

            if (noMercy)
            {
                //values subject to change 
                ravineTransition = ravineTransition == 0 && Utils.RollChance(40) ? 1 : ravineTransition;
                cinderHillsTransition = cinderHillsTransition == 0 && Utils.RollChance(40) ? 1 : cinderHillsTransition;
                windingRiverCaveTransition = windingRiverCaveTransition == 0 && Utils.RollChance(40) ? 1 : windingRiverCaveTransition;
                forlornMuskegTunnelTransition = forlornMuskegTunnelTransition == 0 && Utils.RollChance(40) ? 1 : forlornMuskegTunnelTransition;
                mountainTownCaveTransition = mountainTownCaveTransition == 0 && Utils.RollChance(40) ? 1 : mountainTownCaveTransition;
                hushedRiverValleyCaveTransition = hushedRiverValleyCaveTransition == 0 && Utils.RollChance(40) ? 1 : hushedRiverValleyCaveTransition;
                crumblingHighwayTransition = crumblingHighwayTransition == 0 && Utils.RollChance(30) ? 1 : crumblingHighwayTransition;
                brokenRailroadTunnelTransition = brokenRailroadTunnelTransition == 0 && Utils.RollChance(25) ? 1 : brokenRailroadTunnelTransition;
            }
            else
            {
                if(mountainTownCaveTransition == 0) 
                {
                    forlornMuskegTunnelTransition = forlornMuskegTunnelTransition == 0 && Utils.RollChance(40) ? 1 : forlornMuskegTunnelTransition;
                }
                if (forlornMuskegTunnelTransition == 0) 
                {
                    mountainTownCaveTransition = mountainTownCaveTransition == 0 && Utils.RollChance(40) ? 1 : mountainTownCaveTransition;
                }

                if (ravineTransition == 0 && cinderHillsTransition == 0)
                {
                    windingRiverCaveTransition = windingRiverCaveTransition == 0 && Utils.RollChance(40) ? 1 : windingRiverCaveTransition;
                }
                if (windingRiverCaveTransition == 0 && cinderHillsTransition == 0) 
                {
                    ravineTransition = ravineTransition == 0 && Utils.RollChance(40) ? 1 : ravineTransition;
                }
                if (ravineTransition == 0 && windingRiverCaveTransition == 0)
                {
                    cinderHillsTransition = cinderHillsTransition == 0 && Utils.RollChance(40) ? 1 : cinderHillsTransition;
                }
            }

          

            SaveData();
        }

        public void ModifyRavineTransition()
        {

            if (ravineTransition != 1) return;

            GameObject treeBridge = GameObject.Find("OBJ_TreeCedarFelledC_Prefab");
            treeBridge.active = false;
            
            GameObject snowPatch = GameObject.Find("TRN_SnowPatchMedD_Prefab (1)");
            snowPatch.active = false;

        }

        public void ModifyMineTransition(string scene)
        {

        }

        public void ModifyCaveTransition(string scene)
        {

        }
      

    }
}
