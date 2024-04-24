using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Health : MonoBehaviour
{
    public GameObject GameOverScreen;
    [SerializeField] private int health;
    [SerializeField] private int delay;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip AidKitSound;
    [SerializeField] private AudioClip DeathSound;
    public Action<int, GameObject> OnTakeHit;
    private LevelSystem levelSystem;

    public int CurrentHealth
    {
        get { return health; }
    }
    public int health1
    {
        get { return health; }
        set
        {
            if (value > 200)
                health = value;
        }
    }

    private void Start()
    {
        GameManager.Instance.healthContainer.Add(gameObject, this);
        GameManager.Instance.collisionContainer.Add(gameObject, this);
    }
    public void TakeHit(int damage, GameObject attacker)
    {
        health -= damage;
        if (OnTakeHit != null)
            OnTakeHit(damage, attacker);
        if (health <= 0)
        {
            if (!GameOverScreen.activeSelf)
            {
                GameOverScreen.SetActive(true);
                GetComponent<Player>().speed = 0;
            }
            StartCoroutine(PlayerDie());
            health = 0;
            Time.timeScale = 1;
        }
    }
    public void SetHealth(int bonusHealth)
    {

        health += bonusHealth;

    }
    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        SetHealthBarSize(1f + levelSystem.GetLevelNumber() * .1f);
    }
    private void SetHealthBarSize(float healthBarSize)
    {
        transform.Find("Health").localScale = new Vector3(.7f * healthBarSize, 1, 1);
    }
    IEnumerator PlayerDie()
    {
        soundSource.PlayOneShot(DeathSound);
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        yield break;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Tablet"))
        {
            soundSource.PlayOneShot(AidKitSound);
            Destroy(col.gameObject);
            SetHealth(10);
        }
    }
}
