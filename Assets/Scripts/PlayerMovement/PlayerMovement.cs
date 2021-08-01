using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//add mechaninc to buffer jump when close to ground
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
    [SerializeField]
    float cayoteTime = 0.1f;
    float cayoteTimer = 0.0f;
    [SerializeField]
    LayerMask layerMask;
    BoxCollider2D boxCollider;
    [SerializeField]
    float groundCastLength = 1f;
    [SerializeField]
    float boxCastOffset = 0.5f;

    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
    }

    public void MoveY(bool jump = false)
    {
        gravity = ((-2 * JumpHeight) / (JumpTime * JumpTime));
        bool onGround = Physics2D.BoxCast(new Vector3(transform.position.x, transform.position.y + boxCastOffset), new Vector2(boxCollider.size.x, groundCastLength), 0, Vector2.down, layerMask);
        if (onGround)
        {
            cayoteTimer = cayoteTime;
        }
        else
        {
            cayoteTimer -= Time.deltaTime;
        }
        if (jump == true && cayoteTimer >0)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, (2 * JumpHeight) / JumpTime);
        }
        else
        {
            if (rigidbody.velocity.y < 0)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y + (gravity * fallMultiplier * Time.fixedDeltaTime));
            }
            else
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y + (gravity * Time.fixedDeltaTime));
            }
        }
        
    }

    public void MoveX(float direction)
    {
        rigidbody.velocity = new Vector2(VelocityX * direction, rigidbody.velocity.y);
    }
}
