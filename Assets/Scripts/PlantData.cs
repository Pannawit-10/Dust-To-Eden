//using NUnit.Framework.Interfaces;
//using UnityEngine;

//[CreateAssetMenu(fileName = "NewPlantData", menuName = "Farming/Plant Data")]
//public class PlantData : ScriptableObject
//{
//    public string plantName;
//    public GameObject[] growthStages; // ��������ͧ GameObject/Prefab ����Ѻ�������С���Ժ�
//    public float timeToGrowEachStage; // ���ҷ����㹡����������� (�� 10 �Թҷ�)
//    public GameObject harvestItemPrefab; // �ż�Ե������
//    public int minHarvestAmount;
//    public int maxHarvestAmount;
//    public Sprite seedIcon; // �ͤ͹����Ѻ����� Inventory
//}
//// �������Ҥس�դ��� ItemData ����Ѻ Inventory �ͧ�س
//[CreateAssetMenu(fileName = "NewSeedItem", menuName = "Inventory/Seed Item")]
//public class SeedItem : ItemData // �������� ItemData �繤��ʾ�鹰ҹ����Ѻ�����
//{
//    public PlantData plantData; // ��ҧ�ԧ��ѧ PlantData ������紹��л�١��
//    // public int amount; // �ҡ�� Stackable Item
//}
//[System.Serializable]
//public class PlotState
//{
//    public string plotID; // ID ੾�Тͧ�ŧ��١
//    public string plantDataName; // ���ͧ͢ PlantData �����ѧ��١���� (�����)
//    public int currentGrowthStage; // ���С���Ժ⵻Ѩ�غѹ
//    public float growthTimer; // ���ҷ�������㹡���Ժ�����лѨ�غѹ
//    public bool isWatered; // ʶҹС��ô���
//    public long lastGrowthUpdateTime; // ���ҷ���ѻവ����Ժ�����ش (��㹡�äӹǳ Offline Growth)
//}