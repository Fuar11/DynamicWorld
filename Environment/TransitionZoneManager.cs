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
            if (ldp == null)
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
                if (mountainTownCaveTransition == 0)
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

            if (scene == "LakeRegion" && forlornMuskegTunnelTransition != 0)
            {

                Main.Logger.Log("Modifying tunnel to FM", ComplexLogger.FlaggedLoggingLevel.Debug);

                //GameObject tunnelRock1 = Addressables.LoadAssetAsync<GameObject>("TRN_RockGroupMidB_Main").WaitForCompletion();
                GameObject tunnelRock1 = GameObject.Find("TRN_RockGroupMidB_Top_Prefab");

                Vector3 position1 = new Vector3(774.6f, 34.88f, -206.2f);
                Vector3 rotation1 = new Vector3(-7.93f, -87.81f, -169.5f);

                SceneUtils.InstantiateObjectInScene(tunnelRock1, position1, rotation1);

                //GameObject tunnelRock2 = Addressables.LoadAssetAsync<GameObject>("TRN_RockMidl03").WaitForCompletion();
                GameObject tunnelRock2 = GameObject.Find("TRN_RockMidl03_BaseA_Prefab");

                Vector3 position2 = new Vector3(769.69f, 38.02f, -144.2f);
                Vector3 rotation2 = new Vector3(46.604f, -27.95f, -15.067f);

                SceneUtils.InstantiateObjectInScene(tunnelRock2, position2, rotation2);

                GameObject.Find("Design/Scripting/Transitions/Marsh/TransitionZone").active = false;
                GameObject.Find("Design/Scripting/Transitions/Marsh/TransitionContact").active = false;

            }
            else if (scene == "MarshRegion")
            {

                if (forlornMuskegTunnelTransition != 0)
                {
                    GameObject tunnelRock = GameObject.Find("TRN_RockBig02_Top_Prefab");

                    Vector3 position = new Vector3(1772.2f, -129.88f, 1102.2f);
                    Vector3 rotation = new Vector3(142.15f, 84.32f, 49.5f);

                    SceneUtils.InstantiateObjectInScene(tunnelRock, position, rotation);
                }

                if (brokenRailroadTunnelTransition != 0)
                {

                    GameObject tunnelRock = GameObject.Find("TRN_RockGroupMidB_Top_Prefab");

                    Vector3 position = new Vector3(-105.4f, -137.88f, 832.58f);
                    Vector3 rotation = new Vector3(29.36f, 10.27f, 21.25f);

                    SceneUtils.InstantiateObjectInScene(tunnelRock, position, rotation);

                    GameObject.Find("Design/Transitions/Tracks/TransitionZone").active = false;
                    GameObject.Find("Design/Transitions/Tracks/TransitionContact").active = false;

                }
            }
            else if (scene == "TracksRegion" && brokenRailroadTunnelTransition != 0)
            {
                GameObject tunnelRock = GameObject.Find("TRN_RockMid01_Bottom_Prefab");

                Vector3 position = new Vector3(665.99f, 233.6f, 1675.58f);
                Vector3 rotation = new Vector3(246.36f, -55.63f, 283.67f);

                SceneUtils.InstantiateObjectInScene(tunnelRock, position, rotation);

                GameObject.Find("Design/Transitions/MarshRegion/TransitionZone").active = false;
                GameObject.Find("Design/Transitions/MarshRegion/TransitionContact").active = false;
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
