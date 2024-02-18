using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip clip;
    public void LoadGame()
    {
        animator.Play("MainMenuEyesClose");
        StartCoroutine(GameStart(clip.length));
    }

    public void ExitGame()
    {
        animator.Play("MainMenuEyesClose");
        StartCoroutine(Close(clip.length));
    }

    IEnumerator GameStart(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator Close(float time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
    }
}
