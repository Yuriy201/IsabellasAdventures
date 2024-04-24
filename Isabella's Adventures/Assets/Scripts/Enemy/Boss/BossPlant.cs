using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlant : MonoBehaviour
{
    public float hp, range;
    bool canHurt;
    public GameObject bullet;
    public GameObject[] wall;
    Transform shotPlace;
    // Start is called before the first frame update
    void Start()
    {
        shotPlace = transform.GetChild(0);
        canHurt = true;
        hp = 5;
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(1);
        //float r = Random.Range(5, 10);
        bullet.GetComponent<AimingBullet>().speed = range;
        GetComponent<Animator>().SetTrigger("Shoot");
        StartCoroutine(Shot());
    }

    private void Update()
    {
        range = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        
        if (canHurt == false)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void Shoot()
    {
        Instantiate(bullet, shotPlace.position, transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Attack" && canHurt == true)
        {
            //hp -= other.GetComponent<Damage>().damage;
            hp -= 1;
            StartCoroutine(Hurt());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack" && canHurt == true)
        {
            //hp -= other.GetComponent<Damage>().damage;
            hp -= 1;
            StartCoroutine(Hurt());
        }
    }

    IEnumerator Hurt()
    {
        if (hp > 0)
        {
            canHurt = false;
            yield return new WaitForSeconds(0.5f);
            canHurt = true;
        }
        else
        {
            Destroy(wall[0]);
            Destroy(wall[1]);
            Destroy(gameObject);
        }
    }
}
