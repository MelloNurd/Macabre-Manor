using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Transition : MonoBehaviour
{

    [SerializeField] private GameObject roomUnload, roomLoad;

    [Space(10)]
    public UnityEvent onEnter;

    private void Start() {
        if (roomUnload == null) Debug.LogWarning("Unassigned room unloading on " + this);
        if (roomLoad == null) Debug.LogWarning("Unassigned room loading on " + this);
    }

    private void OnTriggerEnter(Collider other) {
        onEnter?.Invoke();
    }

    public void LoadRooms() {
        if(roomUnload != null) roomUnload.SetActive(false);
        if(roomLoad != null) roomLoad.SetActive(true);
    }

}
