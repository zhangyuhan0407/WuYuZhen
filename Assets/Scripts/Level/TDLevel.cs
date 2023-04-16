using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDLevel : MonoBehaviour
{

    public static TDLevel Instance;

    private void Awake()
    {
        Instance = this;
    }


    public List<string> enemyNames;
    public List<float> enemySpawnTime;
    public List<int> pathIndex;
    

    [HideInInspector]
    public List<TDEnemy> enemies;


    public float clicker;

    public List<int> path1;
    public List<int> path2;
    public List<int> path3;


    // Start is called before the first frame update
    void Start()
    {
        clicker = 0;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (var id in path1)
        {
            Map.instanse.cells[id].GetComponent<Image>().color = Color.yellow;
        }

        foreach (var id in path2)
        {
            Map.instanse.cells[id].GetComponent<Image>().color = Color.yellow;
        }


        foreach (var id in path3)
        {
            Map.instanse.cells[id].GetComponent<Image>().color = Color.yellow;
        }

        if (TDGameManager.instance.IsGameStart == false)
        {
            return;
        }

        clicker += Time.deltaTime;

        for(int i = 0; i<enemySpawnTime.Count;i++)
        {
            if(Check(clicker, enemySpawnTime[i]))
            {
                List<int> path = path1;
                if(pathIndex[i] == 1)
                {
                    path = path1;
                }
                else if (pathIndex[i] == 2)
                {
                    path = path2;
                }
                else if (pathIndex[i] == 3)
                {
                    path = path3;
                }

                CreateEnemey(enemyNames[i], path);
                enemyNames.RemoveAt(i);
                enemySpawnTime.RemoveAt(i);
                pathIndex.RemoveAt(i);
                break;
            }
        }


    }



    public void CreateEnemey(string name , List<int> path)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Enemies/" +name);
        GameObject enemy = Instantiate(prefab);
        enemy.transform.SetParent(Map.instanse.cells[path[0]].transform);
        enemy.gameObject.transform.position = Map.instanse.cells[path[0]].transform.position;

        enemy.GetComponent<EnemyController>().Destinations = new List<GameObject>();
        foreach (var id in path)
        {
            enemy.GetComponent<EnemyController>().Destinations.Add(Map.instanse.cells[id].gameObject);
        }
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
