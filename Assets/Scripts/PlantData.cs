//using NUnit.Framework.Interfaces;
//using UnityEngine;

//[CreateAssetMenu(fileName = "NewPlantData", menuName = "Farming/Plant Data")]
//public class PlantData : ScriptableObject
//{
//    public string plantName;
//    public GameObject[] growthStages; // อาร์เรย์ของ GameObject/Prefab สำหรับแต่ละระยะการเติบโต
//    public float timeToGrowEachStage; // เวลาที่ใช้ในการโตแต่ละระยะ (เช่น 10 วินาที)
//    public GameObject harvestItemPrefab; // ผลผลิตที่จะได้
//    public int minHarvestAmount;
//    public int maxHarvestAmount;
//    public Sprite seedIcon; // ไอคอนสำหรับเมล็ดใน Inventory
//}
//// สมมติว่าคุณมีคลาส ItemData สำหรับ Inventory ของคุณ
//[CreateAssetMenu(fileName = "NewSeedItem", menuName = "Inventory/Seed Item")]
//public class SeedItem : ItemData // สมมติว่า ItemData เป็นคลาสพื้นฐานสำหรับไอเท็ม
//{
//    public PlantData plantData; // อ้างอิงไปยัง PlantData ที่เมล็ดนี้จะปลูกได้
//    // public int amount; // หากเป็น Stackable Item
//}
//[System.Serializable]
//public class PlotState
//{
//    public string plotID; // ID เฉพาะของแปลงปลูก
//    public string plantDataName; // ชื่อของ PlantData ที่กำลังปลูกอยู่ (ถ้ามี)
//    public int currentGrowthStage; // ระยะการเติบโตปัจจุบัน
//    public float growthTimer; // เวลาที่เหลือในการเติบโตในระยะปัจจุบัน
//    public bool isWatered; // สถานะการรดน้ำ
//    public long lastGrowthUpdateTime; // เวลาที่อัปเดตการเติบโตล่าสุด (ใช้ในการคำนวณ Offline Growth)
//}