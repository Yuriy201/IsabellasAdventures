using System;
using UnityEngine;

namespace NeoxiderAudio
{
    public class AudioData : MonoBehaviour
    {
        [System.Serializable]
        public class AudioInfo
        {
            public SourseType sourseType = SourseType.Interface;
            public ClipType type = ClipType.click;
            public AudioClip[] clips;

            public AudioInfo(ClipType type)
            {
                this.type = type;
                sourseType = SourseType.Game;
            }
        }

        public static AudioData Instance;

        [SerializeField] private AudioInfo[] _audioDatas;
        [SerializeField] private bool _autoSetAllType;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public AudioInfo GetData(ClipType clipType)
        {
            foreach (var d in _audioDatas)
            {
                if (d.type == clipType)
                    return d;
            }

            return null;
        }

        private void OnValidate()
        {
            if (_autoSetAllType)
            {
                _autoSetAllType = false;
                int count = Enum.GetNames(typeof(ClipType)).Length;
                _audioDatas = new AudioInfo[count];

                for (int i = 0; i < count; i++)
                {
                    _audioDatas[i] = new AudioInfo((ClipType)Enum.Parse(typeof(ClipType), i.ToString()));
                }
            }
        }
    }
}