using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviour {

    bool lightsOn = true;

    public UnityEvent onDisableLights;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void DisableAllLightsInRoom() {
        if (!lightsOn) return;
        lightsOn = false;
        StartCoroutine(DisableDelay());
    }

     public IEnumerator DisableDelay() {
        yield return new WaitForSeconds(20f);
        onDisableLights?.Invoke();
        foreach (Light light in GetComponentsInChildren<Light>()) {
            if(light.gameObject.tag != "PermanentLight") light.enabled = false;
        }
    }
}
