using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody2D rigidBody2D;
    public Collider2D boxCollider;
    public bool isPlayer1;
    private float inputX;
    private float inputY;
    public float moveSpeed = 100;
    private Vector2 direction;

    void Update()
    {
        HandleInput();
        AllowBallBackCollision();
    }

    private void HandleInput() 
    {
        if (isPlayer1) {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
        }
        else
        {
            inputX = Input.GetAxisRaw("Horizontal2");
            inputY = Input.GetAxisRaw("Vertical2");
        }
        

        if (inputX == 1)
        {
            direction = Vector2.right;
        }
        else if (inputX == -1)
        {
            direction = Vector2.left;
        }


        if (inputY == 1)
        {
            direction = Vector2.up;
        }
        else if (inputY == -1)
        {
            direction = Vector2.down;
        }

        if (inputX == 0 && inputY == 0)
        {
            direction = Vector2.zero;
        }


        rigidBody2D.AddForce(direction * moveSpeed * Time.deltaTime * 100);
    }

    /// <summary>
    /// Permite el paso de la bola por detrás de la pala
    /// </summary>
    private void AllowBallBackCollision() 
    {
        Vector3 ballPosition = GameObject.Find("Ball").transform.position;

        if (isPlayer1)
        {
            if (ballPosition.x < transform.position.x)
            {
                boxCollider.enabled = false;
            }
            else
            {
                boxCollider.enabled = true;
            }
        }
        else
        {
            if (ballPosition.x > transform.position.x)
            {
                boxCollider.enabled = false;
            }
            else
            {
                boxCollider.enabled = true;
            }
        }
    }
}
