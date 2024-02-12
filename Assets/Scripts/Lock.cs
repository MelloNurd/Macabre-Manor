using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{

    public GameObject key;

    public bool locked = true;

    public void Start() {
        if (gameObject.tag != "Lock") gameObject.tag = "Lock";
    }

    public bool Unlock(List<GameObject> items, bool removeItemFromInv = true) {
        foreach (GameObject item in items) {
            if(item == key) {
                locked = false;
                if(removeItemFromInv) items.Remove(item);
                return true;
            }
        }
        return false;
    }
}
