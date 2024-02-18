using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip clip;
    // Start is called before the first frame update
    void Start()
    {
        animator.Play("WakeUp");
        StartCoroutine(GameEnd(clip.length));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GameEnd(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MainMenu");
    }
}
