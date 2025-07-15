//using UnityEngine;
//using System; // ����Ѻ DateTime/TimeSpan
//// using MyGame.Inventory; // ��� InventoryController ����� namespace ���

//public class FarmPlot : MonoBehaviour
//{
//    public string plotID; // ��ͧ��˹� ID � Inspector ���� Generate �ѵ��ѵ�
//    public PlantData currentPlantData;
//    public int currentGrowthStage = -1; // -1 = empty, 0 = seed, 1 = sprout, etc.
//    public float growthTimer; // ���ҷ������ stage �Ѩ�غѹ
//    public bool isWatered = false;
//    private GameObject currentPlantInstance; // ��ҧ�ԧ�֧ prefab ������� instantiate ��

//    // ����Ѻ Offline Growth
//    private long _lastGrowthUpdateTime; // �� System.DateTime.Ticks

//    void Awake()
//    {
//        if (string.IsNullOrEmpty(plotID))
//        {
//            plotID = GlobalHelper.GenerateUniqueId(gameObject); // �� GlobalHelper �ͧ�س
//        }
//    }

//    void Start()
//    {
//        // ����ա����Ŵ�� ��ͧ���¡ LoadState()
//        // ����繤����á ����ʴ�ʶҹ���ҧ����
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
//            currentGrowthStage = 0; // ������ҡ��������
//            growthTimer = 0;
//            isWatered = false; // ���絡��ô�������ͻ�١����
//            _lastGrowthUpdateTime = DateTime.Now.Ticks; // �ѹ�֡���һ�١
//            UpdateVisuals();
//            // Debug.Log($"Plot {plotID}: Planted {plantData.plantName}");
//            SaveLoadManager.Instance.SaveGame(); // ���¡ Save ������ա������¹�ŧ
//        }
//    }

//    public void Water()
//    {
//        if (currentPlantData != null && !isWatered)
//        {
//            isWatered = true;
//            // �Ҩ���ռŵ�͡���Ժ� �� Ŵ growthTimer �������� growthRate
//            // Debug.Log($"Plot {plotID}: Watered plant.");
//            SaveLoadManager.Instance.SaveGame(); // ���¡ Save ������ա������¹�ŧ
//        }
//    }

//    public void Harvest()
//    {
//        if (currentPlantData != null && currentGrowthStage == currentPlantData.growthStages.Length - 1)
//        {
//            // ������Ǽż�Ե
//            // int harvestAmount = Random.Range(currentPlantData.minHarvestAmount, currentPlantData.maxHarvestAmount + 1);
//            // InventoryController.AddItem(currentPlantData.harvestItemPrefab, harvestAmount); // ��ͧ���к� Inventory

//            currentPlantData = null;
//            currentGrowthStage = -1;
//            growthTimer = 0;
//            isWatered = false;
//            _lastGrowthUpdateTime = 0;
//            UpdateVisuals();
//            // Debug.Log($"Plot {plotID}: Harvested plant.");
//            SaveLoadManager.Instance.SaveGame(); // ���¡ Save ������ա������¹�ŧ
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
//        this.currentPlantData = loadedPlantData; // ��ͧ��Ŵ PlantData �ҡ����
//        this.currentGrowthStage = state.currentGrowthStage;
//        this.growthTimer = state.growthTimer;
//        this.isWatered = state.isWatered;
//        this._lastGrowthUpdateTime = state.lastGrowthUpdateTime;

//        // �ӹǳ����Ժ�Ẻ Offline
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
//        // ź Model/Sprite ����͡
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
//                // ��Ѻ���˹�/��Ҵ��������������
//            }
//        }
//        // ��� currentGrowthStage �� -1 (��ҧ����) ������ʴ�����
//    }
//}