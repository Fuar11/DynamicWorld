using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using AudioMgr;
using DynamicWorld.Utilities;
using Il2Cpp;
using Random = System.Random;
using System.Collections;
using Il2CppNodeCanvas.Tasks.Actions;
using UnityEngine;

namespace DynamicWorld.Earthquake
{
    internal class EarthquakeAudioHelper
    {
        static Random rand = new Random();

        public static void PlayAudio(bool eq, float duration, Shot playerShot, ClipManager cm)
        {
           
            playerShot._audioSource.volume = 1f;
            new Utilities.PlayAndFade(playerShot, cm.GetClip(GetBaseAudioForScene(duration)), duration, duration * 0.25f);

            if (eq)
            {
                //complementary audio on objects in scene

                Dictionary<GameObject, Tuple<string, Setting>> pairs = GetAudioObjectClipPairs(duration);

                foreach (KeyValuePair<GameObject, Tuple<string, Setting>> kpv in pairs)
                {
                    Shot shot = kpv.Key.gameObject.GetComponent<Shot>();
                    if (shot == null)
                    {
                        shot = AudioMaster.CreateShot(kpv.Key, AudioMaster.SourceType.SFX);
                    }
                    shot.ApplySettings(kpv.Value.Item2);

                    shot._audioSource.volume = 1f;
                    new Utilities.PlayAndFade(shot, cm.GetClip(kpv.Value.Item1), duration, duration * 0.1f);
                }
            }
        }


