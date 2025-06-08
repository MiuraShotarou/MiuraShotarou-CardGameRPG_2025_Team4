using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Reflector")]
public class ReflectorEffect : UniqueEffect
{
    [SerializeField] float reflectionValue = 3f; //反射ダメージの倍率
    public bool IsReflection = false; //反射状態かどうかのフラグ
    //カードの効果処理
    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        message.text = "反射状態になった";
        IsReflection = true;
    }

    public void Reflection(Battler player, Enemy enemy, int hit, Text message)
    {
        int halfDamage = (int)(hit * 0.5f);
        player.Life -= halfDamage;
        //message.text = $"{halfDamage}ダメージをうけた";
        int damage = (int)(halfDamage * reflectionValue);
        enemy.Base.EnemyLife -= damage;
        enemy.EnemyLifeContlloer.lifeReflection(enemy);
        message.text = $"{halfDamage}ダメージ受けたが,\n" + $"{damage}ダメージ与えた";
        if (enemy.Base.EnemyLife < 0) 
        {
            enemy.Base.EnemyLife = 0;
        }
        IsReflection = false;
    }

    public bool GetIsReflection()
    {
        return IsReflection;
    }
}