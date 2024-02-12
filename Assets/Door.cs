using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    Lock myLock;
    public bool open = false;
    public GameObject child;

    void Start()
    {
        child = this.transform.GetChild(0).gameObject;
        animator = this.transform.GetComponent<Animator>();
        myLock = child.GetComponent<Lock>();
    }

    void OnMouseDown()
    {
        if (myLock.locked == true) // If the door is closed...
        {
            
        }
        else                                  // If it's not closed...
        {
            if (open == false)
            {
                open = true;
                Debug.Log("open");
                animator.Play("DoorOpen");
            }
            else
            {
                open = false;
                animator.Play("DoorClose");
            }
        }
    }
}
