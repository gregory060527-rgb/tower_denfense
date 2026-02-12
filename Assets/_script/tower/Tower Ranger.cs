using System.Numerics;
using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private Towers tower;
    private List<GameObject> targets = new List<GameObject>();

    public int Cost { get; internal set; }

    void Start()
    {
        UpdateRange();
    }

    void Update()
    {
        if (targets.Count > 0)
        {
            if (tower.firt)
            {
                float minDistance = Mathf.Infinity;
                int maxIndex = 0;
                GameObject firtTarget = null;

                foreach (GameObject target in targets)
                {
                   int index = target.GetComponent<Enemy>().index; 
                   float distance = target.GetComponent<Enemy>().distance;

                   if(index < maxIndex || (index == maxIndex && distance < minDistance))
                   {
                       maxIndex = index;
                       minDistance = distance;
                       firtTarget = target;
                   }
                }
                
                tower.Target = firtTarget;

            }
            else if(tower.last)
            {
                float maxDistance = Mathf.NegativeInfinity;
                int minIndex = int.MaxValue;
                GameObject lastTarget = null;

                foreach (GameObject target in targets)
                {
                    int index = target.GetComponent<Enemy>().index;
                    float distance = target.GetComponent<Enemy>().distance;

                    if(index > minIndex || (index == minIndex && distance > maxDistance))
                    {
                        minIndex = index;
                        maxDistance = distance;
                        lastTarget = target;
                    }
                }

                tower.Target = lastTarget;
            
            }
            else if(tower.strong)
            {
                GameObject strongTarget = null;
                float maxHealth = 0;

                foreach (GameObject target in targets)
                {
                    float health = target.GetComponent<Enemy>().health;

                    if(health > maxHealth)
                    {
                        maxHealth = health;
                        strongTarget = target;
                    }
                }
                tower.Target = strongTarget;
            }
            else
            {
                tower.Target = targets[0];
            }

        }
        else
        {   
            tower.Target = null;    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targets.Add(collision.gameObject);          
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targets.Remove(collision.gameObject);
        }
    }
    
       public void UpdateRange()
    {
        transform.localScale = new UnityEngine.Vector3(tower.range, tower.range, tower.range);
    }
}