        private static Dictionary<GameObject, Tuple<string, Setting>> GetAudioObjectClipPairs(float duration)
        {
            Dictionary<GameObject, Tuple<string, Setting>> pairs = new Dictionary<GameObject, Tuple<string, Setting>>();

            string scene = GameManager.m_ActiveScene;

            if(scene == "LakeRegion" || scene == "RuralRegion" || scene == "MountainTownRegion")
            {

                Setting settings = SettingMaster.NewSetting(AudioMaster.SourceType.SFX, 1f, 1f, 1f, 200f, 30f, 1f, 1f, 0f, false, AudioRolloffMode.Linear, null, 1);

                GameObject radioTower = scene == "LakeRegion" ? GameObject.Find("STR_RadioTowerA_Prefab (1)") : GameObject.Find("STR_RadioTowerA_Prefab");
                               
                pairs.Add(radioTower, Tuple.Create("metal1", settings));

            }
            if (scene == "MarshRegion")
            {
                Setting settings = SettingMaster.NewSetting(AudioMaster.SourceType.SFX, 1f, 1f, 1f, 200f, 30f, 1f, 1f, 0f, false, AudioRolloffMode.Linear, null, 1);

                GameObject radioTowerN = GameObject.Find("RadioTowerN");
                GameObject radioTowerSW = GameObject.Find("RadioTowerSW");
                GameObject radioTowerSE = GameObject.Find("RadioTowerSE");

                pairs.Add(radioTowerN, Tuple.Create("metal1", settings));
                pairs.Add(radioTowerSW, Tuple.Create("metal1", settings));
                pairs.Add(radioTowerSE, Tuple.Create("metal1", settings));
            }
            if (scene == "CanneryRegion")
            {
                GameObject sourceObj = GameObject.Find("STR_RadioTowerA_Aurora_Prefab");

                Setting settings = SettingMaster.NewSetting(AudioMaster.SourceType.SFX, 1f, 1f, 1f, 200f, 30f, 1f, 1f, 0f, false, AudioRolloffMode.Linear, null, 1);

                pairs.Add(sourceObj, Tuple.Create("metal1", settings));
            }



            if (scene.Contains("Dam"))
            {
                GameObject sourceObj = GameObject.Find("Root/Art/Geo_A/BaseGeo/OBJ_IndustrialLampA_Prefab");

                Setting settings = SettingMaster.NewSetting(AudioMaster.SourceType.SFX, 1f, 1f, 1f, 200f, 30f, 1f, 1f, 0f, false, AudioRolloffMode.Linear, null, 1);

                pairs.Add(sourceObj, Tuple.Create(GetIndustrialAudio(duration), settings));
            }
            else if (scene.Contains("WhalingWarehouseA"))
            {
                GameObject sourceObj = GameObject.Find("Root/Art/Warehouse Props/OBJ_IndustrialLampA_Prefab");
                Setting settings = SettingMaster.NewSetting(AudioMaster.SourceType.SFX, 1f, 1f, 1f, 100f, 0f, 1f, 1f, 0f, false, AudioRolloffMode.Linear, null, 1);
                pairs.Add(sourceObj, Tuple.Create(GetIndustrialAudio(duration), settings));
            }
            else if (scene.Contains("CoastalHouse") || scene.Contains("MiltonHouse") || scene.Contains("GreyMothersHouseA") || scene.Contains("FarmHouse"))
            {
                GameObject sourceObj = GameObject.Find("OBJ_KitchenCabinetDoorC");
                Setting settings = SettingMaster.NewSetting(AudioMaster.SourceType.SFX, 1f, 1f, 1f, 100f, 0f, 1f, 1f, 0f, false, AudioRolloffMode.Linear, null, 1);
                pairs.Add(sourceObj, Tuple.Create(GetHouseAudio(duration), settings));
            }
            else if (scene.Contains("Quonset"))
            {

                GameObject storeShelfA = GameObject.Find("OBJ_StoreShelfA_Prefab");
                GameObject storeShelfB = GameObject.Find("OBJ_StoreShelfB_Prefab");
                GameObject storeShelfC = GameObject.Find("OBJ_StoreShelfC_Prefab");

                GameObject fridge1 = GameObject.Find("INTERACTIVE_BrandSodaFridge");
                GameObject fridge2 = GameObject.Find("INTERACTIVE_MountainSodaFridge");

                GameObject quonsetCeiling = GameObject.Find("OBJ_IndustrialLampA");

                Setting settings = SettingMaster.NewSetting(AudioMaster.SourceType.SFX, 1f, 1f, 1f, 20f, 1f, 1f, 1f, 0f, false, AudioRolloffMode.Linear, null, 1);

                pairs.Add(storeShelfA, Tuple.Create("storeShelves", settings));
                pairs.Add(storeShelfB, Tuple.Create("storeShelves", settings));
                pairs.Add(storeShelfC, Tuple.Create("storeShelves", settings));

                pairs.Add(fridge1, Tuple.Create("bottles", settings));
                pairs.Add(fridge2, Tuple.Create("bottles", settings));

                pairs.Add(quonsetCeiling, Tuple.Create("metal1", settings));
            }
            else if (scene.Contains("ConvenienceStoreA"))
            { 

                GameObject storeShelfC = GameObject.Find("OBJ_StoreShelfC_Prefab");
                GameObject storeShelfD = GameObject.Find("OBJ_StoreShelfD_Prefab");
                
                GameObject fridge = GameObject.Find("INTERACTIVE_BrandBSodaFridgeStore");

                Setting settings = SettingMaster.NewSetting(AudioMaster.SourceType.SFX, 1f, 1f, 1f, 20f, 1f, 1f, 1f, 0f, false, AudioRolloffMode.Linear, null, 1);

                pairs.Add(storeShelfC, Tuple.Create("genericIndoors", settings));
                pairs.Add(storeShelfD, Tuple.Create("genericIndoors", settings));
                pairs.Add(fridge, Tuple.Create("bottles", settings));
            }



            return pairs;
        }

        private static string GetBaseAudioForScene(float duration)
        {
            string clip = "lowRumbleLong";

            if (SceneUtils.IsSceneWoodBuilding(GameManager.m_ActiveScene)) return "woodInteriorRumble";
            //else if (SceneUtils.IsSceneIndustrialBuilding(GameManager.m_ActiveScene)) return "lowRumbleLong";
            //else if (SceneUtils.IsSceneConcreteBuilding(GameManager.m_ActiveScene)) return "lowRumbleLong";
            else if (SceneUtils.IsSceneUnderground(GameManager.m_ActiveScene)) return GetCaveAudio(duration);
            else return clip;
        }

        private static string GetIndustrialAudio(float duration)
        {
            string clip = "";

            if (duration <= 12f) clip = "genericIndustrial";
            else if (duration > 12f) clip = "heavyIndustrial";

            return clip;
        }

        private static string GetHouseAudio(float duration)
        {
            string clip = "";

            int variant = rand.Next(1);

            if (duration <= 11f) clip = "genericIndoors";
            else if (duration > 11f) clip = variant == 0 ? "genericIndoors2" : "heavyIndoors";

            return clip;
        }

        private static string GetCaveAudio(float duration)
        {
            if (duration <= 9) return "caveInteriorRumble";
            else if (duration >= 9 && duration <= 13) return "caveInteriorRumble2";
            else return "caveInteriorRumble2"; //don't have anything longer for now
        }

    }


}
