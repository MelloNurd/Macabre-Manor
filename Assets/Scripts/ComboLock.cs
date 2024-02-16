using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class ComboLock : MonoBehaviour
{
    public int[] combination = new int[3];
    ComboDigit[] digits;
    public UnityEvent onSolve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        digits = GetComponentsInChildren<ComboDigit>();
    }

    public void CheckCombo()
    {
        for (int i = 0; i < digits.Length; i++)
        {
            if (digits[i].digit != combination[i]) return;
        }
        onSolve?.Invoke();
    }
}
