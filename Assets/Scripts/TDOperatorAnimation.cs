using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class TDOperatorAnimation : MonoBehaviour
{

    Animator ani;
    AudioSource aud;
    TDBar hpBar;
    TDBar apBar;
    GameObject iconSkill;

    public AudioClip attackaud;

    TDOperator op;



    void Start()
    {
        ani= GetComponent<Animator>();
        aud= GetComponent<AudioSource>();

        hpBar = transform.Find("HPBar").gameObject.GetComponent<TDBar>();
        apBar = transform.Find("APBar").gameObject.GetComponent<TDBar>();
        iconSkill = transform.Find("SkillIcon").gameObject;

        op = gameObject.GetComponent<TDOperator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hpBar != null)
        {
            hpBar.SetHP(op.hp, op.maxHP);
        }

        if (apBar != null)
        {
            apBar.SetHP(op.ap, op.maxAP);
        }

        if(op.ap >= op.maxAP)
        {
            iconSkill?.SetActive(true);
        }
        else
        {
            iconSkill?.SetActive(false);
        }
    }


    public void MoveToCell(Cell c)
    {
        c.Acceptplayer(this);
    }


    public void PlayAnimation_Attack()
    {
        ani.SetTrigger("Attack");
        aud.clip = attackaud;
        aud.Play();
    }









}