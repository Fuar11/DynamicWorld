using MelonLoader.Utils;
using DynamicWorld.Utilities.JSON;
using UnityEngine.AddressableAssets;
using DynamicWorld.Utilities;

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

        public void ModifyTunnels()
        {
            Main.Logger.Log("Modifying the tunnels", ComplexLogger.FlaggedLoggingLevel.Debug);

            string scene = GameManager.m_ActiveScene;

            if(scene == "LakeRegion" && forlornMuskegTunnelTransition != 0)
            {

                Main.Logger.Log("Modifying tunnel to FM", ComplexLogger.FlaggedLoggingLevel.Debug);

                //GameObject tunnelRock1 = Addressables.LoadAssetAsync<GameObject>("TRN_RockGroupMidB_Main").WaitForCompletion();
                GameObject tunnelRock1 = GameObject.Find("TRN_RockGroupMidB_Top_Prefab");
                
                Vector3 position1 = new Vector3(774.6f, 34.88f, -206.2f);
                Vector3 rotation1 = new Vector3(-7.93f, -87.81f, -169.5f);

                SceneUtils.InstantiateObjectInScene(tunnelRock1, position1, rotation1);

                //pos - x: 736.88, y: 42.88, z: -155.4
                //rot - x: 185.98, y: -87.34, z: -174
                //mesh TRN_Rock Group Mid B_Base Snow
                //mat 1 TRN_Snow_A02
                //mat 2 TRN_Rock07_Win_01

                //GameObject tunnelRock2 = Addressables.LoadAssetAsync<GameObject>("TRN_RockMidl03").WaitForCompletion();
                GameObject tunnelRock2 = GameObject.Find("TRN_RockMidl03_BaseA_Prefab");

                Vector3 position2 = new Vector3(769.69f, 38.02f, -144.2f);
                Vector3 rotation2 = new Vector3(46.604f, -27.95f, -15.067f);

                SceneUtils.InstantiateObjectInScene(tunnelRock2, position2, rotation2);

                //pos - x: 769.69, y: 38.02, z: -144.2
                //rot - x: 46.604, y: -27.95, z: -15.067
                //mesh TRN_Rock Midl 03_Base A_LOD0 unsure about this
                //mat 1 TRN_Snow_A02
                //mat 2 TRN_Rock07_Win_01
            }
            else if (scene == "TracksRegion" && brokenRailroadTunnelTransition != 0)
            {
                //nothing yet
            }


        }

        public void ModifyMineTransition(string scene)
        {

        }

        public void ModifyCaveTransition(string scene)
        {

        }
      

    }
}
