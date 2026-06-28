using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] private Transform questListParent;
    [SerializeField] private GameObject questItemPrefab;

    [Header("Add Quest Form")]
    [SerializeField] private GameObject addQuestForm;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Dropdown levelDropdown;
    [SerializeField] private TMP_Text nameErrorText;

    [SerializeField] private TMP_Text panelTitleText;
    [SerializeField] private GameObject addQuestButton;
    [SerializeField] private QuestPanelController panelController; // QuestUI

    private bool showingCompleted;

    private readonly List<GameObject> spawned = new List<GameObject>();

    private void Awake()
    {
        if (panelController == null)
            panelController = GetComponent<QuestPanelController>();
    }

    private void Start()
    {
        if (QuestManager.Instance == null)
        {
            Debug.LogError("QuestManager missing in scene!");
            return;
        }

        if (addQuestForm != null)
            addQuestForm.SetActive(false);

        if (nameInput != null)
            nameInput.onSubmit.AddListener(SubmitFromInput);

        QuestManager.Instance.OnQuestsChanged += Refresh;
        Refresh();
    }

    public void ShowActiveQuests()
    {
        showingCompleted = false;
        if (panelTitleText != null) panelTitleText.text = "Quests";
        if (addQuestButton != null) addQuestButton.SetActive(true);
        CancelAddQuestForm();
        OpenQuestPanel();
        Refresh();
    }

    public void ShowCompletedQuests()
    {
        showingCompleted = true;
        if (panelTitleText != null) panelTitleText.text = "Completed";
        if (addQuestButton != null) addQuestButton.SetActive(false);
        CancelAddQuestForm();
        OpenQuestPanel();
        Refresh();
    }

    private void OnDestroy()
    {
        if (nameInput != null)
            nameInput.onSubmit.RemoveListener(SubmitFromInput);

        if (QuestManager.Instance != null)
            QuestManager.Instance.OnQuestsChanged -= Refresh;
    }

    // Main "Add" button — ONLY opens form
    public void OpenAddQuestForm()
    {
        if (addQuestForm == null) return;

        ShowNameError(false);
        addQuestForm.SetActive(true);
        if (nameInput != null)
        {
            nameInput.text = "";
            nameInput.ActivateInputField();
        }
        if (levelDropdown != null)
            levelDropdown.value = 0; // Easy
    }

    public void CancelAddQuestForm()
    {
        ShowNameError(false);
        if (addQuestForm != null)
            addQuestForm.SetActive(false);
    }


    private void OpenQuestPanel()
    {
        if (panelController == null)
        {
            Debug.LogError("QuestPanelController missing on QuestUI!");
            return;
        }

        panelController.OpenPanel();
    }

    private void ShowNameError(bool show, string message = "Name is required...")
    {
        if (nameErrorText == null) return;
        
        nameErrorText.gameObject.SetActive(show);

        if (show)
            nameErrorText.text = message;
    }

    // Confirm button on form — adds ONE quest
    public void ConfirmAddQuest()
    {
        if (nameInput == null || string.IsNullOrWhiteSpace(nameInput.text))
        {
            ShowNameError(true);
            return;
        }
         ShowNameError(false); 

        string title = nameInput.text.Trim();

        QuestLevel level = IndexToLevel(levelDropdown != null ? levelDropdown.value : 0);

        QuestManager.Instance.AddQuest(title, level);

        CancelAddQuestForm();
    }

    private QuestLevel IndexToLevel(int index)
    {
        switch (index)
        {
            case 1: return QuestLevel.Normal;
            case 2: return QuestLevel.Hard;
            case 3: return QuestLevel.Extreme;
            default: return QuestLevel.Easy;
        }
    }

    private void Refresh()
    {
        foreach (GameObject row in spawned)
            Destroy(row);
        spawned.Clear();

        if (QuestManager.Instance == null || questItemPrefab == null || questListParent == null)
            return;

        foreach (Quest q in QuestManager.Instance.GetQuests())
        {

            if (showingCompleted && !q.isCompleted) continue;
            if (!showingCompleted && q.isCompleted) continue;

            GameObject row = Instantiate(questItemPrefab, questListParent);
            row.GetComponent<QuestItemUI>().Setup(q, showingCompleted);
            spawned.Add(row);
        }
    }

    public void SubmitFromInput(string _)
    {
        if (nameInput == null || string.IsNullOrWhiteSpace(nameInput.text))
        {
           ShowNameError(true);
           return;
        }
     
           ConfirmAddQuest();
    }

    public void CancelFromInput(string _)
    {
           CancelAddQuestForm();
    }
    

}