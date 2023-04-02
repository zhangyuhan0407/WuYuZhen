using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [HideInInspector]
    public int hp;
    public int maxHP;
    public int atk, def;
    public float attackRange;
    public float cd;

    float speed;
    Animator ani;
    TDBar enemyHPBar;
    int index;

    

    public List<GameObject> Destinations;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        speed = 100;
        index = 0;
        cd = 2.0f;
        this.hp = this.maxHP;


        GameObject prefabHPBar = Resources.Load<GameObject>("Prefabs/HPBar");
        enemyHPBar = Instantiate(prefabHPBar).GetComponent<TDBar>();
        enemyHPBar.transform.SetParent(this.transform);


        Destinations = new List<GameObject>();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Destination"))
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
        cd -= Time.deltaTime;
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
        TDOperatorAnimation[] Players = GameObject.FindObjectsOfType<TDOperatorAnimation>();
        if (Players.Length > 0 && cd >= 0)
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
        TDOperator[] Players = GameObject.FindObjectsOfType<TDOperator>();
        if (Players.Length == 0)
        {
            return;
        }
        if (cd >= 0)
        {
            return;
        }
        TDOperator player = Players[0];
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
        if (mindis < attackRange)
        {
            ani.SetTrigger("Attack");
            player.DecreaseHP(this.atk);
            cd = 2.0f;
        }
    }
    public void DecreaseHP(int value)
    {
        int realValue = value - this.def;
        realValue = Mathf.Max(0, realValue);
        hp -= realValue;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
