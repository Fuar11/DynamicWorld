using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicWorld.Utilities;
using Il2Cpp;
using MelonLoader;
using AudioMgr;
using static System.Random;
using Random = System.Random;
using DynamicWorld.Environment;
using MelonLoader.Utils;
using DynamicWorld.Utilities.JSON;

namespace DynamicWorld.Earthquake
{
    [RegisterTypeInIl2Cpp]
    internal class EarthquakeComponent : MonoBehaviour
    {
        //SDP
        public EarthquakeSaveDataProxy? SaveDataProxy;

        //in days
        float minTimeToEarthquake = Settings.Instance.eqMinTime;
        float maxTimeToEarthquake = Settings.Instance.eqMaxTime;

        float nextEarthquakeTime;
        float lastEarthquakeTime;

        //in seconds
        float minEarthquakeDuration = 10f;
        float maxEarthquakeDuration = 20f;

        ClipManager clips = Main.EarthquakeAudio;
        Shot player;

        public void Awake()
        {
            player = AudioMaster.CreatePlayerShot(AudioMaster.SourceType.SFX);
            VolumeMaster.onVolumeChange -= player.ResetVolume;
        }

        public void Start()
        {
            LoadOrInitData();
        }

        private void LoadOrInitData()
        {
            SaveDataProxy ??= Main.SaveDataManager?.Load();

            if(SaveDataProxy == null)
            {
                Main.Logger.Log("Save Data is null... Creating SDP", ComplexLogger.FlaggedLoggingLevel.Debug);
                SaveDataProxy = new();
            }
            else
            {
                nextEarthquakeTime = SaveDataProxy.nextEarthquakeTime;
                lastEarthquakeTime = SaveDataProxy.lastEarthquakeTime;
            }
        }

        public void Update()
        {
            float currentTime = GameManager.GetTimeOfDayComponent().GetHoursPlayedNotPaused() / 24f;

            if (currentTime == nextEarthquakeTime && nextEarthquakeTime != 0)
            {
                lastEarthquakeTime = nextEarthquakeTime;
                DoEarthquake();
            }
            else 
            {

                float timeForTremorGuaranteed = FloatUtilities.GetRandomFloat(0.07f, 0.25f);

                //guaranteed minor tremors before and after major earthquake by a few hours
                if(currentTime == nextEarthquakeTime - timeForTremorGuaranteed || currentTime == lastEarthquakeTime + timeForTremorGuaranteed)
                {
                    DoTremor();
                }

                //random tremors
                if(Utils.RollChance(0.0005f)){
                    DoTremor();
                }

            }
        }

        public void DoEarthquake(float x = 0.95f, float y = 0.1f, float weaponSway = 0.1f, float rotation = 0.4f)
        {
     
            float duration = FloatUtilities.GetRandomFloat(minEarthquakeDuration, maxEarthquakeDuration);

            vp_FPSCamera cam = GameManager.GetVpFPSCamera();
            cam.DoEarthQuake(x, y, duration, weaponSway, rotation);


            //audio
            EarthquakeAudioHelper.PlayAudio(true, duration, player, clips);

            TransitionZoneManager tzm = new TransitionZoneManager();
            tzm.RollTransitionZones();

            ScheduleEarthquake();
        }

        //minor tremor
        public void DoTremor(float x = 0.4f, float y = 0.1f, float weaponSway = 0.1f, float rotation = 0.3f)
        {
            float duration = FloatUtilities.GetRandomFloat(1.5f, 5f);

            vp_FPSCamera cam = GameManager.GetVpFPSCamera();
            cam.DoEarthQuake(x, y, duration, weaponSway, rotation);

            //audio
            EarthquakeAudioHelper.PlayAudio(false, duration, player, clips);
        }

        public void ScheduleEarthquake()
        {
            nextEarthquakeTime = FloatUtilities.GetRandomFloat(minTimeToEarthquake, maxTimeToEarthquake);
            float currentTimeInDays = GameManager.GetTimeOfDayComponent().GetHoursPlayedNotPaused() / 24f;
            nextEarthquakeTime = currentTimeInDays + nextEarthquakeTime;

            SaveDataProxy.nextEarthquakeTime = nextEarthquakeTime;
            SaveDataProxy.lastEarthquakeTime = lastEarthquakeTime;

            Main.SaveDataManager.Save(SaveDataProxy);
        }

    }
}
