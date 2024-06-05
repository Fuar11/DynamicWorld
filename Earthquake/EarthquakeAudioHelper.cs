using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioMgr;
using Il2Cpp;
using Random = System.Random;

namespace DynamicWorld.Earthquake
{
    internal class EarthquakeAudioHelper
    {

        static Random rand = new Random();

        public static string GetAudioForScene(bool eq, float duration)
        {

            string clip = "";
            string scene = GameManager.m_ActiveScene.ToLowerInvariant();

            //industrial buildings
            if(scene.Contains("dam") || scene.Contains("WhalingWarehouseA") || scene.Contains("MineConcentratorBuilding"))
            {
                return GetIndustrialAudio(eq, duration);
            } //houses with lots of furnishing and items, i.e. kitchens, living rooms
            else if(scene.Contains("CoastalHouse") || scene.Contains("MiltonHouse") || scene.Contains("GreyMothersHouseA"))
            {
                return GetHouseAudio(eq, duration);
            }

            return clip;
        }

        private static string GetIndustrialAudio(bool eq, float duration)
        {
            string clip = "";

            if (eq)
            {
                if (duration <= 12f) clip = "earthquakeIndoorsIndustrialMedium";
                else if (duration > 12f) clip = "earthquakeIndoorsIndustrialLong";
            }
            else
            {
                clip = "tremorIndoorsIndustrial";
            }

            return clip;
        }

        private static string GetHouseAudio(bool eq, float duration)
        {
            string clip = "";

            if (eq)
            {
                int variant = rand.Next(1);

                if (duration <= 11f) clip = "earthquakeIndoorsHouseShort";
                else if (duration > 11f) clip = variant == 0 ? "earthquakeIndoorsHouseLong" : "heavyIndoors";
            }
            else
            {
                clip = "lowRumble";
            }

            return clip;
        }


    }


}
