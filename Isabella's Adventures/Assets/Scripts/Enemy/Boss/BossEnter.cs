using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnter : MonoBehaviour
{
    public GameObject[] spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            for (int i = 0; i < spawn.Length; i++)
            {
                spawn[i].SetActive(true);
            }
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().far = 15;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            for (int i = 0; i < spawn.Length; i++)
            {
                spawn[i].SetActive(true);
            }
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().far = 15;
            gameObject.SetActive(false);
        }
    }
}
