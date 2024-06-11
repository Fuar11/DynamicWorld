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

            if (scene.Contains("house") || scene.Contains("cabin")) result = true;
            if (scene.Contains("whaling") || scene.Contains("basement") || scene.Contains("light")) result = false;

            return result;
        } 

    }
}
