using UnityEngine;

public class PlayerItemColector : MonoBehaviour
{
    private InventoryController inventoryController;
    // Start is called before the first frame update
    void Start()
    {
        inventoryController = FindFirstObjectByType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            ItemDragHandler item = collision.GetComponent<ItemDragHandler>();
            if (item != null)
            {
                //Add item inventory
            }
        }
    }
}
