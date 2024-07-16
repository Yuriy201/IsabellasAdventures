using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeoxiderUi
{
    public class UIReady : GetPhotonView
    {
        public void Quit()
        {
            Application.Quit();
        }

        public void Restart()
        {
            _view.RPC(nameof(RestartRPC), RpcTarget.AllBuffered);
        }
        [PunRPC]
        private void RestartRPC()
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            PhotonNetwork.LoadLevel(sceneIndex);
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
