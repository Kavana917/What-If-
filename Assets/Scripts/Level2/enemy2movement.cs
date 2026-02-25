using UnityEngine;

public class enemy2movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator; 
    
    [Header("Chase Stats")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float stunDuration = 1.0f; 

    [Header("Ledge Detection")]
    [SerializeField] private Transform ledgeCheck; 
    [SerializeField] private float checkDistance = 0.5f;

    private Transform playerTransform;
    private Vector2 movement;
    private float stunTimer;
    private bool isAttacking = false;

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>(); 
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();
        
        FindPlayer();
    }

    private void Update()
    {
        if (stunTimer > 0) stunTimer -= Time.deltaTime;
        UpdateAnimation();
    }

    // We use LateUpdate for Flipping to override any Scale keyframes in Animations
    private void LateUpdate()
    {
        if (playerTransform == null) return;

        // Determine direction to player
        float directionX = playerTransform.position.x - transform.position.x;
        Vector3 scale = transform.localScale;

        if (directionX > 0.1f) // Player is to the right
        {
            // If enemy faces left by default, scale.x should be negative to face right
            scale.x = Mathf.Abs(scale.x); 
        }
        else if (directionX < -0.1f) // Player is to the left
        {
            scale.x = -Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    private void FixedUpdate()
    {
        if (playerTransform == null)
        {
            FindPlayer();
            return;
        }

        // Stop moving if attacking, stunned, or at a ledge
        if (stunTimer > 0 || isAttacking || !IsGroundAhead()) 
        {
            StopMoving();
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            StopMoving();
        }
    }

    private bool IsGroundAhead()
    {
        if (ledgeCheck == null) return true; 

        // Use Raycast to check for ground
        RaycastHit2D hit = Physics2D.Raycast(ledgeCheck.position, Vector2.down, checkDistance, LayerMask.GetMask("Ground"));
        Debug.DrawRay(ledgeCheck.position, Vector2.down * checkDistance, Color.blue);
        
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            Player2Health pHealth = collision.gameObject.GetComponent<Player2Health>();
            if (pHealth != null) pHealth.TakeDamage(1); 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) isAttacking = false;
    }

    private void UpdateAnimation()
    {
        if (animator == null) return;

        if (isAttacking)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void ChasePlayer()
    {
        float directionX = playerTransform.position.x - transform.position.x;
        float directionY = playerTransform.position.y - transform.position.y;

        // Stun logic if player jumps on head
        if (Mathf.Abs(directionX) < 0.6f && directionY > 0.3f)
        {
            TriggerStun();
            return;
        }

        // Set movement velocity based on direction
        float moveDir = (directionX > 0) ? speed : -speed;
        rb.linearVelocity = new Vector2(moveDir, rb.linearVelocity.y);
    }

    private void StopMoving()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    private void TriggerStun()
    {
        stunTimer = stunDuration;
        float backStep = (transform.localScale.x < 0) ? -5f : 5f; 
        rb.linearVelocity = new Vector2(backStep, rb.linearVelocity.y);
    }

    private void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) playerTransform = playerObj.transform;
    }
}