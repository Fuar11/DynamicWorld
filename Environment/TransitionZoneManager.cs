using MelonLoader.Utils;
using DynamicWorld.Utilities.JSON;
using UnityEngine.AddressableAssets;
using DynamicWorld.Utilities;
using Il2CppSystem;
using Random = System.Random;

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

            Random rand = new Random();

            //check if they're zero first
            ashCanyonCaveTransition = ashCanyonCaveTransition == 0 && Utils.RollChance(40) ? 1 : ashCanyonCaveTransition;
            bleakInletCaveTransition = bleakInletCaveTransition == 0 && Utils.RollChance(40) ? 1 : bleakInletCaveTransition;
            blackrockCaveTransition = blackrockCaveTransition == 0 && Utils.RollChance(70) ? 1 : blackrockCaveTransition;

            if (noMercy)
            {
                //values subject to change 
                ravineTransition = ravineTransition == 0 && Utils.RollChance(70) ? 1 : ravineTransition;
                cinderHillsTransition = cinderHillsTransition == 0 && Utils.RollChance(70) ? 1 : cinderHillsTransition;
                windingRiverCaveTransition = windingRiverCaveTransition == 0 && Utils.RollChance(40) ? 1 : windingRiverCaveTransition;
                forlornMuskegTunnelTransition = forlornMuskegTunnelTransition == 0 && Utils.RollChance(50) ? 1 : forlornMuskegTunnelTransition;
                mountainTownCaveTransition = mountainTownCaveTransition == 0 && Utils.RollChance(40) ? 1 : mountainTownCaveTransition;
                hushedRiverValleyCaveTransition = hushedRiverValleyCaveTransition == 0 && Utils.RollChance(40) ? 1 : hushedRiverValleyCaveTransition;
                crumblingHighwayTransition = crumblingHighwayTransition == 0 && Utils.RollChance(90) ? rand.Next(1, 2) : crumblingHighwayTransition;
                brokenRailroadTunnelTransition = brokenRailroadTunnelTransition == 0 && Utils.RollChance(70) ? 1 : brokenRailroadTunnelTransition;
            }
            else
            {
                if (mountainTownCaveTransition == 0)
                {
                    forlornMuskegTunnelTransition = forlornMuskegTunnelTransition == 0 && Utils.RollChance(50) ? 1 : forlornMuskegTunnelTransition;
                }
                if (forlornMuskegTunnelTransition == 0)
                {
                    mountainTownCaveTransition = mountainTownCaveTransition == 0 && Utils.RollChance(30) ? 1 : mountainTownCaveTransition;
                }

                if (ravineTransition == 0 && cinderHillsTransition == 0)
                {
                    windingRiverCaveTransition = windingRiverCaveTransition == 0 && Utils.RollChance(30) ? 1 : windingRiverCaveTransition;
                }
                if (windingRiverCaveTransition == 0 && cinderHillsTransition == 0)
                {
                    ravineTransition = ravineTransition == 0 && Utils.RollChance(80) ? 1 : ravineTransition;
                }
                if (ravineTransition == 0 && windingRiverCaveTransition == 0)
                {
                    cinderHillsTransition = cinderHillsTransition == 0 && Utils.RollChance(70) ? 1 : cinderHillsTransition;
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

        public void ModifyMineTransition()
        {
            string scene = GameManager.m_ActiveScene;

            if (scene == "HighwayMineTransitionZone")
            {

                if (crumblingHighwayTransition == 1)
                {
                    //disable wood beam
                    GameObject.Find("Art/Geo").transform.GetChild(69).gameObject.active = false;

                    Vector3 position = new Vector3(-93.72f, 0.0126f, -5.175f);
                    Vector3 rotation = new Vector3(-27.54f, 81.652f, -77.40f);

                    SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock24", position, rotation);
                }
                else if(crumblingHighwayTransition == 2)
                {
                    Vector3 position = new Vector3(-51.01f, -3.5f, 7.028f);
                    Vector3 rotation = new Vector3(-173.54f, 30.362f, -137.9f);

                    SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock24", position, rotation);
                }
            }
            else if(scene == "MineTransitionZone" && cinderHillsTransition != 0)
            {
                Vector3 position = new Vector3(-27.77f, -12.35f, 66.1f);
                Vector3 rotation = new Vector3(-153.3f, -71.6f, 60.076f);

                SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock23", position, rotation);
            }

        }

        public void ModifyCaveTransition()
        {
            string scene = GameManager.m_ActiveScene;

            if (scene == "DamCaveTransitionZone" && windingRiverCaveTransition != 0)
            {
                Vector3 position = new Vector3(-49.55f, 4.4f, 76.8f);
                Vector3 rotation = new Vector3(-16.23f, 122.39f, -105.4f);

                SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock23", position, rotation);
            }
            else if (scene == "MountainTownCaveA" && mountainTownCaveTransition != 0)
            {
                Vector3 position = new Vector3(-98.63f, 24.73f, -81.06f);
                Vector3 rotation = new Vector3(303.23f, 243.39f, 197.56f);

                SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock23", position, rotation);
            }
            else if (scene == "CanneryMarshTransitionCave" && bleakInletCaveTransition != 0)
            {
                Vector3 position = new Vector3(-26.9f, 38.08f, 0.263f);
                Vector3 rotation = new Vector3(-238.10f, 9.11f, -279.29f);

                SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock11", position, rotation);
            }
            else if (scene == "BlackrockCaveA")
            {

                if(blackrockCaveTransition == 1)
                {
                    Vector3 position = new Vector3(96.52f, -12.08f, -23.38f);
                    Vector3 rotation = new Vector3(-198.10f, -76.95f, 85.32f);

                    SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock23", position, rotation);
                }
                else if(blackrockCaveTransition == 2)
                {
                    Vector3 position1 = new Vector3(35.83f, -24.20f, 22.38f);
                    Vector3 rotation1 = new Vector3(-201.8f, 64.61f, -170.4f);
                    Vector3 position2 = new Vector3(37.58f, -24.13f, 22.61f);
                    Vector3 rotation2 = new Vector3(-199.8f, 64.97f, -169.19f);

                    SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock11", position1, rotation1);
                    SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock11", position2, rotation2);
                }
            }
            else if (scene == "AshCaveA" && ashCanyonCaveTransition != 0)
            {
                Vector3 position1 = new Vector3(-31.5f, 1.75f, 23.87f);
                Vector3 rotation1 = new Vector3(-58.10f, 51.94f, -269.8f);
                Vector3 position2 = new Vector3(-32.5f, -0.59f, 25.03f);
                Vector3 rotation2 = new Vector3(-148.10f, 75.36f, -72.54f);

                SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock23", position1, rotation1);
                SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock17", position2, rotation2);
            }
            else if (scene == "RiverValleyTransitionCave" && hushedRiverValleyCaveTransition != 0)
            {
                Vector3 position1 = new Vector3(-60f, -7.56f, 55.27f);
                Vector3 rotation1 = new Vector3(232.9f, 13.02f, -103f);
                Vector3 position2 = new Vector3(-59f, -6.8f, 53.83f);
                Vector3 rotation2 = new Vector3(9.9f, 86.02f, -29.73f);

                SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock17", position1, rotation1);
                SceneUtils.InstantiateObjectInScene("OBJ_ModMineRock3", position2, rotation2);
            }

        }

    }
}
