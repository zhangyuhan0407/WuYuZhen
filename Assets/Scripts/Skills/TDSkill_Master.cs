using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDSkill_Master : TDSkill
{


    public override void CastSkill()
    {
        base.CastSkill();

        EnemyController[] Enemies = GameObject.FindObjectsOfType<EnemyController>();
        if (Enemies.Length == 0)
        {
            return;
        }
        for (int i = 0; i < Enemies.Length; i++)
        {
            EnemyController enemy = Enemies[i];
            enemy.DecreaseHP(op.atk);
        }
    }
}
