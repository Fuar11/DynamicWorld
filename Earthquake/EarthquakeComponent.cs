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

namespace DynamicWorld.Earthquake
{
    [RegisterTypeInIl2Cpp]
    internal class EarthquakeComponent : MonoBehaviour
    {

        //in days
        float minTimeToEarthquake = 10f;
        float maxTimeToEarthquake = 60f;

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

        private void LoadOrInitData()
        {



        }

        public void Update()
        {

            if (nextEarthquakeTime == null) return;

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

        public void DoEarthquake(float x = 0.9f, float y = 0.1f, float weaponSway = 0.1f, float rotation = 0.4f)
        {
            Main.Logger.Log("Doing major earthquake!", ComplexLogger.FlaggedLoggingLevel.Trace);

            float duration = FloatUtilities.GetRandomFloat(minEarthquakeDuration, maxEarthquakeDuration);

            vp_FPSCamera cam = GameManager.GetVpFPSCamera();
            cam.DoEarthQuake(x, y, duration, weaponSway, rotation);

            //audio
            EarthquakeAudioHelper.PlayAudio(true, duration, player, clips);

            ScheduleEarthquake();
        }

        //minor tremor
        public void DoTremor(float x = 0.5f, float y = 0.1f, float weaponSway = 0.1f, float rotation = 0.3f)
        {
            Main.Logger.Log("Doing minor tremor.", ComplexLogger.FlaggedLoggingLevel.Trace);

            float duration = FloatUtilities.GetRandomFloat(1f, 4f);

            vp_FPSCamera cam = GameManager.GetVpFPSCamera();
            cam.DoEarthQuake(x, y, duration, weaponSway, rotation);

            //audio
            EarthquakeAudioHelper.PlayAudio(false, duration, player, clips);
        }

        private void ScheduleEarthquake()
        {
            nextEarthquakeTime = FloatUtilities.GetRandomFloat(minTimeToEarthquake, maxTimeToEarthquake);
            float currentTimeInDays = GameManager.GetTimeOfDayComponent().GetHoursPlayedNotPaused() / 24f;
            Main.Logger.Log($"Current time in days: {currentTimeInDays}", ComplexLogger.FlaggedLoggingLevel.Debug);
            nextEarthquakeTime = currentTimeInDays + nextEarthquakeTime;
            Main.Logger.Log($"Next earthquake time: {nextEarthquakeTime}", ComplexLogger.FlaggedLoggingLevel.Debug);
        }

    }
}
