using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardComponent : MonoBehaviour
{
    [Header("Card UI")] [SerializeField] private TMPro.TextMeshProUGUI title;
    [SerializeField] private TMPro.TextMeshProUGUI cardType, body;
    public Button cardButton;
    private Card myCard = null;
    public void LoadCard(Card c)
    {
        myCard = c;
        if (title != null)
        {
            title.text = c.title;
        }

        if (cardType != null)
        {
            cardType.text = c.type;
        }

        if (body != null)
        {
            body.text = c.body;
        }
    }

    private void OnDestroy()
    {
        if (cardButton != null)
        {
            cardButton.onClick.RemoveAllListeners();
        }
    }
}