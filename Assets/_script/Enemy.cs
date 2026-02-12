using System;
using System.Numerics;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int health = 50;
    [SerializeField] private float moveSpeed = 2f;
      
      private Rigidbody2D rb;

      private Transform checkPoint;

      [NonSerialized] public int index = 0;
      [NonSerialized] public float distance = 0f;

     void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        checkPoint = Enemymanager.main.Checkpoints[index];
    }


    void Update()
    {
        checkPoint = Enemymanager.main.Checkpoints[index];
        distance = UnityEngine.Vector2.Distance(transform.position, Enemymanager.main.Checkpoints[index].position);
        
        if(UnityEngine.Vector2.Distance(transform.position, checkPoint.position) < 0.1f)
        {
           index++;
           if(index >= Enemymanager.main.Checkpoints.Length)
              {
                Destroy(gameObject);
              }
        }
    }

    void FixedUpdate()
    {
        UnityEngine.Vector2 direction = (checkPoint.position - transform.position).normalized;
        transform.right = checkPoint.position - transform.position;
        rb.linearVelocity = direction * moveSpeed;
    }

    public void damage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

