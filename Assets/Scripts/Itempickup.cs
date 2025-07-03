using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bool picked = Inventory.Instance.AddItem(itemName);
            if (picked)
            {
                Destroy(gameObject);
            }
        }
    }
}
