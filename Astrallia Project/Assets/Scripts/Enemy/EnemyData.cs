using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData {

    [HideInInspector] public int currentHp;
    public int maxHp;
    public int attack;
    public int expDrop;

    public EnemyData(EnemyData enemyData)
    {
        maxHp = enemyData.maxHp;
        currentHp = maxHp;
        attack = enemyData.attack;
        expDrop = enemyData.expDrop;
    }

}
