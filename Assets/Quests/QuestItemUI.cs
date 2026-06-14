using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Button completeButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private TMP_Text completeMark; // green "V"

    private string questId;


    public void Setup(Quest quest, bool isCompletedView)
    {
        questId = quest.id;
        titleText.text = quest.title;
        levelText.text = quest.GetLevelShort();
       
        bool done = quest.isCompleted;
       
        if (completeButton != null)
        {
            // Active list: show V button if not done
            // Completed list: never show button (only green V)
            bool showButton = !isCompletedView && !done;
            completeButton.gameObject.SetActive(showButton);
          
            completeButton.onClick.RemoveAllListeners();
          
            if (showButton)
                completeButton.onClick.AddListener(() => QuestManager.Instance.CompleteQuest(questId));
        }
     
        if (completeMark != null)
        {
            completeMark.gameObject.SetActive(done);
            completeMark.text = "V";
            // set color green in Inspector
        }
        
        deleteButton.onClick.RemoveAllListeners();
        deleteButton.onClick.AddListener(() => QuestManager.Instance.DeleteQuest(questId));
    }

}