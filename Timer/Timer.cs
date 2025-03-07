/*
 * About:
 * Simple Timer class
 *
 * Features:
 * 3 Timer setup types : Duration, Repeater, Interval
 *
 * How To Use:
 * 1. Create a timer using Timer.CreateNew()
 *      e.g. var timer = Timer.CreateNew(gameObject);
 *
 * 2. Setup Timer using Setup(), SetupRepeater() or SetupInterval()
 *      e.g. timer.Setup(1);
 *
 * 3. Use timer.TimerHit property to check and do something about it
 *      e.g. if(timer.TimerHit) Shoot();
 *
 * How It Works:
 * Uses Unity Engine Update Loop
 *
 * Notes:
 * No Start Delay
 */

using UnityEngine;

namespace asim.unity.utils
{
    public class Timer : MonoBehaviour
    {
        private bool autoContinueRun;
        private bool currentRunEnded;

        private int maxRuns;
        private float timerEnd;

        private float timerStart;

        /* Public Accessors */
        public bool IsRuning { get; private set; }

        public bool TimerHit
        {
            get
            {
                var hit = IsRuning && currentRunEnded;

                if (!autoContinueRun && currentRunEnded)
                {
                    Continue();
                    currentRunEnded = false;
                }

                return hit;
            }
        }

        public float CurrentTimer { get; private set; }

        public float TotalTime { get; private set; }

        public int TotalRuns { get; private set; }

        /* Private */
        private void Reset()
        {
            CurrentTimer = timerStart;
            TotalTime = 0;
            TotalRuns = 0;
            currentRunEnded = false;
        }

        private void Update()
        {
            if (!IsRuning) return;

            if (currentRunEnded)
            {
                if (TotalRuns >= maxRuns)
                {
                    IsRuning = false;
                    return;
                }

                if (autoContinueRun)
                {
                    currentRunEnded = false;
                    Continue();
                }
                else
                {
                    return;
                }
            }

            CurrentTimer += Time.deltaTime;
            TotalTime += Time.deltaTime;
            if (CurrentTimer >= timerEnd)
            {
                TotalRuns++;
                currentRunEnded = true;
            }
        }

        private void Continue()
        {
            CurrentTimer = timerStart;
        }

        /* Public */
        public static Timer CreateNew(GameObject gameObject, string name = null)
        {
            if (string.IsNullOrEmpty(name)) name = $"{gameObject.name} timer";

            var newGO = new GameObject(name, typeof(Timer));
            newGO.transform.parent = gameObject.transform;
            var timer = newGO.GetComponent<Timer>();

            return timer;
        }

        /// <summary>
        ///     Simple timer that counts down from a set duration.
        /// </summary>
        public void Setup(float duration)
        {
            timerStart = 0;
            timerEnd = duration;
            maxRuns = 1;
            autoContinueRun = false;
        }

        /// <summary>
        ///     Timer that counts down and automatically repeats
        /// </summary>
        public void SetupRepeater(float duration, bool autoContinueRun = true, int maxRuns = int.MaxValue)
        {
            timerStart = 0;
            timerEnd = duration;
            this.maxRuns = maxRuns;
            this.autoContinueRun = autoContinueRun;
        }

        /// <summary>
        ///     Setup a custom timer
        /// </summary>
        public void SetupInterval(float start, float end, bool autoContinueRun = true, int maxRuns = int.MaxValue)
        {
            timerStart = start;
            timerEnd = end;
            this.maxRuns = maxRuns;
            this.autoContinueRun = autoContinueRun;
        }

        public void StartTimer()
        {
            IsRuning = true;
            Reset();
        }

        public void StopTimer()
        {
            IsRuning = false;
        }

        public void PauseUnpauseTimer()
        {
            IsRuning = !IsRuning;
        }
    }
}