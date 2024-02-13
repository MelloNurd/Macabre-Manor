using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lock : MonoBehaviour
{
    public List<GameObject> keys;

    public UnityEvent onUnlock;

    public void Start() {
        // If the GameObject of this script does not have the "Lock" tag, set the tag to "Lock"
        // This is needed for how interactions are handled.
        if (gameObject.tag != "Lock") gameObject.tag = "Lock";
    }

    /// <summary>
    /// Attempts to unlock this lock. Searches through a given List of GameObjects for the lock's key.
    /// </summary>
    /// <param name="items">The list to search through for the key.</param>
    /// <param name="removeItemFromInv">Optional, defaults to true; If false, does not remove the key from the inventory.</param>
    /// <returns>True or false, on success or failure (whether or not the List has the key).</returns>
    public bool Unlock(List<GameObject> inventory, bool removeItemFromInv = true) {
        foreach (GameObject item in inventory) { // Goes through List of GameObjects (inventory)
            foreach(GameObject key in keys) {
                if (item == key) { // If the current item is this Lock's key
                    if (removeItemFromInv) inventory.Remove(item); // Removes the key from the list assuming the parameter is true (true by default)
                    keys.Remove(key); // Removes the key from the lock's list of keys
                    if (IsFullyUnlocked()) { // Checks if there are any keys remaining needed to be used
                        onUnlock.Invoke(); // If not, invoke the onUnlock function
                        return true;
                    }
                    return false; // If not fully unlocked, break by returning false;
                }
            }
        }
        return false;
    }

    private bool IsFullyUnlocked() {
        return keys.Count <= 0;
    }
}
