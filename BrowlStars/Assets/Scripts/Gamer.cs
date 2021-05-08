using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamer : Unit
{
    [SerializeField]


    public bool hold;
    public float distance = 1f;
    public RaycastHit2D hit;
    public Transform holdPoint;
    public float thowObject = 5f;
    public int currentfood = 100;
    public Transform attackPose;



    [SerializeField]



    new private Rigidbody2D rigidbody;
    private Animator animator;


    private CharState State
    {
        get
        {
            return (CharState)animator.GetInteger("Paramentr");
        }

        set
        {
            animator.SetInteger("Paramentr", (int)value);
        }

    }

    public SpriteRenderer sprite;

    private Bullet bullet;
    public LayerMask whatIsGround;
    //позиция для проверки касания земли
    public Transform groundCheck;
    public float checkRadius;
    private int maxHealth, maxFood;
    public Image barHealth;
    public Image barFood;
    public Transform allWeapons;


    public int minusFood;
    private float timeMinus;
    private float timeMinusHealth;
    public Deathscreen deathscreen;
    public LayerMask whatIsEnemy;
    public int damage;


    private void Start()
    {
        jumpForce = 5;
        animator = GetComponent<Animator>();
        maxHealth = 100;
        maxFood = 100;
        currentlives = 90;
        currentfood = 100;
        barHealth.fillAmount = currentlives * 0.01f;
        barFood.fillAmount = currentfood * 0.01f;
        attackRange = 1f;
        damage = 1;

        minusFood = 10;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }
    private void Update()
    {
        if (isGrounded)
        {
            State = CharState.Idle;
        }
        if (currentlives > maxHealth)
        {
           
            barHealth.fillAmount = currentlives * 0.01f;
        }
        timeMinus += Time.deltaTime;
        if (timeMinus >= minusFood)
        {
            timeMinus = 2;
            currentfood -= 1;
            barFood.fillAmount = currentfood * 0.01f;
        }
        if (currentfood <= 0)
        {
            timeMinusHealth += Time.deltaTime;
            if (timeMinusHealth >= minusFood)
            {
                currentlives -= 10;
                timeMinusHealth = 0;
                barHealth.fillAmount = currentlives * 0.01f;
            }
        }
        if (currentfood > maxFood)
        {
            
            barFood.fillAmount = currentfood * 0.01f;
        }
        if (currentfood <= 0)
        {
           
            barFood.fillAmount = currentfood * 0.01f;
        }
        if (currentlives < 0)
        {
            GameOver();
            
       
            
        }
        
        if (isGrounded)
        {
            extraJump = extraJumpsValue;
        }
        if (Input.GetButtonDown("Fire1"))
        {

        }
        if (Input.GetButton("Horizontal"))
        {

            GamerRun();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump > 0)
        {
            Jump();

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump == 0 && isGrounded == true)
        {
            rigidbody.velocity = Vector2.up * jumpForce;
            State = CharState.Jump;
        }
        Flip();

        Attack();

    }
    void GamerRun()
    {
        FixedUpdate();


        sprite.flipX = Run().x < 0.0f;


        State = CharState.Run;
    }
    private void Jump()
    {
        rigidbody.velocity = Vector2.up * jumpForce;
        State = CharState.Jump;

        extraJump--;
    }

    public override void ReceiveDamage(int damage)
    {
        currentlives-=damage;
        barHealth.fillAmount = currentlives * 0.01f;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 1f, ForceMode2D.Impulse);
        Debug.Log(currentlives);
    }
    
    public void GameOver()
    {
        deathscreen.Setup();
        Time.timeScale = 0;
       

    }
    private void Shoot()
    {
        Vector3 position = transform.position;
        position.x -= 2.0f;
        position.y += 0.5f;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.gameObject.GetComponent<Unit>();
        if (unit)
        {
            Debug.Log(unit);
            ReceiveDamage(6);
        }

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }


    void Attack()
    {
        ActiveWeapon();
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                State = CharState.Attack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPose.position, attackRange, LayerMask.GetMask("Enemy"));
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Patroler>().ReceiveDamage(damage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPose.position, attackRange);
    }
    void ActiveWeapon()
    {
        for (int i = 0; i < gameObject.transform.GetChild(2).gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(2).gameObject.transform.GetChild(i).gameObject.activeSelf)
            {
                attackRange += gameObject.transform.GetChild(2).gameObject.transform.GetChild(i).gameObject.GetComponent<OpenThings>().itemOpen.attackRange;
                damage += (int)gameObject.transform.GetChild(2).gameObject.transform.GetChild(i).gameObject.GetComponent<OpenThings>().itemOpen.damage;
            }
        }

    }
    void Flip()
    {
        for (int i = 0; i < gameObject.transform.GetChild(2).gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetComponent<SpriteRenderer>().flipX)

            {

                gameObject.transform.GetChild(2).gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
                gameObject.transform.GetChild(2).gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().flipY = false;
            }
        }

    }
}


public enum CharState{
    Idle,
    Run,
    Jump,
    Attack

}