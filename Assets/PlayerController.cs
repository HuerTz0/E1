using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    float movementX;
    float movementY;
    [SerializeField] float speed = 2.0f;
    Rigidbody2D rb;
    bool isGrounded;
    int score = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*float movementDistanceX = movementX * speed * Time.deltaTime;
        float movementDistanceY = movementY * speed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + movementDistanceX, transform.position.y + movementDistanceY);
        */
        rb.linearVelocity = new Vector2(movementX * speed, rb.linearVelocity.y);

        if (movementY > 0 && isGrounded)
        {
            rb.AddForce(new Vector2(0, 100));
        }
    }
    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        movementX = v.x;
        movementY = v.y;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            score++;
            other.gameObject.SetActive(false);
            Debug.Log("Score: " + score);
        }
    }
    void OnDash()
    {
                rb.AddForce(new Vector2(movementX * 7500, movementY*500));
    }
}
