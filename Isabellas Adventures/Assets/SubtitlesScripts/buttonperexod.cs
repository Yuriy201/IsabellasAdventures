using UnityEngine;
using UnityEngine.SceneManagement;

public class Izmenit_Sceny : MonoBehaviour
{
    public string GameMenu;

    public void Na_Nazhatie_Knopki()
    {
        SceneManager.LoadScene(GameMenu);
    }
}
