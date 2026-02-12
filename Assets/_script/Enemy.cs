using System;
using System.Numerics;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int health = 50;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float checkpointRadius = 0.5f;
      
      private Rigidbody2D rb;

      private Transform checkPoint;

      [NonSerialized] public int index = 0;
      [NonSerialized] public float distance = 0f;
      private bool hasReachedFirstCheckpoint = false;

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
        if(index >= Enemymanager.main.Checkpoints.Length)
        {
            Destroy(gameObject);
            return;
        }

        checkPoint = Enemymanager.main.Checkpoints[index];
        distance = UnityEngine.Vector2.Distance(transform.position, checkPoint.position);
        
        if(distance < checkpointRadius)
        {
           if(index == 0 && !hasReachedFirstCheckpoint)
           {
               hasReachedFirstCheckpoint = true;
           }
           
           if(index > 0 || hasReachedFirstCheckpoint)
           {
               index++;
               if(index < Enemymanager.main.Checkpoints.Length)
               {
                   checkPoint = Enemymanager.main.Checkpoints[index];
               }
           }
        }
    }

    void FixedUpdate()
    {
        if(checkPoint == null || index >= Enemymanager.main.Checkpoints.Length)
        {
            rb.linearVelocity = UnityEngine.Vector2.zero;
            return;
        }

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

