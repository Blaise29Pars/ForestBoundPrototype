using UnityEngine;

public class HedgeHog : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    public Transform leftLimit;
    public Transform rightLimit;

    [Header("Movement")]
    public float chaseSpeed = 3f;
    public float jumpForce = 3f;
    public LayerMask groundLayer;

    [Header("Chasing")]
    public float stopDistance = 0.5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private bool shouldJump;
    private float direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null || leftLimit == null || rightLimit == null || rb == null)
        {
            return;
        }

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        float xDistance = player.position.x - transform.position.x;

        if (Mathf.Abs(xDistance) > stopDistance)
        {
            direction = Mathf.Sign(xDistance);
        }
        else
        {
            direction = 0f;
        }

        bool tryingToGoLeftPastLimit = transform.position.x <= leftLimit.position.x && direction < 0;
        bool tryingToGoRightPastLimit = transform.position.x >= rightLimit.position.x && direction > 0;

        if (tryingToGoLeftPastLimit || tryingToGoRightPastLimit)
        {
            direction = 0f;
        }

        if (spriteRenderer != null && direction != 0)
        {
            spriteRenderer.flipX = direction < 0;
        }

        if (isGrounded)
        {
            rb.velocity = new Vector2(direction * chaseSpeed, rb.velocity.y);

            if (direction != 0)
            {
                RaycastHit2D groundInFront = Physics2D.Raycast(
                    transform.position,
                    new Vector2(direction, 0),
                    2f,
                    groundLayer
                );

                RaycastHit2D gapAhead = Physics2D.Raycast(
                    transform.position + new Vector3(direction, 0, 0),
                    Vector2.down,
                    2f,
                    groundLayer
                );

                if (!groundInFront.collider && !gapAhead.collider)
                {
                    shouldJump = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (leftLimit == null || rightLimit == null)
        {
            return;
        }

        float clampedX = Mathf.Clamp(transform.position.x, leftLimit.position.x, rightLimit.position.x);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        if (rb == null || player == null)
        {
            return;
        }

        if (isGrounded && shouldJump)
        {
            shouldJump = false;

            rb.AddForce(new Vector2(direction * jumpForce, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerRespawn playerRespawn = collision.gameObject.GetComponent<PlayerRespawn>();

            if (playerRespawn != null)
            {
                playerRespawn.Respawn();
            }
        }
    }
}