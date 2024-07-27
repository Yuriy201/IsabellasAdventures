using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace NeoxiderUi
{
    public class UIReady : GetPhotonView
    {
        [Inject] private GameConfig _gameConfig;

        private int _currentSceneIndex;

        public void Quit()
        {
            Application.Quit();
        }

        public void Restart()
        {
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (_gameConfig.IsMultiplayer)
            {
                _view.RPC(nameof(RestartRPC), RpcTarget.AllBuffered);
            }
            else
            {
                SceneManager.LoadScene(_currentSceneIndex);
            }
        }
        [PunRPC]
        private void RestartRPC()
        {
            PhotonNetwork.LoadLevel(_currentSceneIndex);
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
            if (_gameConfig.IsMultiplayer)
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
