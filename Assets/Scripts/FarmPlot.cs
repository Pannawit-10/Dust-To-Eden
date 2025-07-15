//using UnityEngine;
//using System; // สำหรับ DateTime/TimeSpan
//// using MyGame.Inventory; // ถ้า InventoryController อยู่ใน namespace อื่น

//public class FarmPlot : MonoBehaviour
//{
//    public string plotID; // ต้องกำหนด ID ใน Inspector หรือ Generate อัตโนมัติ
//    public PlantData currentPlantData;
//    public int currentGrowthStage = -1; // -1 = empty, 0 = seed, 1 = sprout, etc.
//    public float growthTimer; // เวลาที่ใช้ไปใน stage ปัจจุบัน
//    public bool isWatered = false;
//    private GameObject currentPlantInstance; // อ้างอิงถึง prefab ต้นไม้ที่ instantiate มา

//    // สำหรับ Offline Growth
//    private long _lastGrowthUpdateTime; // ใช้ System.DateTime.Ticks

//    void Awake()
//    {
//        if (string.IsNullOrEmpty(plotID))
//        {
//            plotID = GlobalHelper.GenerateUniqueId(gameObject); // ใช้ GlobalHelper ของคุณ
//        }
//    }

//    void Start()
//    {
//        // ถ้ามีการโหลดเกม ต้องเรียก LoadState()
//        // ถ้าเป็นครั้งแรก ให้แสดงสถานะว่างเปล่า
//        UpdateVisuals();
//    }

//    void Update()
//    {
//        if (currentPlantData != null && currentGrowthStage < currentPlantData.growthStages.Length - 1)
//        {
//            growthTimer += Time.deltaTime;
//            if (growthTimer >= currentPlantData.timeToGrowEachStage)
//            {
//                growthTimer = 0;
//                currentGrowthStage++;
//                UpdateVisuals();
//                // Debug.Log($"Plot {plotID}: Plant {currentPlantData.plantName} grew to stage {currentGrowthStage}");
//            }
//        }
//    }

//    public void Plant(PlantData plantData)
//    {
//        if (currentPlantData == null)
//        {
//            currentPlantData = plantData;
//            currentGrowthStage = 0; // เริ่มจากระยะเมล็ด
//            growthTimer = 0;
//            isWatered = false; // รีเซ็ตการรดน้ำเมื่อปลูกใหม่
//            _lastGrowthUpdateTime = DateTime.Now.Ticks; // บันทึกเวลาปลูก
//            UpdateVisuals();
//            // Debug.Log($"Plot {plotID}: Planted {plantData.plantName}");
//            SaveLoadManager.Instance.SaveGame(); // เรียก Save เมื่อมีการเปลี่ยนแปลง
//        }
//    }

//    public void Water()
//    {
//        if (currentPlantData != null && !isWatered)
//        {
//            isWatered = true;
//            // อาจจะมีผลต่อการเติบโต เช่น ลด growthTimer หรือเพิ่ม growthRate
//            // Debug.Log($"Plot {plotID}: Watered plant.");
//            SaveLoadManager.Instance.SaveGame(); // เรียก Save เมื่อมีการเปลี่ยนแปลง
//        }
//    }

//    public void Harvest()
//    {
//        if (currentPlantData != null && currentGrowthStage == currentPlantData.growthStages.Length - 1)
//        {
//            // เก็บเกี่ยวผลผลิต
//            // int harvestAmount = Random.Range(currentPlantData.minHarvestAmount, currentPlantData.maxHarvestAmount + 1);
//            // InventoryController.AddItem(currentPlantData.harvestItemPrefab, harvestAmount); // ต้องมีระบบ Inventory

//            currentPlantData = null;
//            currentGrowthStage = -1;
//            growthTimer = 0;
//            isWatered = false;
//            _lastGrowthUpdateTime = 0;
//            UpdateVisuals();
//            // Debug.Log($"Plot {plotID}: Harvested plant.");
//            SaveLoadManager.Instance.SaveGame(); // เรียก Save เมื่อมีการเปลี่ยนแปลง
//        }
//    }

//    public PlotState GetState()
//    {
//        return new PlotState
//        {
//            plotID = this.plotID,
//            plantDataName = currentPlantData != null ? currentPlantData.name : "",
//            currentGrowthStage = this.currentGrowthStage,
//            growthTimer = this.growthTimer,
//            isWatered = this.isWatered,
//            lastGrowthUpdateTime = _lastGrowthUpdateTime
//        };
//    }

//    public void LoadState(PlotState state, PlantData loadedPlantData)
//    {
//        this.plotID = state.plotID;
//        this.currentPlantData = loadedPlantData; // ต้องโหลด PlantData จากชื่อ
//        this.currentGrowthStage = state.currentGrowthStage;
//        this.growthTimer = state.growthTimer;
//        this.isWatered = state.isWatered;
//        this._lastGrowthUpdateTime = state.lastGrowthUpdateTime;

//        // คำนวณการเติบโตแบบ Offline
//        if (currentPlantData != null && _lastGrowthUpdateTime > 0)
//        {
//            DateTime lastUpdate = new DateTime(_lastGrowthUpdateTime);
//            TimeSpan elapsed = DateTime.Now - lastUpdate;
//            float elapsedSeconds = (float)elapsed.TotalSeconds;

//            while (elapsedSeconds > 0 && currentGrowthStage < currentPlantData.growthStages.Length - 1)
//            {
//                float remainingTimeToNextStage = currentPlantData.timeToGrowEachStage - growthTimer;
//                if (elapsedSeconds >= remainingTimeToNextStage)
//                {
//                    elapsedSeconds -= remainingTimeToNextStage;
//                    growthTimer = 0;
//                    currentGrowthStage++;
//                }
//                else
//                {
//                    growthTimer += elapsedSeconds;
//                    elapsedSeconds = 0;
//                }
//            }
//        }

//        UpdateVisuals();
//        // Debug.Log($"Plot {plotID}: Loaded state. Plant: {currentPlantData?.plantName ?? "Empty"}, Stage: {currentGrowthStage}");
//    }

//    private void UpdateVisuals()
//    {
//        // ลบ Model/Sprite เก่าออก
//        if (currentPlantInstance != null)
//        {
//            Destroy(currentPlantInstance);
//        }

//        if (currentPlantData != null && currentGrowthStage >= 0 && currentGrowthStage < currentPlantData.growthStages.Length)
//        {
//            GameObject stagePrefab = currentPlantData.growthStages[currentGrowthStage];
//            if (stagePrefab != null)
//            {
//                currentPlantInstance = Instantiate(stagePrefab, transform.position, Quaternion.identity, transform);
//                // ปรับตำแหน่ง/ขนาดตามความเหมาะสม
//            }
//        }
//        // ถ้า currentGrowthStage เป็น -1 (ว่างเปล่า) ก็ไม่แสดงอะไร
//    }
//}