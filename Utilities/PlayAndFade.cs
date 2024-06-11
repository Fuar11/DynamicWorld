using MelonLoader;
using Il2CppInterop.Runtime.Injection;
using Il2Cpp;
using UnityEngine;
using UnityEngine.Rendering;
using AudioMgr;
using Il2CppInterop.Runtime.Attributes;
using System.Collections;


namespace DynamicWorld.Utilities
{
    public class PlayAndFade
    {
        Shot _assignedShot;
        Clip _assignedClip;
        double _fadeDuration;
        double _duration;
        double _startTime;
        double _fadeStartTime;
        double _timeElapsed;
        float _startVolume;

        object _timerLoop;
        bool _isPlaying;

        public PlayAndFade(Shot targetShot, Clip audioClip, double duration, double fadeDuration)
        {
            _assignedShot = targetShot;
            _assignedClip = audioClip;
            _fadeDuration = fadeDuration;

            _startTime = AudioSettings.dspTime;

            Main.Logger.Log($"Clip duration: {_assignedClip.clipLength}", ComplexLogger.FlaggedLoggingLevel.Debug);
            Main.Logger.Log($"Duration: {duration}", ComplexLogger.FlaggedLoggingLevel.Debug);
            Main.Logger.Log($"Fade duration: {fadeDuration}", ComplexLogger.FlaggedLoggingLevel.Debug);

            _assignedShot.AssignClip(_assignedClip);
            _assignedShot._audioSource.PlayScheduled(_startTime);
            _duration = duration;
            _startVolume = _assignedShot._audioSource.volume;
            _fadeStartTime = _duration - _fadeDuration;

            _isPlaying = true;

            _timerLoop = MelonCoroutines.Start(TimerLoop());
        }



        [HideFromIl2Cpp]

        private IEnumerator TimerLoop()
        {
            while (_isPlaying)
            {
                _timeElapsed = AudioSettings.dspTime - _startTime;

                if (_timeElapsed < _fadeStartTime)
                {
                    yield return null;
                }
                else
                {
                  
                    double fadeProgress = (AudioSettings.dspTime - (_startTime + _fadeStartTime)) / _fadeDuration;

                    _assignedShot._audioSource.volume = Mathf.Lerp((float)_startVolume, 0f, (float)fadeProgress);

                    if (_assignedShot._audioSource.volume <= 0)
                    {
                        _assignedShot.Stop();
                        _isPlaying = false;
                    }

                    yield return null;
                }
            }
        }
    }
}