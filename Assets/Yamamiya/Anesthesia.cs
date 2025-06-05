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
    }

    //麻酔状態にするかどうかの判定
    public void ApplyAnesthesiaEffect(Text message)
    {
        if (!IsUsedAnesthesia) 
        {
            return;
        }
        int random = Random.Range(0, 30);

        if (Index == anethesiaProbabilities.Length -1) //配列最後尾になったときに麻酔状態を解除する
        {
            ResetAnesthesia(message);
            return;
        }
        else if(anethesiaProbabilities[Index] >= random) //行動不能になるかどうかの判定
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
}