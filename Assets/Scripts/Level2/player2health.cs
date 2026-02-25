using UnityEngine;
using System;

public class Player2Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int health = 10;
    public int currentHealth { get; private set; }
    public int maxHealth { get; private set; }

    [Header("Exhaust / Survival Settings")]
    [SerializeField] private int exhaust = 4; // Max capacity
    [SerializeField] private float drainInterval = 4f; // Time in seconds to lose 1 point
    public int currentExhaust { get; private set; }
    public int maxExhaust { get; private set; }

    private float drainTimer;

    [Header("Inventory Settings")]
    public int moneyCount = 0; 
    public static Action<int> onMoneyCollected; 

    private Animator animator;
    private const string FLASH_RED_ANIM = "FlashRed"; 

    // Static actions for HUD and GameControllers
    public static Action<int> onPlayerTakeDamage;
    public static Action<int> onExhaustChanged; 
    public static Action onPlayerDie;

    private void Awake()
    {
        currentHealth = health;
        maxHealth = health;

        currentExhaust = exhaust;
        maxExhaust = exhaust;
        
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleExhaustDrain();
    }

    private void HandleExhaustDrain()
    {
        drainTimer += Time.deltaTime;

        if (drainTimer >= drainInterval)
        {
            ReduceExhaust(1);
            drainTimer = 0; 
        }
    }

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
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. REFILL STAMINA ONLY (Use the "timepick" tag for Delivery)
        if (collision.CompareTag("timepick"))
        {
            currentExhaust = maxExhaust;
            drainTimer = 0; 
            onExhaustChanged?.Invoke(currentExhaust);

            Debug.Log("Delivery Picked Up! Exhaust Refilled.");
            Destroy(collision.gameObject);
        }
        
        // 2. MONEY COLLECTION ONLY (Money dropped from enemies or Meat)
        else if (collision.CompareTag("Meat"))
        {
            moneyCount++;
            onMoneyCollected?.Invoke(moneyCount);
            
            Debug.Log("Money collected! Total: " + moneyCount);
            Destroy(collision.gameObject);
        }
    }

    public void ReduceExhaust(int amount)
    {
        currentExhaust -= amount;
        currentExhaust = Mathf.Clamp(currentExhaust, 0, maxExhaust);
        
        onExhaustChanged?.Invoke(currentExhaust);

        if (currentExhaust <= 0)
        {
            Debug.Log("Time ran out!");
            Die();
        }
    }

    private void Die()
    {
        // Instead of Destroying, we find the Respawn point
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Respawn");

        if (spawnPoint != null)
        {
            // Move player to start
            transform.position = spawnPoint.transform.position;
            
            // Reset stats
            currentHealth = maxHealth;
            currentExhaust = maxExhaust;
            drainTimer = 0;

            // Update UI
            onPlayerTakeDamage?.Invoke(currentHealth);
            onExhaustChanged?.Invoke(currentExhaust);
            
            Debug.Log("Player Respawned at Start Portal.");
        }
        else
        {
            // Fallback: If no tag exists, just destroy
            onPlayerDie?.Invoke();
            Destroy(gameObject);
        }
    }
}