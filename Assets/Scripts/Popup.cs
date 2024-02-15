using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Popup : MonoBehaviour
{

    Player player;

    public GameObject popupImage;

    public UnityEvent onShow, onHide;

    public bool isShown = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        popupImage.SetActive(false);
        if (popupImage == null) Debug.LogError("No popup image set for: " + this);
    }

    // Update is called once per frame
    void Update()
    {
        //if (isShown && player.Interact()) Hide();
    }

    public void ShowHide() {
        if (isShown) Hide();
        else Show();
    }

    public void Show()
    {
        popupImage.SetActive(true);
        isShown = true;
        onShow?.Invoke();
        player.DisableMove();
    }

    public void Hide()
    {
        popupImage.SetActive(false);
        isShown = false;
        onHide?.Invoke();
        player.EnableMove();
    }
}
