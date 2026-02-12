using System;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour
{
    [Header("Tower stats")]
    public float range = 8f;
    public int damage = 25;
    public float fireRate = 1f;
    public int cost = 50;

    [Header("Targeting Mode")]
    public bool firt = true;
    public bool last = false;
    public bool strong = false;

    
    [Header("Effector")]
    [SerializeField] private GameObject fireEffect;

    [NonSerialized] 
    public GameObject Target;
    private float cooldown = 0f;
    void Start()
    {
        fireEffect.SetActive(false);
    }

    void Update()
    {
       if(Target)
        {
            if(cooldown >= fireRate)
            {
                transform.right = Target.transform.position - transform.position;

                Target .GetComponent<Enemy>().damage(damage);
                cooldown = 0f;
                StartCoroutine(FireEffect());
            }
            else
            {
                cooldown += Time.deltaTime;
            }
            
        }
    }

    System.Collections.IEnumerator FireEffect()
    {
        fireEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        fireEffect.SetActive(false);
    }
        
    
}
