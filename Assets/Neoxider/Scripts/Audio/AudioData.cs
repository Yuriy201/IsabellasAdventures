using System;
using System.Collections.Generic;
using UnityEngine;

namespace NeoxiderAudio
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Neoxider/Audio", order = 32)]
    public class AudioData : ScriptableObject
    {
        [System.Serializable]
        public class AudioInfo
        {
            public AudioClip[] clips;
            public ClipType type = ClipType.click;
            [Range(0, 1f)]
            public float volume = 1;
            public SourseType sourseType = SourseType.Interface;

            public AudioInfo(ClipType type)
            {
                this.type = type;
                sourseType = SourseType.Game;
            }
        }

        public AudioInfo[] audioInfo => _audioInfo;

        [SerializeField] private AudioInfo[] _audioInfo;
        [SerializeField] private Dictionary<ClipType, AudioInfo> _audioInfoDict;

        [Header("Editor")]
        public bool _autoSetAllType;

        internal void SetAudioData(AudioInfo[] audioInfo)
        {
            _audioInfo = audioInfo;
        }

        public AudioInfo GetAudioInfo(ClipType clipType)
        {
            if (_audioInfoDict.TryGetValue(clipType, out AudioInfo audioInfo))
            {
                return audioInfo;
            }

            return null;
        }

        public int GetAudioInfoId(ClipType clipType)
        {
            for (int i = 0; i < _audioInfo.Length; i++)
            {
                if (_audioInfo[i].type == clipType)
                    return i;
            }

            return -1;
        }

        private void OnValidate()
        {
            if (_autoSetAllType)
            {
                _autoSetAllType = false;
                int count = Enum.GetNames(typeof(ClipType)).Length;
                AudioInfo[] audioDatasLast = (AudioInfo[])_audioInfo.Clone();
                _audioInfo = new AudioInfo[count];

                for (int i = 0; i < count; i++)
                {
                    _audioInfo[i] = new AudioInfo((ClipType)Enum.Parse(typeof(ClipType), i.ToString()));
                }

                for (int i = 0; i < audioDatasLast.Length; i++)
                {
                    ClipType type = audioDatasLast[i].type;

                    int id = GetAudioInfoId(type);

                    if (id >= 0)
                        _audioInfo[id] = audioDatasLast[i];
                }
            }

            _audioInfoDict = new Dictionary<ClipType, AudioInfo>();

            for (int i = 0; i < _audioInfo.Length; i++)
            {
                _audioInfoDict.Add(_audioInfo[i].type, _audioInfo[i]);
            }

            //Debug.Log("dict - " + _audioInfo[0].type + _audioInfo[0].clips.Length);
        }
    }
}
