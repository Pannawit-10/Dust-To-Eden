using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }
    }
}

//    public bool AddItem(GameObject itemPrefab)
//    {
//        //look for empty slot
//        foreach (Transform slotTranform in inventoryPanel.transform)
//        {
//            Slot slot = slotTranform.GetComponent<Slot>();
//            if (slot != null && slot.currentItem == null)
//            {
//                GameObject newItem = Instantiate(itemPrefab, slotTranform);
//                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
//                slot.currentItem = newItem;
//                return true;
//            }
//        }

//        Debug.Log("Inventory is full");
//}
