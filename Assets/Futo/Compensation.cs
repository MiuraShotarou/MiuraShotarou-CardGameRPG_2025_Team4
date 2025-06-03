using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Compensation")]
public class CompensationEffect : UniqueEffect
{
    [SerializeField] float compensationValue = 1.5f;
    [SerializeField] int compensationDamage = 10;
    //カードの効果処理
    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        int Hit = (int)(compensationValue * Random.Range(0.8f, 1.2f));
        float defense = 1f - enemy.Base.EnemyDefense / 100f;
        int damage = (int)(Hit * defense);
        enemy.Base.EnemyLife -= damage;
        message.text = $"{damage}ダメージ与えた";
        if (enemy.Base.EnemyLife < 0)
        {
            enemy.Base.EnemyLife = 0;
        }
        player.Life -= compensationDamage;
        message.text = $"代償として{compensationDamage}を受けた";
        if (player.Life < 0)
        {
            player.Life = 0;
        }
    }
}
