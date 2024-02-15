using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour {

    public bool isFull;

    public bool startFull = false;

    public GameObject water;

    Player player;

    private void Start() {
        player = FindAnyObjectByType<Player>();

        water = transform.GetChild(0).gameObject;
        if (gameObject != player.handObj) {
            if (startFull) Fill();
            else Empty();
        }
    }

    public void WaterFix() {
        water.SetActive(isFull);
    }

    public void Fill() {
        isFull = true;
        if (gameObject == player.heldObject) {
            player.ShowItemInHand();
        }
        else water.SetActive(true);
    }

    public void Empty() {
        isFull = false;
        if (gameObject == player.heldObject) {
            player.handObj?.GetComponent<Bucket>()?.water?.SetActive(false);
        }
        else water.SetActive(false);
    }

    public bool IsFull() {
        return isFull;
    }

    public bool IsEmpty() {
        return !isFull;
    }
}
