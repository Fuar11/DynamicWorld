using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2Cpp;
using ComplexLogger;

namespace DynamicWorld.Utilities
{
    internal class SceneUtils
    {

        public static bool IsSceneWoodBuilding(string scene)
        {
            scene = scene.ToLowerInvariant();
            bool result = false;

            if (scene.Contains("house") || scene.Contains("cabin") || scene.Contains("campoffice") || scene.Contains("church") || scene.Contains("lodge") || scene.Contains("safehouse") || scene.Contains("barn") || scene.Contains("community") || scene.Contains("ruralstore")) result = true;
            if (scene.Contains("whaling") || scene.Contains("basement") || scene.Contains("light")) result = false;

            return result;
        } 

        public static bool IsSceneConcreteBuilding(string scene)
        {
            scene = scene.ToLowerInvariant();
            bool result = false;

            if (scene.Contains("conveniencestore") || scene.Contains("quonset") || scene.Contains("lighthouse") || scene.Contains("postoffice") || scene.Contains("bank") || scene.Contains("bunker") || scene.Contains("prepper") || scene.Contains("radiocontrolhut") || scene.Contains("blackrockinterior") || scene.Contains("basement")) result = true;

            return result;
        }

        public static bool IsSceneIndustrialBuilding(string scene)
        {
            scene = scene.ToLowerInvariant();
            bool result = false;

            if (scene.Contains("dam") || scene.Contains("whalingwarehousea") || scene.Contains("concentrator") || scene.Contains("maintenanceshed") || scene.Contains("hangar") || scene.Contains("ship") || scene.Contains("blackrockpowerplant")) result = true;
            if (scene.Contains("cave") || scene.Contains("river") || scene.Contains("trailer")) result = false;

            return result;
        }

        public static bool IsSceneUnderground(string scene)
        {
            scene = scene.ToLowerInvariant();
            bool result = false;

            if (scene.Contains("cave") || scene.Contains("mine") || scene.Contains("miningregionmine")) result = true;

            return result;

        }

    }
}
