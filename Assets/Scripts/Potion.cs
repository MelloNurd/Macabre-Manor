using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public enum PotionColor { red, blue, yellow };

[System.Serializable]
public class Potion : MonoBehaviour
{
    public PotionShelf shelf;

    Material[] mats = new Material[2];

    MeshRenderer mr;

    Player player;

    public static int test;

    public int ColorIndex { get; set; }

    public PotionColor color;
    public int fillHeight;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mats[0] = mr.sharedMaterial;
        mats[1] = mr.sharedMaterial;
        mr.materials = mats;
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetShelfPotion() {
        if (gameObject.TryGetComponent(out InteractableData component)) {
            shelf.SetPotion(component.GetSlot(), this);
        }
        else Debug.LogError("No interactable slot component on " + this);
    }

    public void IncreaseLiquidLevel() {
        if (gameObject != player.heldObject) return;
        if(ColorIndex == (int)color) {
            if (fillHeight < 2) fillHeight++;
            //else fillHeight = 0;
        }
        else {
            color = (PotionColor)ColorIndex;
            fillHeight = 1;
        }
        UpdateModel();
        player.UpdateItemInHand();
    }

    public void UpdateModel() {
        mats[1] = shelf.materials[ColorIndex];
        mr.materials = mats;
        GetComponent<MeshFilter>().mesh = shelf.meshes[fillHeight];
    }
}
