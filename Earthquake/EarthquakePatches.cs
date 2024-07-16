using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using Il2Cpp;
using Random = UnityEngine.Random;

namespace DynamicWorld.Earthquake
{
    internal class EarthquakePatches
    {

        [HarmonyPatch(typeof(SaveGameSlots), nameof(SaveGameSlots.CreateSlot))]

        public class SaveAfterNewGame
        {

            public static void Postfix()
            {
                if (Main.EarthquakeComponent != null)
                {
                    Main.Logger.Log("Scheduling first Earthquake!", ComplexLogger.FlaggedLoggingLevel.Debug);
                    Main.EarthquakeComponent.ScheduleEarthquake();
                }
            }
        }

        [HarmonyPatch(typeof(vp_FPSCamera), nameof(vp_FPSCamera.UpdateEarthQuake))]
        public class EarthquakeCameraChanges
        {

            static private float lastUpdateTime = 0f;

            public static bool Prefix(vp_FPSCamera __instance)
            {

                if (__instance.m_EarthQuakeTime > 0)
                {
                    lastUpdateTime = lastUpdateTime == 0f ? Time.time : lastUpdateTime;

                
                }

                return false;
            }
            public static void Postfix(vp_FPSCamera __instance, ref float time)
            {

                float currentTime = Time.time;
                float elapsedTime = currentTime - lastUpdateTime;

                if (__instance.m_EarthQuakeTime <= 0f)
                {
                    lastUpdateTime = 0f;
                    return;
                }

                __instance.m_EarthQuakeTime -= elapsedTime;
                lastUpdateTime = currentTime;

                if (!__instance.Controller.isGrounded)
                {
                    return;
                }
                Vector3 vector = Vector3.Scale(vp_SmoothRandom.GetVector3CenteredSlow(time, 1f), new Vector3(__instance.m_EarthQuakeMagnitude.x, 0f, 0f)) * Mathf.Min(__instance.m_EarthQuakeTime, 1f);
                float num = 0f;
                if (Random.value < 0.3f)
                {
                    num = Random.Range(0f, __instance.m_EarthQuakeMagnitude.y * 0.35f) * Mathf.Min(__instance.m_EarthQuakeTime, 1f);
                    if (__instance.m_PositionSpring2.State.y >= __instance.m_PositionSpring2.RestState.y)
                    {
                        num = -num;
                    }
                }
                __instance.m_PositionSpring2.AddForce(vector);
                __instance.m_RotationSpring.AddForce(new Vector3(0f, 0f, -vector.x * 2f) * __instance.m_EarthQuakeCameraRollFactor);
                if (__instance.m_CurrentWeapon && __instance.m_CurrentWeapon.GetStartCalled())
                {
                    __instance.m_CurrentWeapon.AddForce(new Vector3(0f, 0f, -vector.z * 0.015f) * __instance.m_EarthQuakeWeaponShakeFactor, new Vector3(num * 2f, -vector.x, vector.x * 2f) * __instance.m_EarthQuakeWeaponShakeFactor);
                }
                __instance.m_PositionSpring2.AddForce(new Vector3(0f, num, 0f));

            }

        }
    }
}
