using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using PlayerScripts;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] public int _health;
    [SerializeField] private HealthPlayer healthBar;

    public Joystick joystick;
    private float moveInput;
    public Button shift;

    [SerializeField] private float damageForce;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip ArrowSound;
    [SerializeField] private AudioClip PlayerDamageWoman;
    [SerializeField] private AudioClip DeathSound;

    

    private int _currentHealth;
    private Rigidbody2D rigidboby;
    private Animator anim;
    public GameObject arrow;
    public Vector2 move;
    public Transform groundCheck, shotPlace;
    public float speed, jump, hp, go;
    public bool isRight, isGrounded, coolDown, canHurt, canShoot, jumpBut;
    private LevelSystem levelSystem;
    private UICharacterController controller;

    public static Player Instance { get; set; }
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int, int> HealthTextChanged;

    void Start()
    {
        rigidboby = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = transform.GetChild(0);
        shotPlace = transform.GetChild(1);
        isRight = true;
        coolDown = false;
        canHurt = true;
        canShoot = true;
        hp = 100;
        go = 0;
        _currentHealth = _health;
        HealthTextChanged?.Invoke(_currentHealth, _health);
    }

    private void Awake()
    {
        Instance = this;
    }

    public void InitUIController(UICharacterController uiController)
    {
        controller = uiController;
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        SetHealthBarSize(1f + levelSystem.GetLevelNumber() * .1f);
    }

    private void SetHealthBarSize(float healthBarSize)
    {
        transform.Find("Health").localScale = new Vector3(.7f * healthBarSize, 1, 1);
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -15)
        {
            Die();
        }

        if (canHurt == false)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        //Go();
        Move();
        Jump();
        anim.SetFloat("Speed", Mathf.Abs(move.x));

        GroundCheck();
    }

    private void Move()
    {

        move = Vector3.zero;
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A))
        {
            move = Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            move = Vector3.right;
        }
#endif

        if (controller.Left.IsPressed)
        {
            move = Vector3.left;
        }

        if (controller.Right.IsPressed)
        {
            move = Vector3.right;
        }

        moveInput = joystick.Horizontal;
        if(moveInput > 0)
        {
            move = Vector3.right;
        }
        if (moveInput < 0)
        {
            move = Vector3.left;
        }

        

        move *= speed;
        move.y = rigidboby.velocity.y;
        rigidboby.velocity = move;

        if (move.x > 0)
            transform.localScale = new Vector2(1, 1);
        if (move.x < 0)
            transform.localScale = new Vector2(-1, 1);
    }

    private void Go()
    {
        //life.fillAmount = hp / 100;
        if (go == 0)
        {
            anim.SetBool("Go", false);
        }
        else
        {
            anim.SetBool("Go", true);
        }

#if UNITY_EDITOR
        go = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Horizontal") > 0)
            transform.localScale = new Vector2(1, 1);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localScale = new Vector2(-1, 1);

#endif

        if (canShoot)
        {
            if (isGrounded == true)
            {
                rigidboby.velocity = new Vector2(speed * go, rigidboby.velocity.y);
            }
            else
            {
                rigidboby.velocity = new Vector2(speed * 1.2f * go, rigidboby.velocity.y);
            }
        }
        else
        {
            rigidboby.velocity = new Vector2(0, 0);
        }
    }

    public bool j;
    public int jit = 0;

    public void JButDown()
    {
        jumpBut = true;
    }

    public void JButUp()
    {
        jumpBut = false;
    }

    void Jump()
    {
        anim.SetBool("isGrounded", isGrounded);

        if (jumpBut == true)
        {
            if (isGrounded)
            {
                anim.SetTrigger("StartJump");
                j = true;
            }
        }
        else
        {
            j = false;
        }

        if (j == true)
        {
            //anim.SetBool("Jump", true);
            if (jit++ < 60 && rigidboby.velocity.y < 50)
            {
                //rigB.AddForce(Vector2.up * jump / jit);
                rigidboby.AddForce(transform.up * jump / jit, ForceMode2D.Impulse);
            }
        }
        else
        {
            jit = 0;
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, LayerMask.GetMask("Ground"));
        //isWalled = Physics2D.OverlapCapsule(wallCheck.position, new Vector2(1f, 2f), CapsuleDirection2D.Vertical, 0, LayerMask.GetMask("Ground"));
    }

    public void ShootBut()
    {
        if (canShoot == true && isGrounded == true)
        {
            anim.SetBool("Go", false);
            go = 0;
            canShoot = false;
            anim.SetTrigger("StartShoot");
            soundSource.PlayOneShot(ArrowSound);
            StartCoroutine(ShootCoolDown());
        }
    }

    public void Shoot()
    {
        if (transform.localScale.x == 1)
        {
            Instantiate(arrow, shotPlace.position, Quaternion.Euler(0, 0, 0));
        }
        else
        {
            Instantiate(arrow, shotPlace.position, Quaternion.Euler(0, 180, 0));
        }
    }

    IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    IEnumerator Hurt()
    {
        if (hp > 0)
        {
            canHurt = false;
            yield return new WaitForSeconds(0.8f);
            canHurt = true;
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        //anim.Play();
        soundSource.PlayOneShot(PlayerDamageWoman);

        StartCoroutine(Hurt());
        HealthChanged?.Invoke(_currentHealth, _health);
        HealthTextChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            //_animator.SetBool("DieBow", true);
            StartCoroutine(PlayerDie());
        }
    }

    IEnumerator PlayerDie()
    {
        soundSource.PlayOneShot(DeathSound);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject, 0.8f);
        Die();
    }

    private void Die()
    {
        StopCoroutine(Hurt());
        StopCoroutine(PlayerDie());
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        print("Die");
    }
}