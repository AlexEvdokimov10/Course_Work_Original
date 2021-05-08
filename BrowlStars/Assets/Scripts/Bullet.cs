
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Bullet : MonoBehaviour
{
    private GameObject parent;
    public ThingsType thingstype;
    public GameObject Parent
    {
        set
        {
            parent = value;
        }
    }
    private float speedBullet=40.0f;
    private Vector3 direction;

    private SpriteRenderer sprite;
    public Vector3 Direction
    {
        set
        {
            direction = value;
        }
    }
    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speedBullet * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit.gameObject!=parent)
        {
            Destroy(gameObject);
        }
    }

}
