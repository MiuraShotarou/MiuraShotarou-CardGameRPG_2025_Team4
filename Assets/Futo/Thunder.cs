using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Thunder")]
public class ThunderEffect : UniqueEffect
{
    [SerializeField, Range(5, 50)] int thunderDamage;
    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        enemy.Base.EnemyLife -= thunderDamage;
        if(enemy.Base.EnemyLife < 0)
        {
            enemy.Base.EnemyLife = 0;
        }
        message.text = $"{thunderDamage}ダメージ与えた";
    }
}
