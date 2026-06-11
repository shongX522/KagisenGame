
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.linearVelocityX = 5f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.linearVelocityX = -5f;
        }
        else { rb.linearVelocityX = 0f; }
    }
}
