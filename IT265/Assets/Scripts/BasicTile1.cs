using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTile1 : MonoBehaviour
{
    [SerializeField] private GameObject point;
    public Vector3 GetPointPosition() {
        return point.transform.position;
    }
}
