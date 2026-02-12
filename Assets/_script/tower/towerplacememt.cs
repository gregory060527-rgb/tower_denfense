using System;
using System.Numerics;
using UnityEngine;

public class PlaceTower : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rangeSprite;
    [SerializeField] private CircleCollider2D rangeCollider;
    [SerializeField] private Color gray;
    [SerializeField] private Color red;

    [NonSerialized] public bool isplacing = true;
    private bool isRestricted = false;
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
        
        if (Input.GetMouseButtonDown(1) && !isRestricted && towers.cost <= player.main.money)
        {
            rangeCollider.enabled = true;
            isplacing = false;
            rangeSprite.enabled = false;
            player.main.money -= towers.cost;
            GetComponent<PlaceTower>().enabled = false;
        }
        
        rangeSprite.color = isRestricted ? red : gray;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag == "restricted" || collision.gameObject.tag == "tower") && isplacing)
        {
            isRestricted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if((collision.gameObject.tag == "restricted" || collision.gameObject.tag == "tower") && isplacing)
        {
            isRestricted = false;
        }
    }
}
