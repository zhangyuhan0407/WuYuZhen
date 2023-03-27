using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDLevel : MonoBehaviour
{

    public static TDLevel Instance;

    private void Awake()
    {
        Instance = this;
    }


    public List<string> enemyNames;
    public List<float> enemySpawnTime;
    public List<int> cellIds;
    

    [HideInInspector]
    public List<TDEnemy> enemies;


    public float clicker;


    // Start is called before the first frame update
    void Start()
    {
        clicker = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(TDGameManager.instance.IsGameStart == false)
        {
            return;
        }


        clicker += Time.deltaTime;

        for(int i = 0; i<enemySpawnTime.Count;i++)
        {
            if(Check(clicker, enemySpawnTime[i]))
            {
                CreateEnemey(enemyNames[i], Map.instanse.cells[cellIds[i]]);
                enemyNames.RemoveAt(i);
                enemySpawnTime.RemoveAt(i);
                cellIds.RemoveAt(i);
                break;
            }
        }
        
    }



    public void CreateEnemey(string name ,Cell cell)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Enemies/" +name);
        GameObject enemy = Instantiate(prefab);
        enemy.transform.SetParent(cell.transform);
        enemy.gameObject.transform.position = cell.transform.position;
    }



    bool Check(float a, float b)
    {
        if(a > b)
        {
            return true;
        }

        return false;
    }


    public bool HasUnspawnedEnemies()
    {
        return this.enemyNames.Count > 0;
    }


}
