using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public int hp,maxHP;
    float speed,EnemyCD;
    public List<GameObject> Destinations;
    public float EnemyAttackRange;
    public Animator ani;
    public HPBar enemyHPBar;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        ani= GetComponent<Animator>();
        speed = 100;
        index = 0;
        EnemyCD = 2.0f;
        GameObject prefabHPBar = Resources.Load<GameObject>("Prefabs/HPBar");
        enemyHPBar = Instantiate(prefabHPBar).GetComponent<HPBar>();
        enemyHPBar.transform.SetParent(this.transform);


        Destinations = new List<GameObject>();
        foreach(var obj in GameObject.FindGameObjectsWithTag("Destination"))
        {
            Destinations.Add(obj);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (TDGameManager.instance.IsGameStart == false) 
        {
           return;
        }
        if (enemyHPBar != null)
        {
            enemyHPBar.SetHP(hp, maxHP);
            enemyHPBar.gameObject.transform.localPosition = new Vector2(0, -200);
        }
        if (CheckReachDestination())
        {
            index++;
        }
        EnemyCD -= Time.deltaTime;
        Move();
        EnemyAttack();
    }
    public void Move()
    {
        if (index >= Destinations.Count)
        {
            Destroy(gameObject);
            TDGameManager.instance.EnemyEnterDoor();
            return;
        }
        if (HasButCD())
        {
            ani.SetBool("Walk", false);
        }
        else
        {
            ani.SetBool("Walk", true);
            Vector3 dir = (Destinations[index].transform.position - transform.position).normalized;
            transform.position = transform.position + dir * speed * Time.deltaTime;
        }
    }
    public bool CheckReachDestination()
    {
        if (transform.position.x > Destinations[index].transform.position.x - 0.1f && transform.position.x < Destinations[index].transform.position.x + 0.1f)
        {
            if (transform.position.y > Destinations[index].transform.position.y - 0.1f && transform.position.y < Destinations[index].transform.position.y + 0.1f)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasButCD()
    {
        PlayerController[] Players = GameObject.FindObjectsOfType<PlayerController>();
        if (Players.Length > 0 && EnemyCD >= 0)
        {
            return true;
        }
        else
        { 
            return false; 
        }
    }
    void EnemyAttack()
    {
        PlayerController[] Players = GameObject.FindObjectsOfType<PlayerController>();
        if (Players.Length == 0)
        {
            return;
        }
        if (EnemyCD >= 0)
        {
            return;
        }
        PlayerController player = Players[0];
        float mindis = (player.transform.position - transform.position).magnitude;
        for (int i = 0; i < Players.Length; i++)
        {
            float dis = (Players[i].transform.position - transform.position).magnitude;
            if (dis < mindis)
            {
                player = Players[i];
                mindis = dis;
            }
        }
        if (mindis < EnemyAttackRange)
        {
            ani.SetTrigger("Attack");
            player.Decreasehp();
            EnemyCD = 2.0f;
        }
    }
    public void Decreasehp()
    {
        hp--;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
