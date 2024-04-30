using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI counterText;
    [SerializeField] private int counter = 0;
    public void ChangeCounter(int change) {
        counter += change;
        if (counterText != null) {
            counterText.text = $"{counter}";
        }
        
    }
    
    private void Awake() {
        if (counterText != null) {
            counterText.text = $"{counter}";
        }
    }
}
