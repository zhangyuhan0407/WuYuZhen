using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDSkill_Warrior : TDSkill
{
    

    public override void CastSkill()
    {
        base.CastSkill();

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
        if (mindis <= 1)
        {
            for (int i = 0; i <= 12; i++)
                enemy.DecreaseHP(op.atk);
        }
    }


}
