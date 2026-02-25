using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this at the top

public class HUD : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider exhaustBar; // NEW: Drag your ExhaustBar slider here
    [SerializeField] private TextMeshProUGUI meatText; // Drag your Text object here
    private int maxHealth;
    private int maxExhaust; // NEW: Store max exhaust

    private void OnEnable()
{
    GameController.onPlayerSpawned += SetupBars;
    PlayerHealth.onPlayerTakeDamage += UpdateHealthBar;
    PlayerHealth.onExhaustChanged += UpdateExhaustBar; // Listen to the new static action
    PlayerHealth.onMeatCollected += UpdateMeatText;
}

private void OnDisable()
{
    GameController.onPlayerSpawned -= SetupBars;
    PlayerHealth.onPlayerTakeDamage -= UpdateHealthBar;
    PlayerHealth.onExhaustChanged -= UpdateExhaustBar;
    PlayerHealth.onMeatCollected -= UpdateMeatText;
}

private void SetupBars(GameObject player)
{
    PlayerHealth ph = player.GetComponent<PlayerHealth>();
    if (ph != null)
    {
        // Setup Health
        maxHealth = ph.maxHealth;
        healthBar.value = 1f;

        // Setup Exhaust
        maxExhaust = ph.maxExhaust; // Now getting this from PlayerHealth
        exhaustBar.value = 1f;
    }
}

    private void UpdateHealthBar(int currentHealth)
    {
        healthBar.value = Mathf.Clamp01((float)currentHealth / maxHealth);
    }

    // NEW: Logic for the Exhaust slider
    private void UpdateExhaustBar(int currentExhaust)
    {
        exhaustBar.value = Mathf.Clamp01((float)currentExhaust / maxExhaust);
    }
    private void UpdateMeatText(int count)
    {
        meatText.text = "Meat : " + count;
    }
    public void HighlightText()
    {
        // Change the color to Green directly via code
        meatText.color = Color.black;
        
        // Or use specific RGB values (0-1 range)
        // meatCountText.color = new Color(1f, 0.5f, 0f); // Orange
    }
}