using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroler :Unit
{
    

    public int positionOfPatrol;

    bool movingRight;

    public Transform groundCheck;
    public float checkRadius;

    public RaycastHit2D hit;
    public int templives;

    Transform player;

    public float stoppingDistance;
    bool chill = false;
    bool angry = false;
    bool goBack = false;
    Vector3 point = new Vector3(0, 0, 0);

    private Animator animator;

    

    private CharState State
    {
        get
        {
            return (CharState)gameObject.GetComponent<Animator>().GetInteger("Parametr");
        }

        set
        {
          gameObject.GetComponent<Animator>().SetInteger("Parametr", (int)value);
        }

    }
    void Start()
    {
        timeBtwAttack = 2;
        this.damage = 3;
        this.currentlives = 100;
        point = transform.position;
        speed = 4;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius,LayerMask.NameToLayer("Ground"));
    }
    
    // Update is called once per frame
    void Update()
    {
       
        if (checkObstacle())
        {
            Jump();
        }
        if (isGrounded)
        {
            extraJump = extraJumpsValue;
        }
        if (Vector2.Distance(transform.position, point) < positionOfPatrol && !angry)
        {
            chill = true;
        }
        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }
        else
        {
            goBack = true;
            angry = false;
        }
        if (chill)
        {
            Chill();
        }
        else if(angry)
        {
            Angry();
        }
        else if (goBack)
        {
            Goback();
        }
        
        if (currentlives < 0)
        {
            Die();
        }
       
    }
    void Chill()
    {
        State = CharState.Run;
        if (transform.position.x > point.x + positionOfPatrol)
        {
            movingRight = false;
        }
        else if (transform.position.x < point.x - positionOfPatrol)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        

    }
    public override void ReceiveDamage(int damage)
    {
        this.currentlives -= damage;
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        gameObject.GetComponent<SpriteRenderer>().flipX = transform.position.x<0.0f;
        State = CharState.Run;
        Attack();

    }
    void Goback()
    {
        transform.position = Vector2.MoveTowards(transform.position, point, speed * Time.deltaTime);
        gameObject.GetComponent<SpriteRenderer>().flipX = transform.position.x < 0.0f;
        State = CharState.Run;
    }
    private void Jump()
    {
       
        if (gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;

        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        }
        State = CharState.Jump;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
        extraJump--;
    }
    bool checkObstacle()
    {
        Physics2D.queriesStartInColliders = false;
        hit = Physics2D.Raycast(gameObject.transform.position, Vector2.right * transform.localScale.x, 1.0f);

        if (hit.collider != null && hit.collider.tag == "Ground" || (hit.collider != null && hit.collider.tag=="Item") || (hit.collider != null && hit.collider.tag == "Obstacle"))
        {
            
            return true;  
         
        }
        return false;
    }
    void Attack()
    {
       
        if (timeBtwAttack <= 0)
        {
           
                State = CharState.Attack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange,LayerMask.GetMask("Player"));
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                enemiesToDamage[i].GetComponent<Gamer>().ReceiveDamage(damage);
                    
                }
            
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }
   
    


}
