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

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddToInventory(GameObject obj) {
        if(inventory.Count < inventorySize) {
            inventory.Add(obj);
            obj.SetActive(false);
            return true;
        }
        return false;
    }

    public List<GameObject> GetInventory() { return inventory; }
}
