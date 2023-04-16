using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TDOperator : MonoBehaviour, IPointerClickHandler
{

    [HideInInspector]
    public int hp, ap;
    public int maxHP, maxAP, clss;
    public float cd, maxCD;
    public float AttackRange;

    public int atk, def, mag, spd;

    TDOperatorAnimation player;
    TDSkill skill;

    float tempAP;


    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<TDOperatorAnimation>();
        skill = gameObject.GetComponent<TDSkill>();

        hp = maxHP;
        ap = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (TDGameManager.instance.IsGameStart == false)
        {
            return;
        }

        cd -= Time.deltaTime;
        tempAP += Time.deltaTime;
        ap = Mathf.Min((int)tempAP, maxAP);

        if (cd <= 0)
        {
            Attack();
            
        }
        
    }


    void Attack()
    {
        EnemyController[] Enemies = GameObject.FindObjectsOfType<EnemyController>();
        if (Enemies.Length == 0)
        {
            return;
        }

        EnemyController enemy = Enemies[0];
        float mindis = (enemy.transform.position - transform.position).magnitude;
        for (int i = 0; i < Enemies.Length; i++)
        {
            float dis = (Enemies[i].transform.position - transform.position).magnitude;
            if (dis < mindis)
            {
                enemy = Enemies[i];
                mindis = dis;
            }
        }
        if (mindis < AttackRange * 150)
        {
            player.PlayAnimation_Attack();
            enemy.DecreaseHP(this.atk);
            cd = 1.5f;
        }
    }



    void Skill()
    {
        if(this.skill == null)
        {
            return;
        }
        skill.CastSkill();
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

    public void OnPointerClick(PointerEventData eventData)
    {

        if(this.ap < this.maxAP)
        {
            HintManager.Instance.ShowHintText("Not Enough AP");
            return;
        }

        this.ap = 0;
        this.tempAP = 0;
        Skill();

    }


    public void MoveToCell(Cell c)
    {
        c.Acceptplayer(this.player);
    }


    private void OnDestroy()
    {
        Debug.Log(gameObject.name);
    }


}
