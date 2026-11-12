using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Joystick joystick;
    [Range(0f, 1f)] public float joystickHorizontalSensitivty = 0.2f;
    [Range(0f, 1f)] public float joystickVerticalSensitivty = 0.15f;

    public float horizontalForce = 500f;
    public float verticalForce = 10000f;

    public Transform spawnPoint;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    public bool isGrounded;
    public bool isJumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.SetInteger("AnimState", 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //Right
        if(joystick.Horizontal > joystickHorizontalSensitivty)
        {
            rb.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
            sr.flipX = false;
            if (!isJumping) animator.SetInteger("AnimState", 1);
        } 
        //Left
        else if(joystick.Horizontal < -joystickHorizontalSensitivty)
        {
            rb.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
            sr.flipX = true;
            if (!isJumping) animator.SetInteger("AnimState", 1);
            
        }
        else if (!isJumping)
        {
            animator.SetInteger("AnimState", 0);
        }
        //Jump
        if(joystick.Vertical > joystickVerticalSensitivty && isGrounded)
        {
            isJumping = true;
            rb.AddForce(Vector2.up * verticalForce * Time.deltaTime);
            animator.SetInteger("AnimState", 2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        isJumping = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("DeathPlane") && spawnPoint != null)
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = spawnPoint.position;
        }
        if (other.CompareTag("Enemy"))
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = spawnPoint.position;
        }
    }
}
