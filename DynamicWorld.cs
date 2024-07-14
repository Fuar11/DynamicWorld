using AudioMgr;
using ComplexLogger;
using DynamicWorld.Earthquake;
using DynamicWorld.Environment;
using DynamicWorld.Utilities;
using DynamicWorld.Utilities.JSON;
using MelonLoader.Utils;
using UnityEngine.Rendering.PostProcessing;

namespace DynamicWorld
{
    public class Main : MelonMod
    {
        internal static ComplexLogger<Main> Logger = new();
        EarthquakeComponent EarthquakeComponent;
        internal static SaveDataManager SaveDataManager;

        //Earthquake Audio
        public static ClipManager EarthquakeAudio;

        public override void OnInitializeMelon()
        {
            SaveDataManager ??= new();

            if (SaveDataManager == null)
            {
                Logger.Log("SaveDataManager remains null", FlaggedLoggingLevel.Error);
                return;
            }

            if (!Directory.Exists(Path.Combine(MelonEnvironment.ModsDirectory, "DynamicWorld"))) Directory.CreateDirectory(Path.Combine(MelonEnvironment.ModsDirectory, "DynamicWorld"));

            MelonLogger.Msg("Dynamic World is online.");
            Settings.OnLoad();
            Logger ??= new();
        }
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName.ToLowerInvariant().Contains("boot") || sceneName.ToLowerInvariant().Contains("empty")) return;
            if (sceneName.ToLowerInvariant().Contains("menu"))
            {
                Logger.Log("Removing EarthquakeManager since we are in menu scene", FlaggedLoggingLevel.Debug);
                GameObject.Destroy(GameObject.Find("EarthquakeManager"));
                EarthquakeComponent = null;
                return;
            }
           
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
            if(sceneName.ToLowerInvariant() == "lakeregion" || sceneName.ToLowerInvariant() == "marshregion"  || sceneName.ToLowerInvariant() == "tracksregion")
            {
                new TransitionZoneManager().ModifyTunnels();
            }
            if(sceneName.ToLowerInvariant() == "minetransitionzone" || sceneName.ToLowerInvariant() == "highwayminetransitionzone")
            {
                new TransitionZoneManager().ModifyMineTransition();
            }
            if (sceneName.ToLowerInvariant().Contains("cave"))
            {
                new TransitionZoneManager().ModifyCaveTransition();
            }
        }
    
        //DEBUG
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
