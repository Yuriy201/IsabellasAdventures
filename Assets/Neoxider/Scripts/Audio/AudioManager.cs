using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static NeoxiderAudio.AudioData;

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

        [Range(0, 1f)]
        public float startVolumeInterface = 0.8f;
        [Range(0, 1f)]
        public float startVolumeMusic = 0.5f;
        [Range(0, 1f)]
        public float startVolumeGame = 1f;

        public AudioData audioData;

        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            Init();
        }

        private void Init()
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

        private void Start()
        {
            for (int i = 0; i < ast.Length; i++)
            {
                AudioSourseType a = ast[i];

                if (a.sourseType == SourseType.Interface && a.sourse != null)
                {
                    a.sourse.volume = startVolumeInterface;
                }
                else if (a.sourseType == SourseType.Music && a.sourse != null)
                {
                    a.sourse.volume = startVolumeInterface;
                }
                else if (a.sourseType == SourseType.Game && a.sourse != null)
                {
                    a.sourse.volume = startVolumeGame;
                }
            }
        }

        public static void PlaySound(int clipId = 0)
        {
            if (clipId >= 0)
                PlaySound((ClipType)clipId);
        }

        public static void PlaySound(ClipType clipType, float volume = 1f, Transform transform = null)
        {
            if (Instance != null)
            {
                AudioInfo aData = Instance.audioData.GetAudioInfo(clipType);

                if (aData != null)
                {
                    if (TryGetRandomClip(aData.clips, out AudioClip clip))
                    {
                        AudioSource source = Instance.GetSourse(aData.sourseType);

                        if (transform != null)
                        {
                            source.spatialBlend = 1;
                            source.transform.position = transform.position;
                        }

                        float avarageVolume = (volume + aData.volume) / 2;
                        source.PlayOneShot(clip, avarageVolume);
                    }
                }
            }
            else
            {
                
         

                CreateInstance();

                Instance.StartCoroutine(LoadAudioData(() =>
                {
                    PlaySound(clipType, volume, transform);
                }));
         
            }
        }

        private static IEnumerator LoadAudioData(System.Action callback)
        {           
            var audioDataHandler = Addressables.LoadAssetAsync<AudioData>("AudioData");
            yield return audioDataHandler;

            Instance.audioData = audioDataHandler.Result;

            if (Instance.audioData == null)
            {
                Debug.LogWarning("AudioManager has not audioData");
            }

            callback.Invoke();
        }

        private static void CreateInstance()
        {
            GameObject newGameObject = new GameObject();
            newGameObject.name = nameof(AudioManager);

            AudioManager audioManager = newGameObject.AddComponent<AudioManager>();
            Instance = audioManager;
            DontDestroyOnLoad(audioManager.gameObject);

            for (int i = 0; i < Instance.ast.Length; i++)
            {
                GameObject newAudioSource = new GameObject();
                newAudioSource.transform.SetParent(Instance.transform, false);
                newAudioSource.name = Instance.ast[i].sourseType.ToString() + " Audio Source";
                Instance.ast[i].sourse = newAudioSource.AddComponent<AudioSource>();
            }
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

        private static bool TryGetRandomClip(AudioClip[] clips, out AudioClip clip)
        {
            if (clips.Length == 1)
            {
                clip = clips[0];
                return true;
            }
            else if (clips.Length > 1)
            {
                clip = clips[Random.Range(0, clips.Length)];
                return true;
            }

            clip = null;
            return false;
        }

        private void OnValidate()
        {

        }
    }
}
