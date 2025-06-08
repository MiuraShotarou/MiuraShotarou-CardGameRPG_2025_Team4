using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Poison")] //ScriptableObject新規作成時の名前を決めるコード(Attack という名前は必ず変更してください)
public class PoisonEffect : UniqueEffect 　　　　　　//クラス名 (AttackEffect もリネームしてください)
{
    [SerializeField] int isPoisonTurn = 5;        //default 5
    [SerializeField] int poisonAttackStatus = 10; //default 10
    [SerializeField] int poisonMultiplier = 5;    //default 5
    [SerializeField] int poisonAttackLimit = 30;  //default 30
    bool isPoison = false;
    int _isPoisonTurn;
    int _poisonDamage;

    int IsPoisonTurn
    {
        get { return _isPoisonTurn; }
        set {_isPoisonTurn = value; if (_isPoisonTurn <= 0){ isPoison = false; } }
    }
    int PoisonDamage
    {
        get { return _poisonDamage; }
        set { _poisonDamage = value; if (_poisonDamage >= poisonAttackLimit) { _poisonDamage = poisonAttackLimit; } }
    }

    //カードの効果処理
    public override void Execute(Card card, Card flontCard, Battler player,Enemy enemy,Text message)
    {
        IsPoisonTurn = isPoisonTurn;
        isPoison = true;
        message.text = "毒を付与した。";
    }

    public bool GetIsPoison()
    {
        return isPoison;
    }

    public int ExcutePoison()
    {
        PoisonDamage = poisonAttackStatus + (poisonMultiplier * (isPoisonTurn - IsPoisonTurn)); //ターンが経過するごとに追加ダメージ倍増 → 書き直し。
        IsPoisonTurn--;
        return PoisonDamage;
    }


    ////① 一枚前のカードの追加効果処理
    //public int FlontBuff(Card card, Card flontCard)
    //{
    //    float attackValue = (int)card.Base.CardStatus.Attack_Status; //インスペクターで設定したAttack_Statusと同じ。

    //    if (flontCard == null)　　　　　　　　　　　　　　　　　　　 ///もし、このカードの前に別のカードが選択されていなかったら。
    //    {　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　///↓
    //        return (int)attackValue;　　　　　　　　　　　　　　　　 ///攻撃力にバフ・デバフはかからずそのままの値になる。
    //    }
    //    else　　　　　　　　　　　　　　　　　　　　　　　　　　　　 ///もし、何かしらのカードが選択されていたとしたら。
    //    {
    //        string cardName = flontCard.Base.CardName;　　　　　　　 ///そのカードの名前を一時的に記憶して、↓
    //        FlontBuff foundBuff = card.Base.FlontBuff.Find(buff => buff.flontCard == cardName);　　　　　///インスペクターで設定
    //        　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　///したカードの中に含まれているかを確認する。
    //        if (foundBuff == null)　　　　　　　　　　　　　　　　　 ///そして、含まれていなかった場合、、
    //        {　　　　　　　　　　　　　　　　　　　　　　　　　　　　///バフ・デバフの条件に該当するカードはなかったことになるので、
    //            return (int)attackValue;　　　　　　　　　　　　　　 ///攻撃力にバフ・デバフはかからずそのままの値になる。
    //        }
    //        　　　　　　　　　　　　　　　　　　　　　　　　　　　　 ///無事、バフ・デバフ条件のカードが選択されていたとしたら、
    //        attackValue *= foundBuff.buff;　　　　　　　　　　　　　 ///インスペクターで設定した倍率を元の攻撃力に掛け算し、
    //        return (int)attackValue;　　　　　　　　　　　　　　　　 ///算出された値をこのカードの攻撃力とする。
    //    }
    //}
}