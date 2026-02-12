using UnityEngine;

public class upgradeTower : MonoBehaviour
{
    [System.Serializable]
    class Level
    {
        public float range = 8f;
        public int damage = 25;
        public float fireRate = 1f;
    }
    [SerializeField] private Level[] levels = new Level[3];
    [System.NonSerialized] public int currentLevel = 0;

    private Towers tower;


    void Awake()
    {
         tower = GetComponent<Towers>();   
    }
    
    public void upgrade()
    {
        if(currentLevel < levels.Length)
        {
            tower.range = levels[currentLevel].range;
            tower.damage = levels[currentLevel].damage;
            tower.fireRate = levels[currentLevel].fireRate;

            currentLevel++;

            Debug.Log("upgrade");
        }

    }

}
