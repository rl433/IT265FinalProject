using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesInventory : MonoBehaviour
{
    [SerializeField] private GameObject currentObject;
    public void ClearCurrentObject() {
        currentObject = null;
    }
    public void SetCurrentObject(GameObject go) {
        if (go != null) {
            currentObject = go;
        } else {
            currentObject = null;
        }
    }

    public GameObject GetCurrentObject() {
        return currentObject;
    }
}
