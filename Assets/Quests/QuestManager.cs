using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private readonly List<Quest> quests = new List<Quest>();
    public event Action OnQuestsChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public IReadOnlyList<Quest> GetQuests() => quests;

    public void AddQuest(string title, QuestLevel level)
    {
        quests.Add(new Quest(title, level));
        OnQuestsChanged?.Invoke();
    }


    public void CompleteQuest(string id)
    {
        Quest q = quests.Find(x => x.id == id);
        if (q == null || q.isCompleted) return;

        q.isCompleted = true;
        OnQuestsChanged?.Invoke();

        ResourceManager.Instance.AddBronze(q.GetRewardBronze());
    }

    public void DeleteQuest(string id)
    {
        quests.RemoveAll(x => x.id == id);
        OnQuestsChanged?.Invoke();
    }
}