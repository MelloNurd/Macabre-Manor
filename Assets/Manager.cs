using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] AnimationClip clip;

    public static Color brightLighting = new Color(0.29f, 0.15f, 0.03f);
    public static Color darkLighting = Color.black;

    // Start is called before the first frame update
    void Awake() {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name != "FinalRoom") RenderSettings.ambientLight = brightLighting;
        else RenderSettings.ambientLight = darkLighting;
    }

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
