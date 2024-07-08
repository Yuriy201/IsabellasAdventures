using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace NeoxiderUi
{
    public class Roulette : MonoBehaviour
    {
        [SerializeField] private Transform _roll;
        [SerializeField] private int _countBonus;
        [SerializeField] private float _speedRotate = 400;
        [SerializeField] private float _timeRotate = 5;
        [SerializeField] private float _startBonusRotate = -10f;

        public bool isSpinning { get => _isSpinning; }
        public bool countSpin { get => countSpin; }

        private bool _isSpinning;
        private int _countSpin = 1;

        public UnityEvent<int> OnChangeCount = new UnityEvent<int>();
        public UnityEvent<int> OnWinIdBonus = new UnityEvent<int>();

        private void Start()
        {
            _countSpin = PlayerPrefs.GetInt(nameof(_countSpin), _countSpin);
        }

        public void AddCountSpin(int count = 1)
        {
            _countSpin += count;
            OnChangeCount?.Invoke(_countSpin);

            PlayerPrefs.SetInt(nameof(_countSpin), _countSpin);
        }

        public void Spin(int count, float time = 0)
        {
            _countBonus = count;

            if (time != 0)
            {
                _timeRotate = time;
            }

            if (_countSpin > 0 && !_isSpinning)
            {
                _countSpin--;
                OnChangeCount?.Invoke(_countSpin);
                StartCoroutine(RollCoroutine());
                PlayerPrefs.SetInt(nameof(_countSpin), _countSpin);
            }
        }

        private IEnumerator RollCoroutine()
        {
            _isSpinning = true;
            float timeSpent = 0;
            float initialSpeed = _speedRotate + Random.Range(-100, 100);
            float totalRotationTime = _timeRotate + Random.Range(-1f, 1f);

            while (timeSpent < totalRotationTime)
            {
                float currentSpeed = Mathf.Lerp(initialSpeed, 0, timeSpent / totalRotationTime);
                _roll.Rotate(0, 0, currentSpeed * Time.deltaTime);
                timeSpent += Time.deltaTime;
                yield return null;
            }

            DetermineBonus();
            _isSpinning = false;
        }

        private void DetermineBonus()
        {
            float angle = _roll.eulerAngles.z - _startBonusRotate;
            angle = (360 + angle) % 360;
            float range = 360 / _countBonus;
            int bonusIndex = Mathf.FloorToInt(angle / range);
            bonusIndex = Mathf.Clamp(bonusIndex, 0, _countBonus - 1);

            Win(bonusIndex);
        }

        private void Win(int id)
        {
            OnWinIdBonus?.Invoke(id);
        }
    }
}
