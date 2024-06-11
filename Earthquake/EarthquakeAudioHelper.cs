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

            if(!eq) new PlayAndFade(playerShot, cm.GetClip(GetBaseAudioForScene(duration)), duration, duration * 0.25f);

            if (eq)
            {
                //complementary audio on objects in scene

                Dictionary<GameObject, Tuple<string, Setting>> pairs = GetAudioObjectClipPairs(duration);

                foreach (KeyValuePair<GameObject, Tuple<string, Setting>> kpv in pairs)
                {
                    Shot shot = kpv.Key.gameObject.GetComponent<Shot>();
                    if (shot == null)
                    {
                        Main.Logger.Log("Shot doesn't exist, creating", ComplexLogger.FlaggedLoggingLevel.Debug);
                        shot = AudioMaster.CreateShot(kpv.Key, AudioMaster.SourceType.SFX);
                    }
                    shot.ApplySettings(kpv.Value.Item2);

                    shot._audioSource.volume = 1f;
                    new PlayAndFade(shot, cm.GetClip(kpv.Value.Item1), duration, duration * 0.1f);
                }
            }
        }


        private static Dictionary<GameObject, Tuple<string, Setting>> GetAudioObjectClipPairs(float duration)
        {
            Dictionary<GameObject, Tuple<string, Setting>> pairs = new Dictionary<GameObject, Tuple<string, Setting>>();

            string scene = GameManager.m_ActiveScene;

            if (scene.Contains("Dam"))
            {
                GameObject sourceObj = GameObject.Find("Root/Art/Geo_A/BaseGeo/OBJ_IndustrialLampA_Prefab");

                Setting settings = SettingMaster.NewSetting(AudioMaster.SourceType.SFX, 1f, 1f, 1f, 200f, 30f, 1f, 1f, 0f, false, AudioRolloffMode.Linear, null, 1);

                pairs.Add(sourceObj, Tuple.Create(GetIndustrialAudio(duration), settings));
            }
            else if (scene.Contains("WhalingWarehouseA"))
            {
                GameObject sourceObj = GameObject.Find("Root/Art/Structure/STR_SteelBeamB_LOD0");
                Setting settings = SettingMaster.NewSetting(AudioMaster.SourceType.SFX, 1f, 1f, 1f, 100f, 0f, 1f, 1f, 0f, false, AudioRolloffMode.Linear, null, 1);
                pairs.Add(sourceObj, Tuple.Create(GetIndustrialAudio(duration), settings));
            }
            else if (scene.Contains("CoastalHouse") || scene.Contains("MiltonHouse") || scene.Contains("GreyMothersHouseA"))
            {
                GameObject sourceObj = GameObject.Find("OBJ_KitchenCabinetDoorC");
                Setting settings = SettingMaster.NewSetting(AudioMaster.SourceType.SFX, 1f, 1f, 1f, 100f, 0f, 1f, 1f, 0f, false, AudioRolloffMode.Linear, null, 1);
                pairs.Add(sourceObj, Tuple.Create(GetHouseAudio(duration), settings));
            }


            return pairs;
        }


        //change this to have different base audios for specific scenes

        private static string GetBaseAudioForScene(float duration)
        {
            return SceneUtils.IsSceneWoodBuilding(GameManager.m_ActiveScene) ? "lowRumbleInteriorWood" : "lowRumble";
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
    }


}
