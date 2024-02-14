using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Popup : MonoBehaviour
{

    Player player;

    public UnityEvent onShowFail, onShow, onHide;

    public bool isShown = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShown && player.Interact()) Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        player.DisableMove();
        isShown = true;
        onShow?.Invoke();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        player.EnableMove();
        onHide?.Invoke();
    }
}
