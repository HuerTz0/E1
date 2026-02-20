using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerController : MonoBehaviour
{

    float movementX;
    float movementY;
    [SerializeField] float speed = 2.0f;
    Rigidbody2D rb;
    bool isGrounded;
    int score = 0;
    [SerializeField] GameManager gm;

    Animator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] Transform groundCheck;

    public UnityEvent playerDeath;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*float movementDistanceX = movementX * speed * Time.deltaTime;
        float movementDistanceY = movementY * speed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + movementDistanceX, transform.position.y + movementDistanceY);
        */
        rb.linearVelocity = new Vector2(movementX * speed, rb.linearVelocity.y);
       isGrounded = CheckisGrounded();
        if (movementY > 0 && isGrounded)
        {
            rb.AddForce(new Vector2(0, 100));
            source.PlayOneShot(jumpClip);
        }
        if(!Mathf.Approximately(movementX,0f))
        {
            animator.SetBool("isRunning", true);
           spriteRenderer.flipX = movementX < 0;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if(!Mathf.Approximately(rb.linearVelocity.y,0f))
        {
            animator.SetBool("isGrounded", false);
        }
        else
        {
            animator.SetBool("isGrounded", true);
        }



    }
    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        movementX = v.x;
        movementY = v.y;
    }
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //}
    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            score++;
            other.gameObject.SetActive(false);
            gm.UpdateScore(score);
        }
        else if(other.gameObject.CompareTag("Spike"))
        {
            playerDeath.Invoke();
        }
    }
    void OnDash()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(movementX * 7000, movementY + 200));
        }
        else
        {
            rb.AddForce(new Vector2(movementX * 7000, 0));
        }
    }
    bool CheckisGrounded()
    {
        float rayCastDistance = 0.5f;
        int layerMask = 1 << 3;
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, rayCastDistance, layerMask);
        return hit.collider != null;
    }
    void OnJump()
    {
               if (isGrounded)
        {
            rb.AddForce(new Vector2(0, 200));
            source.PlayOneShot(jumpClip);
        }
    }
}
