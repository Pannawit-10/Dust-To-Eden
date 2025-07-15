using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public string mapBoundary; //The boundary name for the map
    public List<InventorySaveData> inventorySaveData;
    public List<ChestSaveData> chestSaveData;
}

[System.Serializable]
public class ChestSaveData
{
    public string chestID;
    public bool isOpened;
}

//// ใน SaveData.cs (หรือรวมกับ SaveController)
//[System.Serializable]
//public class SaveData
//{
//    public Vector3 playerPosition;
//    public string mapBoundary;
//    public List<InventorySaveData> inventorySaveData; // สมมติว่ามี
//    public List<ChestSaveData> chestSaveData; // สมมติว่ามี
//    public List<PlotState> plotSaveData; // เพิ่มเข้ามาสำหรับ FarmPlot
//}