using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] AnimationClip clip;
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    
    public void LoadSceneOnDelay(string sceneName)
    {
        StartCoroutine(DelayedTransition(clip.length, sceneName));
    }

    IEnumerator DelayedTransition(float time, string sceneName)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
