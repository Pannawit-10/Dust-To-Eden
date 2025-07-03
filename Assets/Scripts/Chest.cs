using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }
    public string ChestID { get; private set; }
    public GameObject itemPrefeb; //Item that chest drops
    public Sprite openedSprite;

    // Start is called before the first frame update
    void Start()
    { 
        ChestID = //UniqueID
    }

    public bool CanInteract()
    { 
        throw new System.NotImplementedException();
    }
    public void Interact()
    { 
        throw new System.NotImplementedException();
    }



}
