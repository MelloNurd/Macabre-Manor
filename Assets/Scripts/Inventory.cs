using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> inventory = new List<GameObject>();

    int inventorySize = 3;

    /// <summary>
    /// Attempts to add given the GameObject to the inventory list.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>Returns true or false on success/failure. Fails if the inventory is full.</returns>
    public bool AddToInventory(GameObject obj) {
        if(inventory.Count < inventorySize) {
            inventory.Add(obj);
            obj.SetActive(false);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets the List of GameObjects which acts as the inventory
    /// </summary>
    /// <returns>List of type GameObject</returns>
    public List<GameObject> GetInventory() { return inventory; }
}
