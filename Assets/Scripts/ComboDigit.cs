using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDigit : MonoBehaviour
{
    public int digit = 0;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Increment()
    {
        transform.Rotate(36.0f, 0f, 0f);
        digit++;
        if (digit > 9) digit = 0;
        GetComponentInParent<ComboLock>().CheckCombo();
    }
}
