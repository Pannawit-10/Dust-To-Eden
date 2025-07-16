using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public Transform questListContent;
    public GameObject questEntryPrefab;
    public GameObject objectiveTextPrefab;

    public Quest testQuest;
    public int testQusetAmount;
    private List<QuestProgress> testQuests = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < testQusetAmount; i++)
        {
            testQuests.Add(new QuestProgress(testQuest));
        }

        UpdateQusetUI();
    }

    public void UpdateQusetUI()
    {
        foreach(Transform child in questListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var questProgress in testQuests)
        {
            GameObject entry = Instantiate(questEntryPrefab, questListContent);
            TMP_Text questNameText = entry.transform.Find("QuestName").GetComponent<TMP_Text>();
            Transform objectiveList = entry.transform.Find("ObjectivesList");

            questNameText.text = questProgress.quest.questName;

            foreach (var objective in questProgress.objectives)
            {
                GameObject objectGO = Instantiate(objectiveTextPrefab, objectiveList);
                TMP_Text objectiveText = objectGO.GetComponent<TMP_Text>();
                objectiveText.text = $"{objective.description} ({objective.currentAmount}/{objective.requiredAmount})";
            }
        }
    }
}
