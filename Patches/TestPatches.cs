﻿using Il2CppNodeCanvas.Tasks.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DynamicWorld.Patches
{
    internal class TestPatches
    {

        [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
        public class Test
        {
            public static void Postfix(GearItem __instance)
            {
               if(__instance.gameObject.GetComponent<TestComponent>() == null)
                {
                    __instance.gameObject.AddComponent<TestComponent>();
                }
            }

        }

       

       

    }
}