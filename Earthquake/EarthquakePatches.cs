using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using Il2Cpp;

namespace DynamicWorld.Earthquake
{
    internal class EarthquakePatches
    {

        [HarmonyPatch(typeof(FoodItem), nameof(FoodItem.Awake))]

        public class FoodItem_Awake
        {

            private static void Postfix(FoodItem __instance)
            {
              
            }

        }

    }
}
