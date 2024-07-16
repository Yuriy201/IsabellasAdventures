using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeoxiderUi
{
    public class UIReady : MonoBehaviourPunCallbacks
    {
        public void Quit()
        {
            Application.Quit();
        }

        public void Restart()
        {
            int idScene = SceneManager.GetActiveScene().buildIndex;
            LoadScene(idScene);
        }
        public void Pause(bool activ)
        {
            if (activ)
                Time.timeScale = 0;
            else
                Time.timeScale = 1.0f;
        }

        public void LoadScene(int idScene) //добавить возможность асинхронной загрузки
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(idScene);
        }
        private void OnValidate()
        {
            name = nameof(UIReady);
#if UNITY_2023_1_OR_NEWER
#else
#endif
        }
    }
}
