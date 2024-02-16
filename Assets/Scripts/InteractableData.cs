using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableData : MonoBehaviour
{
    public int slot;
    public Material material;

    public int GetSlot() { return slot; }
    public Material GetModifier() { return material ?? null; }
}
