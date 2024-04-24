using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Testing : MonoBehaviour
{
    [SerializeField] private LevelWindow levelWindow;
    [SerializeField] private Player player;
    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();
        levelWindow.SetLevelSystem(levelSystem);
    }
    
}
