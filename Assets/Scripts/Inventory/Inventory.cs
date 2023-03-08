using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public GameObject inventoryParentObject;
    GameObject selectedInventory;

    public ContainerStuffs containerStuffs;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && containerStuffs.gameManager.isCustomizing == false) inventoryParentObject.SetActive(!inventoryParentObject.gameObject.activeSelf);

        // Click and Drag Inventory
        if(Input.GetMouseButtonDown(0) && containerStuffs.gameManager.isCustomizing == false) {
            
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);

            if(collider != null) {
                Debug.Log("you clicked on the inventory");
            }
        }
    }
}