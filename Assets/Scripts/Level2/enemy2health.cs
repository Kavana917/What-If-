using UnityEngine;
using System.Collections;

public class enemy2health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 2; 
    private int currentHealth;

    [Header("Visual Feedback")]
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
        // Safety check: Don't take damage too fast (prevents multi-hits in one frame)
        if (Time.time < lastHitTime + 0.2f) return;
        lastHitTime = Time.time;

        currentHealth -= damage;
        Debug.Log(gameObject.name + " hit! Remaining Health: " + currentHealth);

        // Visual feedback
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
        Debug.Log("Enemy2 Destroyed!");

        // --- UPDATED CONNECTION FOR PLAYER 2 ---
        // Find Player2 using the Tag (Make sure your Player2 object is tagged "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Connects to your Player2Health script
            Player2Health p2Health = player.GetComponent<Player2Health>();
            
            if (p2Health != null)
            {
                // If you later add back the ReduceExhaust method to Player2Health, 
                // you can uncomment the line below:
                // p2Health.ReduceExhaust(1); 
            }
        }

        // Spawn the loot (Meat)
        if (meatPrefab != null)
        {
            Instantiate(meatPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}