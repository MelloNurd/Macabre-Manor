using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKey : MonoBehaviour
{
    [SerializeField] GameObject fire;
    [SerializeField] GameObject hint;
    Openable script;
    // Start is called before the first frame update
    void Start()
    {
        script = fire.GetComponent<Openable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (script.open == false)
        {
  
        }
        else
        {
            gameObject.tag = "Holdable";
        }
    }
}
