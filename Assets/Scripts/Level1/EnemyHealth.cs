using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 2; 
    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float lastHitTime;
    [Header("Loot Settings")]
    [SerializeField] private GameObject meatPrefab; // Drag your Meat Prefab here in the Inspector

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int damage)
    {
        // Safety check: Don't take damage too fast
        if (Time.time < lastHitTime + 0.2f) return;
        lastHitTime = Time.time;

        currentHealth -= damage;
        Debug.Log("Enemy hit! Remaining Health: " + currentHealth);

        // Visual feedback for the enemy being hit
        if (spriteRenderer != null) StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    void Die()
    {
        Debug.Log("Enemy Destroyed!");

        // --- NEW EXHAUST CONNECTION ---
        // Find the player object using its Tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Get the PlayerHealth script (which now contains ReduceExhaust)
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            
            if (ph != null)
            {
                ph.ReduceExhaust(1); // Reduces the bar by 2 points (20% of 10)
            }
        }

        // Spawn the meat at the Bull's current position
    if (meatPrefab != null)
    {
        Instantiate(meatPrefab, transform.position, Quaternion.identity);
    }

    Destroy(gameObject);

    }
}