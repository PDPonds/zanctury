using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ZombieData
{
    public float enemyLife;
    public float enemyDamage;
    public float alertArea;
    public float sawRange;
    public float atkArea;
    public float atkRange;
    public float atkDuration;
    public float OutOfSightDuration;
    public void ZombieDatas()
    {
        enemyLife = Random.Range(15f, 20f);
        enemyDamage = Random.Range(10f, 15f);
        alertArea = 10f;
        sawRange = 10f;
        atkArea = 3f;
        atkRange = 1f;
        atkDuration = 2f;
        OutOfSightDuration = 5f;
    }
}

