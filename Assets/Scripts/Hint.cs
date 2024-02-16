using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] GameObject hint;

    public void ShowHint() {
        hint.SetActive(true);
        StartCoroutine(HintWait(2f));
    }

    IEnumerator HintWait(float time) {
        yield return new WaitForSeconds(time);
        hint.SetActive(false);
        Debug.Log(hint.name + " deactivated");
    }
}
