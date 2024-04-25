using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public string sceneName;

    public void OpenScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
