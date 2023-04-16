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
    public float cd,maxCD;

    float speed;
    Animator ani;
    TDBar enemyHPBar;

    public int destIndex = 0;
    public List<GameObject> Destinations;

    TDOperator player;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        speed = 100;
        //if(destIndex != 0)
        //{
        //    destIndex = 0;
        //}
        
        cd= 0;
        this.hp = this.maxHP;


        GameObject prefabHPBar = Resources.Load<GameObject>("Prefabs/HPBar");
        enemyHPBar = Instantiate(prefabHPBar).GetComponent<TDBar>();
        enemyHPBar.transform.SetParent(this.transform);
        enemyHPBar.transform.localPosition = this.transform.position - new Vector3(0, -50, 0);

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
            enemyHPBar.gameObject.transform.localPosition = new Vector2(0, -50);
        }
        if (CheckReachDestination())
        {
            destIndex++;
        }
        cd -= Time.deltaTime;
        Move();
        EnemyAttack();
    }
    public void Move()
    {
        if (destIndex >= Destinations.Count)
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
            Vector3 dir = (Destinations[destIndex].transform.position - transform.position).normalized;
            transform.position = transform.position + dir * speed * Time.deltaTime;
        }
    }
    public bool CheckReachDestination()
    {
        if (transform.position.x > Destinations[destIndex].transform.position.x - 0.1f && transform.position.x < Destinations[destIndex].transform.position.x + 0.1f)
        {
            if (transform.position.y > Destinations[destIndex].transform.position.y - 0.1f && transform.position.y < Destinations[destIndex].transform.position.y + 0.1f)
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
        if (mindis < attackRange * 150)
        {
            ani.SetTrigger("Attack");
            cd = maxCD;
            this.player = player;
            Invoke("DamagePlayer", 0.2f);
        }
    }


    public void DamagePlayer()
    {
        player.DecreaseHP(this.atk);
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
