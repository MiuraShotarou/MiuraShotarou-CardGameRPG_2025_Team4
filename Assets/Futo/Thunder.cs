using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Thunder")]
public class ThunderEffect : UniqueEffect
{
    [SerializeField, Range(5, 50)] int thunderDamage;
    public bool _isThunder = false;
    [SerializeField, Range(1, 5)] int absoluteThunder = 4;
    [SerializeField] int thunderCount = 0;
    [SerializeField] int isThunderpercent = 5;

    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        enemy.Base.EnemyLife -= thunderDamage;
        if(enemy.Base.EnemyLife < absoluteThunder)
        {
            enemy.Base.EnemyLife = 0;
        }
        message.text = $"{thunderDamage}ダメージ与えた";
        thunderCount++;
        if (thunderCount < 3)
        {
            int R = Random.Range(0, 99);
            if ( R < isThunderpercent )
            {
                _isThunder = true ;
            }
        }
        if( thunderCount >= absoluteThunder)
        {
            _isThunder = true;
            thunderCount = 0;
        }
    }
}
