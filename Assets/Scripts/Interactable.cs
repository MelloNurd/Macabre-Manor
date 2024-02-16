using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Events;
public enum crosshairOnHover {
    basic,
    hand,
    locked,
}

public class Interactable : MonoBehaviour
{
    private Player player;

    private List<Requirements> requirements = new List<Requirements>();

    [Header("Interactable Settings")][Space(5)]
    [Tooltip("Enabling this will use the \"Basic\" crosshair instead of the hand")]
    public crosshairOnHover crosshair = crosshairOnHover.hand;

    [Space(10)][Header("Lock Settings")][Space(5)]
    [Tooltip("Whether or not this interactable is disabled, and is to be unlocked from another interaction.")]
    public bool isDisabled = false;

    [Space(10)][Header("Item Settings")][Space(5)]
    [Tooltip("Whether or not this interactable requires an item to interact with it.")]
    public bool requiresItem = false;
    [Tooltip("Whether or not this interactable only requires the required item for its first interaction.")]
    public bool requiresItemOnlyOnce = false;
    [Tooltip("Whether or not this interactable deletes the required item used to interact with it.")]
    public bool deletesItem = false;
    [Tooltip("The item required to interact with this interactable.")]
    public GameObject requiredItem = null;

    [Space(10)][Header("Events")][Space(5)]
    public UnityEvent onInteractFail;
    public UnityEvent onInteract;
    public UnityEvent onEnable;

    private void Awake() {
        requirements = GetComponents<Requirements>().ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void Interact() {
        if (isDisabled) {
            onInteractFail?.Invoke();
            Debug.Log("Failed interacting with " + this);
            return;
        }

        if (requirements.Count > 0 && !MeetsRequirements()) {
            onInteractFail?.Invoke();
            Debug.Log("Failed interacting with " + this);
            return;
        }

        if (requiresItem) {
            if (player.heldObject != requiredItem) {
                onInteractFail?.Invoke();
                Debug.Log("Failed interacting with " + this);
                return;
            }
            if (requiresItemOnlyOnce) requiresItem = false;
            if (deletesItem) player.heldObject.GetComponent<Pickupable>().RemoveItem();
        }

        Debug.Log("Successfully interacted with " + this);
        onInteract?.Invoke();
    }

    public bool MeetsRequirements() {
        if (requirements.Any(x => !x.CheckReq())) {
            return false;
        }
        return true;
    }

    public void OnEnableInteraction() {
        Debug.Log("Enabled Interaction on " + this);
        onEnable?.Invoke();
    }

    public void EnableInteraction() {
        isDisabled = false;
        OnEnableInteraction();
    }

    public void DisableInteraction() {
        isDisabled = true;
    }

    public void EnableBasicCrosshair() {
        crosshair = crosshairOnHover.basic;
    }

    public void EnableHandCrosshair() {
        crosshair = crosshairOnHover.hand;
    }

    public void EnableLockCrosshair() {
        crosshair = crosshairOnHover.locked;
    }
}

public abstract class Requirements : MonoBehaviour {
    public virtual bool CheckReq() { return true; }
}
