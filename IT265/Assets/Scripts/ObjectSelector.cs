using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject selected;
    [SerializeField] private bool fixOffset = false;
    [SerializeField] private LayerMask layersToInteractWith;
    [SerializeField] private LayerMask placementLayers;
    [SerializeField]
    private Vector3 specificOffset;
    [SerializeField] private PiecesInventory piecesInventoryRef;
    [SerializeField] public bool rightClickToDelete = false;
    public bool allowMove = false;
    private void Awake()
    {
        var c = GetComponent<Camera>();
        if (c != null && cam == null)
        {
            cam = c;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rightClickToDelete && Input.GetMouseButtonDown(1))
        {
            Vector3 mouse = Vector3.zero;
            mouse.x = Input.mousePosition.x;
            mouse.y = Input.mousePosition.y;
            Ray ray = cam.ScreenPointToRay(mouse);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                int layer = hitInfo.collider.gameObject.layer;
                if ((layersToInteractWith & (1 << layer)) != 0)
                {
                    //layer exists in mask
                    Destroy(hitInfo.collider.gameObject);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!allowMove && piecesInventoryRef != null)
            {
                selected = piecesInventoryRef.GetCurrentObject();
            }
            Debug.Log("User clicked the left mouse button");
            Vector3 mouse = Vector3.zero;
            mouse.x = Input.mousePosition.x;
            mouse.y = Input.mousePosition.y;
            Ray ray = cam.ScreenPointToRay(mouse);
            //Vector3 origin = this.transform.position;
            //Vector3 direction = this.transform.forward;
            RaycastHit hitInfo;
            Debug.DrawLine(ray.origin, ray.direction * 100, Color.blue, 3);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
               
                Debug.DrawLine(ray.origin, hitInfo.point, Color.green, 3);
                Debug.Log($"Hit Name: {hitInfo.collider.gameObject.name} " +
                          $"Tag: {hitInfo.collider.gameObject.tag} " +
                          $"Layer: {LayerMask.LayerToName(hitInfo.collider.gameObject.layer)}");
                if (selected != null)
                {
                    Debug.Log("Placed item");
                    int layer = hitInfo.collider.gameObject.layer;
                    if ((placementLayers & (1 << layer)) != 0)
                    {
                        //layer exists in mask
                       
                    }
                    else
                    {
                        //layer doesn't exist in mask
                        return;// don't place items if the object is not in the placement layers mask
                    }
                    if (fixOffset)
                    {
                        Vector3 pos = hitInfo.point;
                        var offset = hitInfo.transform.localScale / 2;
                        var tile = hitInfo.collider.gameObject.GetComponent<BasicTile1>();
                        if (tile != null)
                        {
                            //this item is a tile
                            pos = tile.GetPointPosition();
                        }

                        if (!allowMove && piecesInventoryRef != null)
                        {
                            Instantiate(selected, pos + specificOffset, Quaternion.identity);
                        }
                        else if(selected != null)
                        {
                            selected.transform.position = pos + specificOffset; //pos + offset;
                        }

                    }
                    else
                    {
                        if (!allowMove && piecesInventoryRef != null)
                        {
                            Instantiate(selected, hitInfo.point, Quaternion.identity);
                        }
                        else if(selected != null)
                        {
                            selected.transform.position = hitInfo.point;
                        }
                    }
                   
                }
                else
                {
                    Debug.Log("Selected new item");
                    int layer = hitInfo.collider.gameObject.layer;
                    if ((layersToInteractWith & (1 << layer)) != 0)
                    {
                        //layer exists in mask
                        selected = hitInfo.collider.gameObject;
                        Selectable s = selected.GetComponent<Selectable>();
                        if (s != null)
                        {
                            s.SetSelected(true);
                        }
                    }
                    else
                    {
                        //layer doesn't exist in mask
                    }
                  
                }
            }
            else
            {
                if (selected != null)
                {
                    Selectable s = selected.GetComponent<Selectable>();
                    if (s != null)
                    {
                        s.SetSelected(false);
                    }
                }
                selected = null;
            }
        }
    }
}