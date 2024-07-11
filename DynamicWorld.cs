using AudioMgr;
using ComplexLogger;
using DynamicWorld.Earthquake;
using DynamicWorld.Environment;
using UnityEngine.Rendering.PostProcessing;

namespace DynamicWorld
{
    public class Main : MelonMod
    {
        internal static ComplexLogger<Main> Logger = new();
        EarthquakeComponent EarthquakeComponent;

        //Earthquake Audio
        public static ClipManager EarthquakeAudio;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Dynamic World is online.");
            Settings.OnLoad();
            Logger ??= new();
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName.ToLowerInvariant().Contains("menu") || sceneName.ToLowerInvariant().Contains("boot") || sceneName.ToLowerInvariant().Contains("empty")) return;

            if (!sceneName.Contains("_SANDBOX") && !sceneName.Contains("_DLC") && !sceneName.Contains("_WILDLIFE"))
            {
                GameObject EarthquakeManager = new() { name = "EarthquakeManager", layer = vp_Layer.Default };
                UnityEngine.Object.Instantiate(EarthquakeManager, GameManager.GetVpFPSPlayer().transform);
                GameObject.DontDestroyOnLoad(EarthquakeManager);
                EarthquakeComponent ??= EarthquakeManager.AddComponent<EarthquakeComponent>();
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName.ToLowerInvariant().Contains("menu"))
            {
                EarthquakeAudio = AudioMaster.NewClipManager();
                EarthquakeAudio.LoadClipsFromDir("DynamicWorld/Audio", ClipManager.LoadType.Compressed);
            }

            if(sceneName.ToLowerInvariant() == "ravinetransitionzone")
            {
                new TransitionZoneManager().ModifyRavineTransition();
            }
            if(sceneName.ToLowerInvariant() == "lakeregion" || sceneName.ToLowerInvariant() == "tracksregion")
            {
                new TransitionZoneManager().ModifyTunnels();
            }
        }

        public override void OnUpdate()
        {
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.Keypad9))
            {
                EarthquakeComponent.DoTremor();
            }
            else if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.Keypad8))
            {
                EarthquakeComponent.DoEarthquake();
            }

            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.KeypadEnter))
            {
                TransitionZoneManager tmz = new TransitionZoneManager();
                tmz.ResetSaveData();
            }


        }

    }
}
