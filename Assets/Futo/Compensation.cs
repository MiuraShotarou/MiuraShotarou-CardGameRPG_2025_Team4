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
        int attackValue = (int)card.Base.CardStatus.Attack_Status;  

        int Hit = (int)(attackValue * compensationValue * Random.Range(0.8f, 1.2f));
        float defense = 1f - enemy.Base.EnemyDefense / 100f;
        int damage = (int)(Hit * defense);
        enemy.Base.EnemyLife -= damage;
        if (enemy.Base.EnemyLife < 0)
        {
            enemy.Base.EnemyLife = 0;
        }
        player.Life -= compensationDamage;
        if (player.Life < 0)
        {
            player.Life = 0;
        }
        message.text = $"{damage}ダメージ与えた\n代償として{compensationDamage}を受けた";
    }
}
