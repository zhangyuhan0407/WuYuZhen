using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDGameManager : MonoBehaviour
{
    public static TDGameManager instance;
    private void Awake()
    {
        instance = this;
    }
    public int EnemyCount;
    public int maxEnemyCount;
    public Text Lose;
    public Text state;
    public bool IsGameStart;
    public bool IsGameEnd;

    public Text textCost;
    public int cost;
    public float maxCDTime;
    [HideInInspector]
    public float cdTime;

    public GameObject winPanel;
    public GameObject losePanel;

    // Start is called before the first frame update
    void Start()
    {
        EnemyCount = 0;
        Lose.text = "Life: " + maxEnemyCount + "";
        IsGameStart = false;
        state.text = "Start";
        cdTime = maxCDTime;
        IsGameEnd = false;

        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameStart)
        {
            if (!IsGameEnd)
            {
                IsLose();
                IsWin();
                IncreaseAP();
            }
        }
    }

    public void IncreaseAP()
    {
        cdTime -= Time.deltaTime;
        textCost.text = "Cost: " + cost;
        if (cdTime < 0)
        {
            cost += 1;
            cdTime = maxCDTime;
        }
    }
    public void EnemyEnterDoor() 
    { 
        EnemyCount++;
        Lose.text = "Life: "+ (maxEnemyCount - EnemyCount)+ "";
    }
    public void StartGame()
    {
        IsGameStart= !IsGameStart;
        if (state.text == "Start")
        {
            state.text = "Pause";
        }
        else
        {
            state.text = "Start";
        }

        GameObject.Find("HeroPanel").GetComponent<TDHeroPanel>().Accept(TDPlayerSystem.Instance.heroes);


    }




    public void IsLose()
    {
        if (EnemyCount >= maxEnemyCount)
        {
            IsGameEnd = true;
            losePanel.SetActive(true);
            StartGame();
        }
        return;
    }

    public void IsWin()
    {
        if(TDLevel.Instance.HasUnspawnedEnemies())
        {
            return;
        }

        EnemyController[] Enemies = GameObject.FindObjectsOfType<EnemyController>();
        if (Enemies.Length == 0)
        {
            IsGameEnd = true;
            winPanel.SetActive(true);
            StartGame();
        }
        return;
    }


}
