using UnityEngine;
using UnityEngine.Events;

namespace NeoxiderUi
{
    public class LevelMoney : MonoBehaviour
    {
        public int money => _money;
        public int levelMoney => _levelMoney;

        public UnityEvent<int> OnChangedLevelMoney;
        public UnityEvent<int> OnChangedMoney;

        [SerializeField] private int _money;
        [SerializeField] private int _levelMoney;
        [SerializeField] private const string _moneySave = "MoneyStandart";

        void Start()
        {
            Load();
            OnChangedMoney?.Invoke(_money);
            ResetLevelMoney();
        }

        private void Load()
        {
            _money = PlayerPrefs.GetInt(_moneySave, _money);
        }

        private void Save()
        {
            PlayerPrefs.SetInt(_moneySave, _money);
        }

        internal void AddLevelMoney(int count)
        {
            _levelMoney += count;
            OnChangedLevelMoney?.Invoke(_levelMoney);
        }

        internal void AddMoney(int count)
        {
            _money += count;
            Save();
            OnChangedMoney?.Invoke(_money);
        }

        internal void ResetLevelMoney()
        {
            _levelMoney = 0;
            OnChangedLevelMoney?.Invoke(_levelMoney);
        }

        internal void SetMoney()
        {
            _money += _levelMoney;
            OnChangedMoney?.Invoke(_money);
            Save();
        }

        internal bool SpendMoney(int count)
        {
            if (CheckSpend(count))
            {
                _money -= count;
                OnChangedMoney?.Invoke(_money);
                Save();
                return true;
            }

            return false;
        }

        internal bool CheckSpend(int count)
        {
            return _money >= count;
        }
    }

}