using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Scripting.APIUpdating;

public class Enemymanager : MonoBehaviour
{
    
    public static Enemymanager main;
    public Transform spawnPoint;
    public Transform[] Checkpoints;
    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject esquelto;
    [SerializeField] private GameObject fantsma;

    [SerializeField] private int wave = 1;
    [SerializeField] private int enemyCount = 6;
    [SerializeField] private float enemyCountRate = 0.2f;
    [SerializeField] private float spawnDelayMax = 1f;
    [SerializeField] private float spawnDelayMin = 0.75f;

    [SerializeField] private float zombieRate = 0.5f;
    [SerializeField] private float esqueltoRate = 0.4f; 
    [SerializeField] private float fantsmaRate = 0.2f;

    [SerializeField] private GameObject nextpanel;
    private bool waveover = false;
    private bool wavedome = false;
    private List<GameObject> waveset = new List<GameObject>();
    private int enemyleft;
    private int count = 0;
    private int zombieCount;
    private int esqueltoCount;  
    private int fantsmaCount;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        SetWave();
    }
    
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (Input.GetKeyDown(KeyCode.Return) && wavedome && enemies.Length == 0)
        {
            wave++;
            wavedome = false;
            enemyCount = Mathf.RoundToInt(enemyCount * enemyCountRate);
            SetWave();
        }

        if(waveover && wavedome && enemies.Length== 0)
        {
            player.main.money += 50 + (10 + wave);
            waveover=true;
            nextpanel.SetActive(true);
        }

    }

    private void SetWave()
    {
        zombieCount = Mathf.RoundToInt(enemyCount * (zombieRate + fantsmaRate));
        esqueltoCount = Mathf.RoundToInt(enemyCount * esqueltoRate);
        fantsmaCount = 0;

        if(wave % 5 == 0)
        {
            zombieCount = Mathf.RoundToInt(enemyCount * zombieRate);
            fantsmaCount += Mathf.RoundToInt(enemyCount * fantsmaRate);
        } 
        
        enemyleft = zombieCount + esqueltoCount + fantsmaCount;
        enemyCount = enemyleft;

        waveset = new List<GameObject>();
        
        for (int i = 0; i < zombieCount; i++)
        {
            waveset.Add(zombie);
        }
        for (int i = 0; i < esqueltoCount; i++)
        {
            waveset.Add(esquelto);
        }
        for (int i = 0; i < fantsmaCount; i++)
        {
            waveset.Add(fantsma);
        }

        waveset = Shuffle(waveset);

        StartCoroutine(spawn());
    }
    
    public List<GameObject> Shuffle(List<GameObject> waveSet)
    {
        List<GameObject> temp = new List<GameObject>();
        List<GameObject> result = new List<GameObject>();
        temp.AddRange(waveSet);

        for (int i = 0; i < waveSet.Count; i++)
        {
            int index = Random.Range(0, temp.Count - 1);
            result.Add(temp[index]);
            temp.RemoveAt(index);
        }

        return result;
    }
    public void NextWave()
    {
        
    }
   
    IEnumerator spawn()
    {

        for (int i = 0; i < waveset.Count; i++)
        {
            Instantiate(waveset[1], spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(spawnDelayMin,spawnDelayMax));

        }

        wavedome = true;    
    }
    
}