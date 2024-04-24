using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Menu _menu;

    [SerializeField] private GameObject _assasin;
    [SerializeField] private GameObject _witch;
    [SerializeField] private GameObject _warrior;
    [SerializeField] private GameObject _womanWarrior;
    [SerializeField] private GameObject _wizard;

    [SerializeField] private Sprite _background1;
    [SerializeField] private Sprite _background2;
    [SerializeField] private Sprite _background3;
    [SerializeField] private Sprite _background4;
    [SerializeField] private Sprite _background5;
    [SerializeField] private Sprite _background6;

    public GameObject _selectedCharacter;

    private int startLevel = 1;
    

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            return;
        }

        if (GameObject.FindWithTag("CharacterSpawnPoint"))
        {
            var character = Instantiate(_selectedCharacter);
            character.transform.position = GameObject.FindWithTag("CharacterSpawnPoint").transform.position;

            /*switch (level)
            {
                case 1:
                    GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = _background1;
                    break;
                case 2:
                    GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = _background2;
                    break;
                case 3:
                    GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = _background3;
                    break;
                case 4:
                    GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = _background4;
                    break;
                case 5:
                    GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = _background3;
                    break;
                case 6:
                    GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = _background6;
                    break;
            }*/
            
        }
        else
        {
            Debug.LogWarning("There is no Object with CharacterSpawnPoint tag!");
        }
    }

    public void SetCharacterAssasin()
    {
        _selectedCharacter = _assasin;
        _menu.LoadTo(startLevel);
    }

    public void SetCharacterWitch()
    {
        _selectedCharacter = _witch;
        _menu.LoadTo(startLevel);
    }

    public void SetCharacterWarrior()
    {
        _selectedCharacter = _warrior;
        _menu.LoadTo(startLevel);
    }

    public void SetCharacterWomanWarrior()
    {
        _selectedCharacter = _womanWarrior;
        _menu.LoadTo(startLevel);
    }

    public void SetCharacterWizard()
    {
        _selectedCharacter = _wizard;
        _menu.LoadTo(startLevel);
    }
}
