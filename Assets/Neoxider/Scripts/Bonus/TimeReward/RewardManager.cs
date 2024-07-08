using System;
using UnityEngine;
using UnityEngine.Events;

namespace NeoxiderUi
{
    public class RewardManager : MonoBehaviour
    {
        [SerializeField] private int _secondsToWaitForReward = 60 * 60; //1 hours

        [SerializeField] private string _lastRewardTimeStr;
        [SerializeField] private float _updateTime = 1;
        [SerializeField] private string _addKey = "Bonus 24";
        [SerializeField] private const string _lastRewardTimeKey = "LastRewardTime";

        public UnityEvent<float> OnChangeTime = new UnityEvent<float>();


        private void Start()
        {
            InvokeRepeating(nameof(GetTime), 0, _updateTime);
        }

        private void GetTime()
        {
            OnChangeTime?.Invoke(GetSecondsUntilReward());
        }

        public static string FormatTime(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return time.ToString(@"hh\:mm\:ss");
        }

        public int GetSecondsUntilReward()
        {
            _lastRewardTimeStr = PlayerPrefs.GetString(_lastRewardTimeKey + _addKey, string.Empty);

            if (!string.IsNullOrEmpty(_lastRewardTimeStr))
            {
                DateTime lastRewardTime;

                if (DateTime.TryParse(_lastRewardTimeStr, out lastRewardTime))
                {
                    DateTime currentTime = DateTime.UtcNow;
                    TimeSpan timeSinceLastReward = currentTime - lastRewardTime;
                    int secondsPassed = (int)timeSinceLastReward.TotalSeconds;
                    int secondsUntilReward = _secondsToWaitForReward - secondsPassed;

                    return secondsUntilReward > 0 ? secondsUntilReward : 0;
                }
            }

            return 0;
        }

        public bool GiveReward()
        {
            if (CanTakeReward())
            {
                SaveCurrentTimeAsLastRewardTime();
                OnChangeTime?.Invoke(GetSecondsUntilReward());
                return true;
            }

            return false;
        }

        public bool CanTakeReward()
        {
            return GetSecondsUntilReward() == 0;
        }

        private void SaveCurrentTimeAsLastRewardTime()
        {
            Debug.Log(nameof(SaveCurrentTimeAsLastRewardTime) + " " + _addKey);
            PlayerPrefs.SetString(_lastRewardTimeKey + _addKey, DateTime.UtcNow.ToString());
        }
    }
}
