using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Deathscreen deathscreen;
    public float currentlives = 100;
    protected float speed = 5.0f;
    protected float jumpForce = 5.0f;
    protected bool isGrounded;
    protected int extraJump;
    protected int extraJumpsValue;
    protected int damage;
    

    protected float timeBtwAttack;
    protected float startTimeBtwAttack;

   
    protected float attackRange;

    public virtual void ReceiveDamage(int damage)
    {
        currentlives -= damage;
    }
    protected virtual void Die()
    {
            Destroy(gameObject);   
    }
     public virtual Vector3 Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        

        return direction;

    }
    
    
    
}
