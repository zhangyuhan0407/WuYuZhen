using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour,IPointerClickHandler
{
    float CD,MasterCD,WarrorCD,HunterCD;
    public int hp, maxHP, clss;
    // Start is called before the first frame update
    public float AttackRange;
    public Animator ani;
    public AudioSource aud;
    public AudioClip attackaud;
    public HPBar hpBar;
    void Start()
    {
        ani= GetComponent<Animator>();
        aud= GetComponent<AudioSource>();
        CD = 1.0f;

        GameObject prefabHPBar = Resources.Load<GameObject>("Prefabs/HPBar");
        
        hpBar = Instantiate(prefabHPBar).GetComponent<HPBar>();
        hpBar.transform.SetParent(this.transform);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            Map.instanse.cells[3].Acceptplayer(this);
        }
        if (TDGameManager.instance.IsGameStart == false)
        {
            return;
        }

        if (hpBar != null)
        {
            hpBar.SetHP(hp, maxHP);
            hpBar.gameObject.transform.localPosition = new Vector2(0, -270);
        }
        CD -= Time.deltaTime;
        Attack();
    }
    void Attack()
    {
        EnemyController[] Enemies =  GameObject.FindObjectsOfType<EnemyController>();
        if (Enemies.Length == 0)
        {
            return;
        }
        if (CD >= 0)
        {
            return;
        }
        EnemyController enemy= Enemies[0];
        float mindis=(enemy.transform.position - transform.position).magnitude;
        for (int i=0;i<Enemies.Length;i++) 
        {
            float dis = (Enemies[i].transform.position - transform.position).magnitude;
            if (dis < mindis)
            {
                enemy = Enemies[i];
                mindis = dis;
            }
        }
        if (mindis < AttackRange)
        {
            ani.SetTrigger("Attack");
            Invoke("PlayerAudioAttack",0.05f);
            enemy.Decreasehp();
            CD = 1.5f;
        }
    }
    public void WarriorSkill()
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
        if (mindis < AttackRange)
        {   
            for (int i = 0;i <= 12;i++)  
            enemy.Decreasehp();
        }
    }
    public void HunterSkill()
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
        for (int i = 0; i <= 8; i++)
        {
            enemy.Decreasehp();
        }
    }
    public void MasterSkill()
    {
        EnemyController[] Enemies = GameObject.FindObjectsOfType<EnemyController>();
        if (Enemies.Length == 0)
        {
            return;
        }
        for (int i = 0; i < Enemies.Length; i++)
        {
            EnemyController enemy = Enemies[i];
            enemy.Decreasehp();
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
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clss == 1)
        { 
                
        }
        else if (clss == 2)
             {
                HunterSkill();
             }
        else if (clss == 3)
            {
                 MasterSkill();
            }
    }
    public void MoveToCell(Cell c)
    {
        c.Acceptplayer(this);
    }
    public void PlayerAudioAttack()
    {
        aud.clip = attackaud;
        aud.Play();
    }
}