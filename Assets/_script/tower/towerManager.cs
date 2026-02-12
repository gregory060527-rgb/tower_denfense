using JetBrains.Annotations;
using Mono.Cecil;
using NUnit.Framework.Internal.Commands;
using Unity.VisualScripting;
using UnityEngine;

public class towerManager : MonoBehaviour
{
    [Header("Tower stats")]
    [SerializeField] private GameObject sniper;
    [SerializeField] private GameObject pistola;
    [SerializeField] private GameObject soldado;
    [SerializeField] private LayerMask towerLayer;
    private GameObject SelectedTower;
    private GameObject placingTower;
    public int Cost = 100;
   void Update()
{
     if (Input.GetKeyDown(KeyCode.Escape))
    {
        ClearSelection();
    }

    if (placingTower)
    {
        PlaceTower placeTowerComp = placingTower.GetComponent<PlaceTower>();
        if (placeTowerComp != null && !placeTowerComp.isplacing)
        {
            placingTower = null;
        }
    }

    if (Input.GetMouseButtonDown(0))
    {
        RaycastHit2D hit = Physics2D.Raycast(
            Camera.main.ScreenToWorldPoint(Input.mousePosition),
            Vector2.zero,
            100f,
            towerLayer
        );

        if (hit.collider != null)
        {
            if (SelectedTower && SelectedTower.transform.childCount > 1)
            {
                GameObject range1 = SelectedTower.transform.GetChild(1).gameObject;
                range1.GetComponent<SpriteRenderer>().enabled = false;
            }

            SelectedTower = hit.collider.gameObject;

            if (SelectedTower.transform.childCount > 1)
            {
                GameObject range2 = SelectedTower.transform.GetChild(1).gameObject;
                range2.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    if (Input.GetKeyDown(KeyCode.U) && SelectedTower != null)
    {
        Towers towerComponent = SelectedTower.GetComponent<Towers>();
        if (towerComponent != null)
        {

        }
    }
}

    private void ClearSelection()
    {
        if(placingTower)
        {
            Destroy(placingTower);
            placingTower = null;
        }
    }

    public void setTower(GameObject tower)
    {
        ClearSelection();
        placingTower = Instantiate(tower);
    }
    public void SelectSniper()
    {
    setTower(sniper);
    }

    public void SelectPistola()
    {
    setTower(pistola);
    }

    public void SelectSoldado()
    {   
    setTower(soldado);
    }
    
    

}
