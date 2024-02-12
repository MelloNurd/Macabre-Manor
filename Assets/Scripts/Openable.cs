using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Openable : MonoBehaviour
{
    Animator animator;
    Lock myLock;
    public bool open = false;
    public GameObject child;

    public AnimationClip openAnimation, closeAnimation;

    private bool isLocked = true;
    private bool isMoving = false;
    private bool canClose = true;

    void Start()
    {
        child = this.transform.GetChild(0).gameObject;
        animator = this.transform.GetComponent<Animator>();
        myLock = child.GetComponent<Lock>();
    }

    public void Unlock() {
        isLocked = false;
    }

    public void OpenClose() {
        if (!isMoving && !isLocked) {
            if (open == false) {
                open = true;
                Debug.Log("open");
                animator.Play(openAnimation.name);
                isMoving = true;
                StartCoroutine(AnimationEnd(openAnimation.length));
            }
            else {
                open = false;
                animator.Play(closeAnimation.name);
                isMoving = true;
                StartCoroutine(AnimationEnd(closeAnimation.length));
            }
        }
    }

    IEnumerator AnimationEnd(float time) {
        yield return new WaitForSeconds(time);
        isMoving = false;
    }
}
