﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class DeckCustomize : MonoBehaviour
{
    [SerializeField] GameObject deckContents;
    [SerializeField] GameObject cardContents;
    [SerializeField] GameObject exitButton;

    public Deck deck;
    
    public void SerCardToCustom(Card card)
    {
        card.OnClickCard = SelectedDeckCard;
    }

    //カードクリック時のリアクション
    public void SelectedDeckCard(Card card)
    {
        if (card.transform.parent == deckContents.transform)
        {
            int destD = deck.DeckAll.FindIndex(number => number == card.Base.ID);
            int destL = deck.LookDeck.FindIndex(number => number == card);
            if (destD != -1
                &&
                destL != -1)
            {
                deck.DeckAll.RemoveAt(destD);
                Destroy(deck.LookDeck[destL].gameObject);
                deck.LookDeck.RemoveAt(destL);

                deck.deckArignment();
                card.PosReset();
            }
            else
            {
                Debug.Log("Indexが不適切");
            }
        }
        else if (deck.DeckAll.Count < 15 && card.transform.parent == cardContents.transform)
        {
            deck.DeckAll.Add(card.Base.ID);
            Card newCard = deck.Generator.Spawn(card.Base.ID);
            SerCardToCustom(newCard);
            newCard.transform.SetParent(deckContents.transform);
            deck.LookDeck.Add(newCard);

            deck.deckArignment();
        }

        CustomizeCompletion();
    }

    //デッキが15枚以下の時戻るボタンを消す
    void CustomizeCompletion()
    {
        if (deck.DeckAll.Count == 15)
        {
            exitButton.SetActive(true);
        }
        else
        {
            exitButton.SetActive(false);
        }
    }
}

public class MemorizedDeckClass
{
    static int[] _memorizedDeck;
    public static int[] MemorizedDeck { get { return _memorizedDeck; } set { _memorizedDeck = value; } }
}