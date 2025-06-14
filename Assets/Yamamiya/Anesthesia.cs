using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Anesthesia")]
public class AnesthesiaEffect : UniqueEffect
{
    public static int Index = 0; //麻酔ポーションを使ってからの判定
    public bool IsAnesthesia; // 麻酔状態になっているかどうかの変数
    private bool IsUsedAnesthesia;
    [SerializeField] float[] anethesiaProbabilities = new float[3]; //麻酔になる確率を入れておくための配列
    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        ResetAnesthesia(message);
        message.text = "麻酔ポーションを使った";
        IsUsedAnesthesia = true;
        Debug.Log($"Index == {Index}, IsAnesthesia == {IsAnesthesia}, IsUsedAnesthesia == {IsUsedAnesthesia}, {anethesiaProbabilities[0]},{anethesiaProbabilities[1]},{anethesiaProbabilities[2]},");
    }

    //麻酔状態にするかどうかの判定
    public void ApplyAnesthesiaEffect(Text message) //毎回ジャッジする、は仕様と違う説。
    {
        Debug.LogError($"毎回ジャッジするのではなく、一度「麻酔状態」に入ったら最大ターン数まで麻痺している仕様のはず。");
        if (!IsUsedAnesthesia)
        {
            return;                                    //Indexが上がらない。 66 スルー　33でジャッジ
        }
        int random = Random.Range(0, 100);
        Debug.LogError("0 ～ 99の値が取得されてしまう。 → 1 ～ 99 + 0 の整数で100の確率を表現しているので、[SerializeField] にて66が代入されると実質67として扱われてしまう。→ 確率の不一致");

        if (Index == anethesiaProbabilities.Length -1) //配列最後尾になったときに麻酔状態を解除する [追加] Indexが2のとき、ResetAnesthesia()が起動
        {
            ResetAnesthesia(message);
            return;
        }
        else if(anethesiaProbabilities[Index] >= random) //行動不能になるかどうかの判定 [Index2 に該当する数字は-1より小さくなければならない。] 最大値は99
        {
            IsAnesthesia = true;
            message.text = "相手が麻酔状態になった";
        }
        else //麻酔状態を解除する
        {
            ResetAnesthesia(message);
        }
        Debug.Log($"今回の麻酔になる確率{anethesiaProbabilities[Index]}に対して " + $"{random}" + IsAnesthesia);
        Index++;
    }

    //麻酔状態のリセット
    public void ResetAnesthesia(Text message)
    {
        Debug.Log("麻酔状態リセット");
        IsAnesthesia = false;
        IsUsedAnesthesia = false;
        Index = 0;
        message.text = "麻酔ポーションの効果が切れた";
    }

    public bool GetIsAnesthesia()
    {
        return IsAnesthesia;
    }

    public bool GetUsedAnesthesia()
    {
        return IsUsedAnesthesia;
    }
}