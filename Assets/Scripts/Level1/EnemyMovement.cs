using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [Header("Chase Stats")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float stunDuration = 1.0f; // How long it waits before chasing again

    private Transform playerTransform;
    private Vector2 movement;
    private float stunTimer;

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>(); 
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        FindPlayer();
    }

    private void Update()
    {
        // Countdown the stun timer
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (playerTransform == null)
        {
            FindPlayer();
            return;
        }

        // If stunned, don't execute chasing logic
        if (stunTimer > 0) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    private void ChasePlayer()
    {
        float directionX = playerTransform.position.x - transform.position.x;
        float directionY = playerTransform.position.y - transform.position.y;

        // NEW: Detection for when player is on head
        if (Mathf.Abs(directionX) < 0.6f && directionY > 0.3f)
        {
            TriggerStun(directionX);
            return;
        }

        Vector3 localScale = transform.localScale;

        if (directionX > 0)
        {
            movement.x = speed;
            localScale.x = -1f; 
        }
        else if (directionX < 0)
        {
            movement.x = -speed;
            localScale.x = 1f;
        }

        transform.localScale = localScale;
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
    }

    private void TriggerStun(float dirX) // dirX is still passed but we use localScale instead
{
    stunTimer = stunDuration;
    // If localScale.x is -1 (Facing Right), backStep should be negative (Left)
    // If localScale.x is 1 (Facing Left), backStep should be positive (Right)
    float backStep = (transform.localScale.x < 0) ? -5f : 5f; 
    
    // Apply the backward shove at 5f speed
    rb.linearVelocity = new Vector2(backStep, rb.linearVelocity.y);
}

    private void FindPlayer()
    {
        GameObject playerObj = GameObject.Find("Player(Clone)");
        if (playerObj != null) playerTransform = playerObj.transform;
    }
    
}