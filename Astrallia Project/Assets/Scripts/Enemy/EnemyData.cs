using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData {

    public int hp;
    public int attack;
    public int expDrop;

    public EnemyData(EnemyData enemyData)
    {
        hp = enemyData.hp;
        attack = enemyData.attack;
        expDrop = enemyData.expDrop;
    }

}
