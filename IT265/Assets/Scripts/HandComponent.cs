using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandComponent : MonoBehaviour
{
    [SerializeField] private int ownerId;

    [SerializeField] private GameObject container;

    [SerializeField] private GameObject cardPrefab;

    private List<Card> cards = new List<Card>();
    [SerializeField] private TMPro.TextMeshProUGUI playerLabel;
    public void ReceiveCard(Card c)
    {
        cards.Add(c);

        InefficientRemove();
        InefficientShow();
    }

    private void OnValidate()
    {
        if (playerLabel != null)
        {
            playerLabel.text = $"Player {ownerId}";
        }
    }

    private void InefficientRemove()
    {
        for (int i = 0; i < container.transform.childCount; i++)
        {
            Destroy(container.transform.GetChild(i).gameObject);
        }
    }

    private void InefficientShow()
    {
        foreach (Card card in cards)
        {
            var cardGo = Instantiate(cardPrefab, container.transform);
            if (cardGo != null)
            {
                CardComponent cc = cardGo.GetComponent<CardComponent>();
                if (cc != null)
                {
                    cc.LoadCard(card);
                    cc.cardButton.onClick.AddListener(()=>DiscardCard(card));
                }
            }
        }
    }

    public void DiscardCard(Card c)
    {
        if (cards.Remove(c))
        {
            InefficientRemove();
            InefficientShow();
        }
    }
}