using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // private void Start()
    // {
    //     UpdateUI();
    // }

    public void AddWood(int amount = 1)
    {
        if (ResourceManager.Instance != null)
            ResourceManager.Instance.AddWood(amount);
    }

    // private void UpdateUI()
    // {
    //     if (woodText != null)
    //         woodText.text = "Wood: " + woodCount;
    // }
}