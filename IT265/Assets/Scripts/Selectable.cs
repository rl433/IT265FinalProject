using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] private GameObject selectionIndicator;
    public void SetSelected(bool isSelected) {
        if (selectionIndicator != null) {
            selectionIndicator.SetActive(isSelected);
        }
    }
}
