using UnityEngine;

public class Goalkeeper : MonoBehaviour
{

    public Rigidbody2D rigidBody2D;
    public bool isPlayer1;
    private float inputY;
    public float moveSpeed = 100;
    private Vector2 direction;

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (isPlayer1)
        {
            inputY = Input.GetAxisRaw("Vertical");
        }
        else
        {
            inputY = Input.GetAxisRaw("Vertical2");
        }

        if (inputY == 1)
        {
            direction = Vector2.up;
        }
        else if (inputY == -1)
        {
            direction = Vector2.down;
        }

        if (inputY == 0)
        {
            direction = Vector2.zero;
        }


        rigidBody2D.AddForce(direction * moveSpeed * Time.deltaTime * 100);
    }

}
