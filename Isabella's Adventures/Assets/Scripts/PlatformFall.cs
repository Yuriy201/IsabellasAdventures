using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{

    bool stand;
    // Start is called before the first frame update
    void Start()
    {
        stand = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.5f);
        if (stand == true)
        {
            gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().freezeRotation = true;
        }
        yield break;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stand = true;
            StartCoroutine(Fall());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stand = false;
        }
    }
}
