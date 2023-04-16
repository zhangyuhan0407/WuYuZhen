using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDSkill_Rage : MonoBehaviour
{

    TDOperator ope;

    bool isRage;

    // Start is called before the first frame update
    void Start()
    {
        ope = GetComponent<TDOperator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ope.hp < ope.maxHP / 2)
        {
            EnterRage();
        }
        else
        {
            ExitRage();
        }
    }


    public void EnterRage()
    {
        if(isRage)
        {
            return;
        }
        isRage = true;
        RageEffect();
    }
    public void ExitRage()
    {
        if(isRage == false)
        {
            return;
        }
        isRage = false;
        ope.atk = (int)((float)ope.atk / 1.4f);
    }


    public void RageEffect()
    {
        //ope.atk = (int)((float)(ope.atk)* 1.40);
        
        float deltaATK = (float)(ope.atk) * 0.4f;
        ope.atk = ope.atk + (int)deltaATK;      
    }


}
