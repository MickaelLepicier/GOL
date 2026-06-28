using UnityEngine;

public class QuestPanelController : MonoBehaviour
{
    [SerializeField] private GameObject questPanel;
    [SerializeField] private GameObject modalBlocker;

    public bool IsOpen => questPanel != null && questPanel.activeSelf;
    
    public void OpenPanel()
    {
       SetOpen(true);
    }

    public void ToggleQuestPanel()
    {
        bool open = !questPanel.activeSelf;
        SetOpen(open);
    }

    public void CloseQuestPanel()
    {
        SetOpen(false);
    }

    private void SetOpen(bool open)
    {
        questPanel.SetActive(open);
        if (modalBlocker != null)
            modalBlocker.SetActive(open);
    }
}