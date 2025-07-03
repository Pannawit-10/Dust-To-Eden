using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<string> items = new List<string>();
    public int capacity = 10;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool AddItem(string itemName)
    {
        if (items.Count >= capacity)
        {
            Debug.Log("Inventory full!");
            return false;
        }

        items.Add(itemName);
        Debug.Log("Picked up: " + itemName);
        return true;
    }

    public void PrintInventory()
    {
        Debug.Log("Inventory:");
        foreach (string item in items)
        {
            Debug.Log("- " + item);
        }
    }
}
