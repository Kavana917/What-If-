using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
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

    private PlayerHealth playerHealth;
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
        playerHealth = GetComponent<PlayerHealth>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        HandleInput();
        
        // FIXED: HandleAttacks is now in Update to prevent multiple hits per physics tick
        HandleAttacks();
        
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

    void FixedUpdate()
    {
        HandleMovement();
        // HandleAttacks removed from here
        attack = false; 
    }

    private void HandleInput()
    {
        if (Keyboard.current == null || Mouse.current == null) return;

        moveInput = 0;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveInput = -1;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveInput = 1;

        if (Mouse.current.leftButton.wasPressedThisFrame) attack = true;

        bool jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame || 
                           Keyboard.current.wKey.wasPressedThisFrame || 
                           Keyboard.current.upArrowKey.wasPressedThisFrame;
        
        if (jumpPressed && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void HandleMovement()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        animator.SetBool("isRunning", moveInput != 0);

        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleAttacks()
    {
        if (attack && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetTrigger("attack"); 
            rb.linearVelocity = Vector2.zero; 
            PerformAttack(); 
        }
    }

    private void PerformAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth eHealth = enemy.GetComponent<EnemyHealth>();
            if (eHealth != null)
            {
                eHealth.TakeDamage(attackDamage);
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
            if (transform.position.y > collision.transform.position.y + 0.5f)
            {
                playerHealth.TakeDamage(2); 
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 5f); 
                isTouchingEnemy = false; 
            }
            else 
            {
                isTouchingEnemy = true;
                playerHealth.TakeDamage(2); 
                damageTimer = 0;
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