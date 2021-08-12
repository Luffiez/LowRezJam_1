using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : Entity
{
    float direction;
    [SerializeField]
    float speedX;
    [SerializeField]
    float speedY;
    bool HitWall = false;
    bool hitFloor = false;
    Rigidbody2D rigidbody;
    [SerializeField]
    GameObject InteractOject;
    Vector2 velocity;
    Animator animator;
    public float boxCastOffset;
    public float groundCastLength;
    public LayerMask floorMask;
    BoxCollider2D boxCollider;
    bool onGround;

    // Start is called before the first frame update
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
   
    public void SetDirection(float _direction)
    {
        HitWall = false;
        hitFloor = false;
        boxCollider.isTrigger = true;
        animator.SetBool("Grow", false);
        InteractOject.SetActive(false);
        velocity = new Vector2(speedX * _direction, 0);   
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = velocity;
   

    }

    private void Update()
    {
        onGround = Physics2D.BoxCast(new Vector3(transform.position.x, transform.position.y + boxCastOffset), new Vector2(boxCollider.size.x * 0.8f, groundCastLength), 0, Vector2.down, 0, floorMask);

        if (!hitFloor && HitWall && onGround)
        {
            HitFloor();
        }
        else if (hitFloor && !onGround)
        {
            hitFloor = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HitWall) return;
        if (!collision.CompareTag("Player"))
        {
            HitWall = true;
            boxCollider.isTrigger = false;
            animator.SetBool("Grow", true);

            velocity = new Vector2(0, 0);
            Invoke("FallDown", 0.5f);
        }
    }

    void HitFloor()
    {
        hitFloor = true;
        DustManager.instance.SpawnDust(transform.position);
    }

    void FallDown()
    {
        InteractOject.SetActive(true);
        velocity = new Vector2(0, speedY);
    }

    public override void ResetEntity()
    {
        Debug.Log("Reset");
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        if (!boxCollider)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + boxCastOffset), new Vector2(boxCollider.size.x * 0.8f, groundCastLength));
    }
}
