using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 15f;
    public int damage = 1;
    public float lifeTime = 3f;

    private Rigidbody2D rb;
    private bool hasSetVelocity = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Fallback destruction
        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
{
    if (!hasSetVelocity && rb != null)
    {
        // Force the Rigidbody to wake up
        rb.WakeUp();
        
        // Use transform.right * speed. 
        // If rotation is 0, this is (1, 0). If rotation is 180, this is (-1, 0).
        rb.linearVelocity = transform.right * speed;
        
        hasSetVelocity = true;
    }
}

    private void OnTriggerEnter2D(Collider2D collision)
{
    // 1. Ignore the player so the arrow doesn't hit you instantly
        if (collision.CompareTag("Player")) return;

        // 2. If it hits an enemy, deal damage
        if (collision.CompareTag("Enemy"))
        {
            enemy2health enemy = collision.GetComponent<enemy2health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        // 3. Destroy the arrow no matter what it hit (Wall, Floor, Enemy, etc.)
        Destroy(gameObject);
}
}