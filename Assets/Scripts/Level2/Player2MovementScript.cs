using UnityEngine;
using UnityEngine.InputSystem;

public class Player2MovementScript : MonoBehaviour
{
    [Header("Movement Stats")]
    public float moveSpeed = 8f;
    public float jumpForce = 10f;

    [Header("Detection Settings")]
    [SerializeField] private int thornDamage = 2; 
    [SerializeField] private float damageInterval = 1f; 

    [Header("Combat Settings")]
    [SerializeField] private int attackDamage = 1; 
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [Header("Ranged Combat")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint; // A child object where arrow starts

    private Player2Health playerHealth; // Updated for Level 2
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    private float moveInput;
    private float damageTimer;
    private bool isTouchingHazard; 
    private bool isTouchingEnemy; 
    private bool attack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<Player2Health>(); // Updated for Level 2
        
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        HandleInput();
        HandleAttacks();
        HandleHazardDamage();
    }

    void FixedUpdate()
    {
        HandleMovement();
        attack = false; 
    }

    private void HandleInput()
    {
        if (Keyboard.current == null) return;

        moveInput = 0;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveInput = -1;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveInput = 1;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) attack = true;

        bool jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame || 
                           Keyboard.current.wKey.wasPressedThisFrame || 
                           Keyboard.current.upArrowKey.wasPressedThisFrame;
        
        // Simple ground check via velocity
        if (jumpPressed && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void HandleMovement()
    {
        // Prevent movement sliding during attack if you want the attack to feel "heavy"
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        // Updated parameter name to match your Animator screenshot: "IsWalking"
        animator.SetBool("IsWalking", moveInput != 0);

        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleAttacks()
    {
        // Using "attack" trigger from your screenshot
        if (attack && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            animator.SetTrigger("attack"); 
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); 
            PerformAttack(); 
        }
    }

    private void PerformAttack()
{
    // 1. Melee detection (using your existing AttackPoint and attackRange)
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    foreach (Collider2D enemy in hitEnemies)
    {
        enemy.GetComponent<enemy2health>()?.TakeDamage(attackDamage);
    }

    // 2. Spawn Arrow
    if (arrowPrefab != null && attackPoint != null)
    {
        // 1. Calculate rotation BEFORE instantiating
        // 0 degrees for right, 180 degrees for left
        float angle = (transform.localScale.x > 0) ? 0f : 180f;
        Quaternion spawnRotation = Quaternion.Euler(0, 0, angle);

        // 2. Spawn the arrow WITH the rotation
        GameObject newArrow = Instantiate(arrowPrefab, attackPoint.position, spawnRotation);
    }
}

    private void HandleHazardDamage()
    {
        if (isTouchingHazard || isTouchingEnemy)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                int damageToApply = isTouchingEnemy ? 2 : thornDamage;
                playerHealth.TakeDamage(damageToApply);
                damageTimer = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            isTouchingHazard = true; 
            playerHealth.TakeDamage(thornDamage);
            damageTimer = 0;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Simple stomp logic: if player is above enemy
            if (transform.position.y > collision.transform.position.y + 0.5f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 5f); 
                isTouchingEnemy = false; 
            }
            else 
            {
                isTouchingEnemy = true;
                playerHealth.TakeDamage(2); 
                damageTimer = 0;
                
                // Knockback
                float knockbackDir = (transform.position.x > collision.transform.position.x) ? 4f : -4f;
                rb.linearVelocity = new Vector2(knockbackDir, 3f);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard")) isTouchingHazard = false;
        if (collision.gameObject.CompareTag("Enemy")) isTouchingEnemy = false;
        damageTimer = 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}