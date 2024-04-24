using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerSelect : MonoBehaviour
{
    [SerializeField] private CharacterSpawner charSpawn;
    [SerializeField] private Sprite[] characterSprites;

    
    public void Assasin()
    {
        GameObject player = Resources.Load<GameObject>("Player");

        player.GetComponent<SpriteRenderer>().sprite = characterSprites[0];

        charSpawn._selectedCharacter = player;

        SceneManager.LoadScene(1);
    }

    public void Wizard()
    {
        GameObject player = Resources.Load<GameObject>("Player");

        player.GetComponent<SpriteRenderer>().sprite = characterSprites[1];

        charSpawn._selectedCharacter = player;

        SceneManager.LoadScene(1);
    }

    public void Witch()
    {
        GameObject player = Resources.Load<GameObject>("Player");

        player.GetComponent<SpriteRenderer>().sprite = characterSprites[2];

        charSpawn._selectedCharacter = player;

        SceneManager.LoadScene(1);
    }

    public void KnightMale()
    {
        GameObject player = Resources.Load<GameObject>("Player");

        player.GetComponent<SpriteRenderer>().sprite = characterSprites[3];

        charSpawn._selectedCharacter = player;

        SceneManager.LoadScene(1);
    }

    public void KnightFemale()
    {
        GameObject player = Resources.Load<GameObject>("Player");

        player.GetComponent<SpriteRenderer>().sprite = characterSprites[4];

        charSpawn._selectedCharacter = player;

        SceneManager.LoadScene(1);
    }
}
