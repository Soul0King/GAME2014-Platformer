using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    private Transform[] PatrolPoints = new Transform[2];

    Transform PPoint1;
    bool HasReachedP1 = true;
    Transform PPoint2;
    bool HasReachedP2 = false;

    public float horizontalForce = 500f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PatrolPoints[0] = transform.Find("PPoint1");
        PatrolPoints[1] = transform.Find("PPoint2");
        transform.DetachChildren();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PPoint1 = PatrolPoints[0];
        PPoint2 = PatrolPoints[1];
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (transform.position.x <= PPoint1.transform.position.x)
        {
            HasReachedP1 = true;
            HasReachedP2 = false;
            rb.linearVelocityX = 0;
        }
        else if (transform.position.x >= PPoint2.transform.position.x)
        {
            HasReachedP2 = true;
            HasReachedP1 = false;
            rb.linearVelocityX = 0;
        }
        //right
        if (HasReachedP1)
        {
            rb.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
        }
        //left
        else if (HasReachedP2)
        {
            rb.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
        }
    }
}
