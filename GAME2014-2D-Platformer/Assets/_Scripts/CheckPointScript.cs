using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public Sprite[] sprites;

    private bool isActive = false;

    private Animator animator;
    private SpriteRenderer sr;

    private Sprite CheckpointDead;
    private Sprite CheckpointActive;
    private CircleCollider2D cc;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        CheckpointDead = sprites[0];
        CheckpointActive = sprites[1];
        sr.sprite = CheckpointDead;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive)
        {
            isActive = true;
            sr.sprite = CheckpointActive;
            animator.SetTrigger("Activate");
            cc.enabled = false;
        }
    }
}
