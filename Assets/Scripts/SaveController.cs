//using System.IO;
//using Unity.Cinemachine;
//using UnityEngine;

//public class SaveController : MonoBehaviour
//{
//    private string saveLocation;
//    // Start is called before the first frame update
//    void Start()
//    {
//        //Define save location
//        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
//        //inventoryController = FindFirstObjectByType<InventoryController>();
        
//        LoadGame();
//    }

//    public void SaveGame()
//    {
//        SaveData saveData = new SaveData
//        {
//            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
//            mapBoundary = FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D.name,
//            //inventorySaveData = inventoryController.GetInventoryItem()
//        };

//        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    
//    }

//    public void LoadGame()
//    {
//        if (File.Exists(saveLocation))
//        {
//            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            
//            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            
//            FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
//            //InventoryController.SetInventoryItems(savdData.inventorySaveData);
//        }
//        else
//        {
//            SaveGame();
//        }
//    }
//}
