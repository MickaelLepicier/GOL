using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [Header("Resources")]
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text stoneText;

    [Header("Coins")]
    [SerializeField] private TMP_Text bronzeText;
    [SerializeField] private TMP_Text silverText;
    [SerializeField] private TMP_Text goldText;

    private int wood;
    private int stone;
    private int totalBronze; // single source for coin tiers

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start() => UpdateUI();

    public void AddWood(int amount) { wood += amount; UpdateUI(); }
    public void AddStone(int amount) { stone += amount; UpdateUI(); }

    public void AddBronze(int amount)
    {
        totalBronze += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (woodText != null) woodText.text = "Wood: " + wood;
        if (stoneText != null) stoneText.text = "Stone: " + stone;

        int gold = totalBronze / 10000;
        int silver = (totalBronze / 100) % 100;
        int bronze = totalBronze % 100;

        if (bronzeText != null) bronzeText.text = "B: " + bronze;
        if (silverText != null) silverText.text = "S: " + silver;
        if (goldText != null) goldText.text = "G: " + gold;
    }
}