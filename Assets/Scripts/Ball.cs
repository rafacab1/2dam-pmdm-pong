using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 4;
    public float speedIncrement = 1.05f;
    private Vector2 startPos;


    void Start()
    {
        startPos = transform.position;
        Launch();
    }

    public void Reset()
    {
        transform.position = startPos;
        rb.velocity = Vector2.zero;
        Launch();
    }

    public void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        rb.velocity = new Vector2(speed * x, speed * y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // spin
        transform.Rotate(new Vector3(0, 0, collision.relativeVelocity.x * 10f), Space.Self);

        if (collision.gameObject.CompareTag("Paddle") || collision.gameObject.CompareTag("Wall"))
        {
            // Incremento de velocidad en rebotes

            // Limitar velocidad de la bola para que no se vaya la bola volando
            float velocityX = rb.velocity.x;
            float velocityY = rb.velocity.y;

            if (System.Math.Abs(velocityX) <= 25)
            {
                velocityX *= speedIncrement;
            } 
            if (System.Math.Abs(velocityY) <= 25)
            {
                velocityY *= speedIncrement;
            }

            // Incrementar velocidad
            rb.velocity = new Vector2(velocityX, velocityY);

        }

        if (collision.gameObject.CompareTag("Goal1"))
        {
            Collider2D collider = collision.collider;

            if (collider.bounds.center.x < collision.contacts[0].point.x) 
            {
                GameManager.Instance.Player2Scored();
            }
        }

        if (collision.gameObject.CompareTag("Goal2")) 
        {
            Collider2D collider = collision.collider;

            if (collider.bounds.center.x > collision.contacts[0].point.x) 
            {
                GameManager.Instance.Player1Scored();
            }
        }
    }
}
