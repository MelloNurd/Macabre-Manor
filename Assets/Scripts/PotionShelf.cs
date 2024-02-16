using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PotionShelf : MonoBehaviour
{

    public Material[] materials = new Material[3];
    public Mesh[] meshes = new Mesh[3];

    public UnityEvent onSolve;

    [System.Serializable]
    public struct PotionPlacementSlot {
        public PotionColor color;
        public int fillHeight;

    }

    public PotionPlacementSlot[] solution = new PotionPlacementSlot[3];
    private PotionPlacementSlot[] slots = new PotionPlacementSlot[3];

    public void SetPotion(int index, Potion potion) {
        slots[index].color = potion.color;
        slots[index].fillHeight = potion.fillHeight;
        if (IsSolved()) onSolve?.Invoke();
        else {
            Debug.Log("SLOTS:");
            foreach(PotionPlacementSlot slot in slots) {
                Debug.Log(slot.color.ToString() + ", " + slot.fillHeight);
            }
            Debug.Log("SOLUTION:");
            foreach (PotionPlacementSlot slot in solution) {
                Debug.Log(slot.color.ToString() + ", " + slot.fillHeight);
            }
        }
    }

    public bool IsSolved() {
        for(int i = 0; i < slots.Length; i++) {
            if (solution[i].color != slots[i].color || solution[i].fillHeight != slots[i].fillHeight) return false;
        }
        return true;
    }
}
