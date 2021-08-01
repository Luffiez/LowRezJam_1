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
    [SerializeField]
    float VelocityX = 1f;
    float gravity = 0f;
    [SerializeField]
    float fallMultiplier = 0f;

    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        gravity = ((-2 * JumpHeight) / (JumpTime * JumpTime));
        if (Input.GetKey(KeyCode.A))
        {
           
            MoveX(0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveX(2);
        }
        else
        {
          
            MoveX(1);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            MoveY(true);
        }
        else
        {
            MoveY(false);
        }
    }

    public void MoveY(bool jump = false)
    {
        if (jump == true)
        {
            Debug.Log("jump");
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, (2 * JumpHeight) / JumpTime);
        }
        else
        {
            Debug.Log("fall");
            if (rigidbody.velocity.y < 0)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y + (gravity * fallMultiplier * Time.deltaTime));
            }
            else
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y + (gravity * Time.deltaTime));
            }
        }
        
    }

    public void MoveX(int direction)
    {
        if (direction == 0)
        {
            Debug.Log("moveLeft");
            rigidbody.velocity = new Vector2(-VelocityX, rigidbody.velocity.y);
        }
        else if (direction == 1)
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
        else if (direction == 2)
        {
            Debug.Log("MoveRight");
            rigidbody.velocity = new Vector2(VelocityX, rigidbody.velocity.y);
        }
    }
}
