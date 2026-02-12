using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public static player main;
    
    [SerializeField] private int healh = 500;
    public int money = 100;

    [SerializeField] private TextMeshProUGUI HPGui;
    [SerializeField] private TextMeshProUGUI MoneyGUi;

    void Awake()
    {
        
    }

    void Update()
    {
        HPGui.text = "HP: " + healh.ToString();
        MoneyGUi.text = "money " + money.ToString();
    }
}
