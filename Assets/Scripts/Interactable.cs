using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private Player player;

    private List<Requirements> requirements = new List<Requirements>();

    [Header("Interactable Settings")][Space(5)]
    [Tooltip("Enabling this will use the \"Basic\" crosshair instead of the hand")]
    public bool displayNormalCrosshair = false;
    //[Tooltip("Whether or not this interactable will display the locked crosshair when aiming at it.")]
    //public bool disaplyLockCrosshair = false; // could use some work. would be nice if it automatically set to false if the player has the required item

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
        if (isDisabled) displayNormalCrosshair = true;
    }

    public void Interact() {
        if (requiresItem) {
            if (player.heldObject != requiredItem) {
                onInteractFail?.Invoke();
                return;
            }
            if (requiresItemOnlyOnce) requiresItem = false;
            else if (deletesItem) player.heldObject.GetComponent<Pickupable>().RemoveItem();
            //if (disaplyLockCrosshair) disaplyLockCrosshair = false;
        }
        if (isDisabled || (requirements.Count > 0 && !MeetsRequirements())) {
            onInteractFail?.Invoke();
            return;
        }

        onInteract?.Invoke();
    }

    public bool MeetsRequirements() {
        if (requirements.Any(x => !x.CheckReq())) {
            return false;
        }
        return true;
    }

    public void OnEnableInteraction() {
        onEnable?.Invoke();
    }

    public void EnableInteraction() {
        isDisabled = false;
        EnableHandCrosshair();
        OnEnableInteraction();
    }

    public void DisableInteraction() {
        isDisabled = true;
    }

    public void EnableHandCrosshair() {
        displayNormalCrosshair = false;
    }

    public void DisableHandCrosshair() {
        displayNormalCrosshair = true;
    }
}

public abstract class Requirements : MonoBehaviour {
    public virtual bool CheckReq() { return true; }
}
