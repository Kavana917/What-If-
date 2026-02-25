using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int health = 10;
    public int currentHealth { get; private set; }
    public int maxHealth { get; private set; }

    [Header("Exhaust Settings")]
    [SerializeField] private int exhaust = 10; // NEW: Initial exhaust value
    public int currentExhaust { get; private set; }
    public int maxExhaust { get; private set; }

    [Header("Inventory Settings")]
    public int meatCount = 0;
    public static Action<int> onMeatCollected; // Event for the HUD

    private Animator animator;
    private const string FLASH_RED_ANIM = "FlashRed";

    // Actions for HUD to listen to
    public static Action<int> onPlayerTakeDamage;
    public static Action<int> onExhaustChanged; // NEW: Action for exhaust updates
    public static Action onPlayerDie;

    private void Awake()
    {
        // Initialize Health
        currentHealth = health;
        maxHealth = health;

        // Initialize Exhaust
        currentExhaust = exhaust;
        maxExhaust = exhaust;
        
        animator = GetComponent<Animator>();
    }

    // --- HEALTH LOGIC ---
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        onPlayerTakeDamage?.Invoke(currentHealth);

        if (animator != null)
        {
            animator.SetTrigger(FLASH_RED_ANIM);
        }

        if (currentHealth <= 0)
        {
            onPlayerDie?.Invoke();
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Meat"))
    {
        meatCount++;
        onMeatCollected?.Invoke(meatCount);
        Destroy(collision.gameObject);
        Debug.Log("Meat collected! Total: " + meatCount);
    }
}

    // --- EXHAUST LOGIC ---
    public void ReduceExhaust(int amount)
    {
        currentExhaust -= amount;
        
        // Clamp ensures the value doesn't go below 0
        currentExhaust = Mathf.Clamp(currentExhaust, 0, maxExhaust);
        
        // Notify HUD
        onExhaustChanged?.Invoke(currentExhaust);

        Debug.Log("Exhaust Reduced! Current: " + currentExhaust);

        if (currentExhaust <= 0)
        {
            onPlayerDie?.Invoke();
            Destroy(gameObject);
        }
    }
}