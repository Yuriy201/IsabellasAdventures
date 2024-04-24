using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathCanvas : MonoBehaviour
{
    private const int GameMenuIndex = 0;
    private Scene scene;

    private void OnEnable()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void ReStart()
    {
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void Exit()
    {
        SceneManager.LoadScene(GameMenuIndex);
    }
}
