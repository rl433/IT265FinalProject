using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class DieRoller : MonoBehaviour
{
    [SerializeField] private DieRollerMode mode = DieRollerMode.EQ;
    [SerializeField]
    private int sides = 6, dice = 1;

    [SerializeField] private bool useUI = true;
    [Header("UI")] [SerializeField] private GameObject canvasGO;
    [SerializeField] private TMPro.TextMeshProUGUI dieType, resultText;

    [SerializeField] private UnityEvent<int> onRoll;
    
    [ContextMenu("Test Roll")]
    public void Roll()
    {
        int result = -1;
        if (mode == DieRollerMode.EQ)
        {
            result = dice + Random.Range(0, ((sides * dice) - dice) + 1);
        }
        else
        {
            for (int i = 0; i < dice; i++)
            {
                for (int k = 0; k < sides; k++)
                {
                    result += Random.Range(1, sides + 1);
                }
            }
        }

        if (useUI && resultText != null)
        {
            resultText.text = $"{result}";
        }

        onRoll?.Invoke(result);
        Debug.Log($"Rolles {dice}d{sides} and got {result}");
    }

    private void Awake()
    {
        UpdateDiceEQ();
    }

    private void OnValidate()
    {
        UpdateDiceEQ();
        if (canvasGO != null)
        {
            canvasGO.SetActive(useUI);
        }
    }

    private void UpdateDiceEQ()
    {
        if (useUI && dieType != null)
        {
            dieType.text = $"{dice}d{sides}";
        }
    }
}

public enum DieRollerMode
{
    RUNNING_TOTAL, EQ
}