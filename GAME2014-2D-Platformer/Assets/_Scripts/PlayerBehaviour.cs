using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public Joystick joystick;
    [Range(0f, 1f)] public float joystickHorizontalSensitivty = 0.2f;
    [Range(0f, 1f)] public float joystickVerticalSensitivty = 0.15f;

    public float horizontalForce = 500f;
    public float knockbackForce = 250f;
    public float verticalForce = 10000f;

    public Transform spawnPoint;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private CinemachineBasicMultiChannelPerlin camShaker;

    public bool isGrounded;
    public bool isJumping;
    private bool isEnemyToTheRight;

    public int MaxHealth = 3;
    private int CurrentHealth;

    public Slider HealthBar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        camShaker = FindAnyObjectByType<CinemachineBasicMultiChannelPerlin>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.SetInteger("AnimState", 0);
        CurrentHealth = MaxHealth;
        HealthBar.value = CurrentHealth;
    }

    // Update is called once per frame
    void Update()
    {

  
    }

    private void FixedUpdate()
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
        if(joystick.Vertical > joystickVerticalSensitivty && isGrounded && rb.linearVelocityY <= 0)
        {
            isJumping = true;
            rb.AddForce(Vector2.up * verticalForce * Time.deltaTime);
            animator.SetInteger("AnimState", 2);
        }
    }

    private void EnemyDirection(GameObject enemy)
    {
        if (enemy.transform.position.x > transform.position.x)
        {
            isEnemyToTheRight = true;
        }
        else if (enemy.transform.position.x < transform.position.x)
        {
            isEnemyToTheRight = false;
        }
    }

    private void Die()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = spawnPoint.position;
        CurrentHealth = 3;
        HealthBar.value = CurrentHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        isJumping = false;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyDirection(collision.gameObject);
            if (isEnemyToTheRight) 
            { 
                rb.AddForce(Vector2.left * knockbackForce); 
            }
            else 
            { 
                rb.AddForce(Vector2.right * knockbackForce); 
            }
            camShaker.AmplitudeGain = 1;
            CurrentHealth--;
            HealthBar.value = CurrentHealth;
            if(CurrentHealth <= 0)
            {
                Die();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        camShaker.AmplitudeGain = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("DeathPlane") && spawnPoint != null)
        {
            Die();
        }

        if (other.CompareTag("EndGoal"))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        
    }

}
