using UnityEngine;

public class SpiderChase : MonoBehaviour
{
    public GameObject player;
    public float speed = 13f;
    private float distance;
    public float chaseDistance = 40f;
    public float startChaseDistance = 20f;
    private bool isChasing = false;
    private Rigidbody2D rb;
    private Health playerHealth;
    public LayerMask obstacleLayer;
    private Animator animator;
    private Quaternion lastChasingRotation;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastChasingRotation = transform.rotation;

    }

    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Vector2 rayStart = (Vector2)transform.position + direction * 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, direction, chaseDistance, obstacleLayer);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == player && distance < startChaseDistance)
            {
                isChasing = true;
                lastChasingRotation = Quaternion.Euler(Vector3.forward * angle);
                transform.rotation = lastChasingRotation;
            }
            else
            {
                isChasing = false;
            }
        }

        if (isChasing)
        {
            rb.velocity = direction * speed;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            if (!animator.GetBool("IsChasing"))
            {
                animator.SetBool("IsChasing", true);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            transform.rotation = lastChasingRotation;
            if (animator.GetBool("IsChasing"))
            {
                animator.SetBool("IsChasing", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Abigail"))
        {
            playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }
}
