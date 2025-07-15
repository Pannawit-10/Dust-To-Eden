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

//// � SaveData.cs (��������Ѻ SaveController)
//[System.Serializable]
//public class SaveData
//{
//    public Vector3 playerPosition;
//    public string mapBoundary;
//    public List<InventorySaveData> inventorySaveData; // ����������
//    public List<ChestSaveData> chestSaveData; // ����������
//    public List<PlotState> plotSaveData; // �������������Ѻ FarmPlot
//}