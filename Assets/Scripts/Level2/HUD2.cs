using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD2 : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider exhaustBar; 
    [SerializeField] private TextMeshProUGUI moneyText;

    private int maxHealth;
    private int maxExhaust;

    private void OnEnable()
    {
        // Subscribing to Level 2 events
        Game2Controller.onPlayer2Spawned += SetupBars;
        Player2Health.onPlayerTakeDamage += UpdateHealthBar;
        Player2Health.onExhaustChanged += UpdateExhaustBar; // Now active
        Player2Health.onMoneyCollected += UpdateMoneyText;  // Now active
    }

    private void OnDisable()
    {
        // Unsubscribing to prevent memory leaks
        Game2Controller.onPlayer2Spawned -= SetupBars;
        Player2Health.onPlayerTakeDamage -= UpdateHealthBar;
        Player2Health.onExhaustChanged -= UpdateExhaustBar;
        Player2Health.onMoneyCollected -= UpdateMoneyText;
    }

    private void SetupBars(GameObject player)
    {
        Player2Health ph = player.GetComponent<Player2Health>();
        if (ph != null)
        {
            // Set Max values
            maxHealth = ph.maxHealth;
            maxExhaust = ph.maxExhaust;

            // Set initial Bar values
            healthBar.value = (float)ph.currentHealth / maxHealth;
            
            if(exhaustBar != null)
            {
                exhaustBar.value = (float)ph.currentExhaust / maxExhaust;
            }
            
            // Set initial Money text
            UpdateMoneyText(ph.moneyCount);
        }
    }

    private void UpdateHealthBar(int currentHealth)
    {
        if (healthBar != null)
        {
            healthBar.value = Mathf.Clamp01((float)currentHealth / maxHealth);
        }
    }

    private void UpdateExhaustBar(int currentExhaust)
    {
        if (exhaustBar != null)
        {
            exhaustBar.value = Mathf.Clamp01((float)currentExhaust / maxExhaust);
        }
    }

    private void UpdateMoneyText(int count)
    {
        if (moneyText != null)
        {
            moneyText.text = "Money : " + count;
        }
    }
}