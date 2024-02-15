using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour {

    private bool isFull;

    public bool startFull = false;

    public GameObject water;

    private void Start() {
        water = transform.GetChild(0).gameObject;
        if (startFull) Fill();
        else Empty();
    }

    public void Fill() {
        isFull = true;
        water.SetActive(true);
    }

    public void Empty() {
        isFull = false;
        water.SetActive(false);
    }

    public bool IsFull() {
        return isFull;
    }

    public bool IsEmpty() {
        return !isFull;
    }
}
