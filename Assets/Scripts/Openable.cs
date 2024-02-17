using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering;

public class Openable : MonoBehaviour
{
    public AudioClip openSound, closeSound;
    private AudioSource audioSource;

    Animator animator;
    public bool isOpen = false;

    float slamSpeed = 4f;

    public AnimationClip openAnimation, closeAnimation;

    private bool isPlaying = false;

    [Space(10)]
    public UnityEvent onOpenFinish, onCloseFinish;

    void Start()
    {
        animator = this.transform.GetComponent<Animator>();
        audioSource = this.transform.GetComponent<AudioSource>();
        if(audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    public void ToggleOpen() {
        if (!isPlaying) {
            if (isOpen) Close();
            else Open();
        }
    }

    public void Open() {
        isOpen = true;
        animator.Play(openAnimation.name);
        isPlaying = true;
        if (openSound != null) {
            audioSource.clip = openSound;
            audioSource.Play();
        }
        StartCoroutine(AnimationEnd(openAnimation.length, true));
    }

    public void Close() {
        isOpen = false;
        animator.Play(closeAnimation.name);
        isPlaying = true;
        if (closeSound != null) {
            audioSource.clip = closeSound;
            audioSource.Play();
        }
        StartCoroutine(AnimationEnd(closeAnimation.length, false));
    }

    public void SlamClose() {
        animator.speed = slamSpeed;
        Close();
    }

    public void SlamOpen() {
        animator.speed = slamSpeed;
        Open();
    }

    IEnumerator AnimationEnd(float time, bool isClosing) {
        yield return new WaitForSeconds(time);
        if (isClosing) onCloseFinish?.Invoke();
        else onOpenFinish?.Invoke();
        animator.speed = 1f;
        isPlaying = false;
    }
}
