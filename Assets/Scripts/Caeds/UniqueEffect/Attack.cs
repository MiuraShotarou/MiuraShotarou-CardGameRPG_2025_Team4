using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Attack")] //ScriptableObject新規作成時の名前を決めるコード(Attack という名前は必ず変更してください)
public class AttackEffect : UniqueEffect 　　　　　　//クラス名 (AttackEffect もリネームしてください)
{
    [SerializeField]
    //カードの効果処理
    public override void Execute(Card card, Card flontCard, Battler player,Enemy enemy,Text message)
    {
        int attackValue = FlontBuff(card, flontCard);　　　　　　　  //①

        int Hit = (int)(attackValue * Random.Range(0.8f, 1.2f));     //攻撃力が一定にならないよう乱数を掛け算。
        float defense = 1f - enemy.Base.EnemyDefense / 100f;　　　　 //防御力をダメージカット倍率に変換し、その後下の行にて使える値に修正。
        int damage = (int)(Hit * defense);　　　　　　　　　　　　　 //ダメージを計算。
        enemy.Base.EnemyLife -= damage;　　　　　　　　　　　　　　　//敵のHPからダメージを引いた。
        message.text = $"{damage}ダメージ与えた";　　　　　　　　　　//というのをプレイヤーにテキストで伝えた!
        if (enemy.Base.EnemyLife < 0)　　　　　　　　　　　　　　　　//敵のHPが0以下になった場合、
        {
            enemy.Base.EnemyLife = 0;　　　　　　　　　　　　　　　　//0以下だと面倒なので値を0に戻してやる。
        }
    }

    //① 一枚前のカードの追加効果処理
    public int FlontBuff(Card card, Card flontCard)
    {
        float attackValue = (int)card.Base.CardStatus.Attack_Status; //インスペクターで設定したAttack_Statusと同じ。
        
        if (flontCard == null)　　　　　　　　　　　　　　　　　　　 ///もし、このカードの前に別のカードが選択されていなかったら。
        {　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　///↓
            return (int)attackValue;　　　　　　　　　　　　　　　　 ///攻撃力にバフ・デバフはかからずそのままの値になる。
        }
        else　　　　　　　　　　　　　　　　　　　　　　　　　　　　 ///もし、何かしらのカードが選択されていたとしたら。
        {
            string cardName = flontCard.Base.CardName;　　　　　　　 ///そのカードの名前を一時的に記憶して、↓
            FlontBuff foundBuff = card.Base.FlontBuff.Find(buff => buff.flontCard == cardName);　　　　　///インスペクターで設定
            　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　///したカードの中に含まれているかを確認する。
            if (foundBuff == null)　　　　　　　　　　　　　　　　　 ///そして、含まれていなかった場合、、
            {　　　　　　　　　　　　　　　　　　　　　　　　　　　　///バフ・デバフの条件に該当するカードはなかったことになるので、
                return (int)attackValue;　　　　　　　　　　　　　　 ///攻撃力にバフ・デバフはかからずそのままの値になる。
            }
            　　　　　　　　　　　　　　　　　　　　　　　　　　　　 ///無事、バフ・デバフ条件のカードが選択されていたとしたら、
            attackValue *= foundBuff.buff;　　　　　　　　　　　　　 ///インスペクターで設定した倍率を元の攻撃力に掛け算し、
            return (int)attackValue;　　　　　　　　　　　　　　　　 ///算出された値をこのカードの攻撃力とする。
        }
    }
}