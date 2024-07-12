using UnityEngine;

namespace NeoxiderAudio
{
    public enum SourseType
    {
        Interface,
        Music,
        Game
    }

    public class AudioManager : MonoBehaviour
    {

        [System.Serializable]
        public class AudioSourseType
        {
            public SourseType sourseType;
            public AudioSource sourse;

            public AudioSourseType(SourseType s)
            {
                sourseType = s;
            }
        }

        public AudioSourseType[] ast = {
        new AudioSourseType(SourseType.Interface),
        new AudioSourseType(SourseType.Music),
        new AudioSourseType(SourseType.Game) };

        public AudioData audioDatas;

        //public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            //if (Instance == null)
            //{
            //    Instance = this;
            //    DontDestroyOnLoad(gameObject);
            //}
            //else
            //{
            //    Destroy(gameObject);
            //}
        }

        public void PlayClick()
        {
            PlaySound(ClipType.click);
        }

        public void PlaySound(ClipType clipType, float volume = 1f, Transform transform = null)
        {
            AudioData.AudioInfo aData = audioDatas.GetData(clipType);
            AudioClip clip = GetRandomElement(aData.clips);
            AudioSource source = GetSourse(aData.sourseType);

            if (transform != null)
            {
                source.spatialBlend = 1;
                source.transform.position = transform.position;
            }

            source.PlayOneShot(clip, volume);
        }

        public void MuteAll(bool activ)
        {
            foreach (var a in ast)
            {
                if (a.sourse != null)
                {
                    a.sourse.mute = !activ;
                }
            }
        }

        public void Mute(SourseType sourseType, bool activ)
        {
            AudioSource aud = GetSourse(sourseType);

            if (aud != null)
            {
                aud.mute = !activ;
            }
        }

        public void MuteInterface(bool activ)
        {
            Mute(SourseType.Interface, activ);
        }

        public void MuteGame(bool activ)
        {
            Mute(SourseType.Game, activ);
        }

        public void MuteEfx(bool activ)
        {
            Mute(SourseType.Interface, activ);
            Mute(SourseType.Game, activ);
        }

        public void MuteMusic(bool activ)
        {
            Mute(SourseType.Music, activ);
        }

        public bool SwitchMuteEfx()
        {
            bool activ = GetSourse(SourseType.Interface).mute;
            MuteEfx(activ);
            return activ;
        }

        public bool SwitchMuteMusic()
        {
            bool activ = GetSourse(SourseType.Music).mute;
            MuteMusic(activ);
            return activ;
        }

        public bool SwitchAllSounds()
        {
            bool activ = GetSourse(SourseType.Music).mute;
            MuteAll(activ);
            return activ;
        }

        private AudioSource GetSourse(SourseType sourseType)
        {
            foreach (var a in ast)
            {
                if (a.sourseType == sourseType)
                    return a.sourse;
            }

            return null;
        }

        private AudioClip GetRandomElement(AudioClip[] clips)
        {
            if (clips.Length == 1)
                return clips[0];

            return clips[Random.Range(0, clips.Length)];
        }

        private void OnValidate()
        {
            audioDatas = FindFirstObjectByType<AudioData>();
        }
    }
}
