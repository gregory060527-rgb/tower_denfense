using System;
using System.Numerics;
using UnityEngine;

[RequireComponent(typeof(PlaceTower))]
public class PlaceTower : MonoBehaviour
{
    private TowerManager towerManager;
    private player player;

    [SerializeField] private SpriteRenderer rangeSprite;
    [SerializeField] private CircleCollider2D rangeCollider;
    [SerializeField] private Color gray;
    [SerializeField] private Color red;

    [NonSerialized] public bool isplacing = true;
    private bool isRestricted = false;
    private TowerManager TowerManager;
    private player Player;

    private Towers towers;

    void Awake()
    {
        towers = GetComponent<Towers>();
        rangeCollider.enabled = false;
    }

    void Update()
    {
        if(isplacing)
        {
            UnityEngine.Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = mousePosition;
        }
        
        if (Input.GetMouseButtonDown(1) && !isRestricted && towerManager.Cost < player.main.money)

        {
            rangeCollider.enabled = true;
            isplacing = false;
            rangeSprite.enabled = false;
            player.main.money -= towers.cost;
            GetComponent<PlaceTower>().enabled = false;
        }
        
        if(isRestricted)
        {
            rangeSprite.color = red;
        }
        else
        {
            rangeSprite.color = gray;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "restricted" || collision.gameObject.tag == "tower" && isplacing)
        {
            isRestricted = true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "restricted" || collision.gameObject.tag == "tower" && isplacing)      
        {
            isRestricted = false;
        }
    }
}
