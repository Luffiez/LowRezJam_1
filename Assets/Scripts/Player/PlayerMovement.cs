using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//add mechaninc to buffer jump when close to ground
public class PlayerMovement : MonoBehaviour
{
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
    LayerMask groundMask;
    BoxCollider2D boxCollider;
    [SerializeField]
    float groundCastLength = 1f;
    [SerializeField]
    float boxCastOffset = 0.5f;
    [SerializeField]
    bool onGround;
    [SerializeField]
    AudioClip jumpSound;
    bool holdJump = false;
    bool isFacingLeft = false;

    float jumpTimer = 0;

    public bool FacingLeft { get { return IsFacingLeft; } }

    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    public bool IsFacingLeft { get => isFacingLeft; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    public void MoveY(bool jump = false)
    {
        if (jumpTimer > 0)
            jumpTimer -= Time.deltaTime;

        if (holdJump == true && jump == false)
        {
            holdJump = false;
        }

        gravity = ((-2 * JumpHeight) / (JumpTime * JumpTime));
        onGround = Physics2D.BoxCast(new Vector3(transform.position.x, transform.position.y + boxCastOffset), new Vector2(boxCollider.size.x * 0.8f, groundCastLength), 0, Vector2.down, groundCastLength,groundMask);
        if (Jumping == true && onGround && jumpTimer <= 0)
        {
            Jumping = false;
        }
        else if (Jumping && jump == false && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        if (onGround)
        {
            cayoteTimer = cayoteTime;
        }
        else
        {
            cayoteTimer -= Time.deltaTime;
        }
        if (jump == true && cayoteTimer > 0 && Jumping == false && !holdJump)
        {
            holdJump = true;
            jumpTimer = 0.3f;
            Jumping = true;
            rb.velocity = new Vector2(rb.velocity.x, (2 * JumpHeight) / JumpTime);
            SoundManager.instance.PlaySfx(jumpSound);
        }
        else
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (gravity * fallMultiplier * Time.fixedDeltaTime));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (gravity * Time.fixedDeltaTime));
            }
        }

        Debug.Log("OnGround: " + onGround);
        Debug.Log("Jumpin: " + Jumping);
        anim.SetBool("Jump", Jumping);
    }

    public void MoveX(float direction)
    {
        rb.velocity = new Vector2(VelocityX * direction, rb.velocity.y);
        anim.SetInteger("Speed", Mathf.RoundToInt(rb.velocity.x));
        if (direction > 0 && IsFacingLeft ||
            direction < 0 && !IsFacingLeft)
            FlipX();
    }

    void FlipX()
    {
        isFacingLeft = !isFacingLeft;
        spriteRenderer.flipX = isFacingLeft;
    }

    private void OnDrawGizmosSelected()
    {
        if (!boxCollider)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + boxCastOffset), new Vector2(boxCollider.size.x * 0.8f, groundCastLength));
    }
}
