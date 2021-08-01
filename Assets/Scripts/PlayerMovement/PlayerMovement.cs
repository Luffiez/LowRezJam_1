using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    bool OnGround = false;
    bool Jumping = false;
    [SerializeField]
    float JumpHeight = 0.0f;
    [SerializeField]
    float JumpTime = 0.0f;
    float VelocityX;
    float gravity;

    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        gravity = ((-2* JumpHeight)/(JumpTime*JumpTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveX(int direction)
    {
        if (direction == 0)
        {
            rigidbody.velocity = new Vector2(-VelocityX, rigidbody.velocity.y);
        }
        else if (direction == 1)
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
        else if (direction == 2)
        {
            rigidbody.velocity = new Vector2(VelocityX, rigidbody.velocity.y);
        }
    }

    public void Jump()
    {
        rigidbody.velocity = new Vector2( rigidbody.velocity.x,(2 * JumpHeight) / JumpTime);
    }
}
