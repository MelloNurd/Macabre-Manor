using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{

    public GameObject key;

    public bool locked = true;

    public bool Unlock(List<GameObject> items) {
        foreach (GameObject item in items) {
            if(item == key) {
                locked = false;
                return true;
            }
        }
        return false;
    }
}
