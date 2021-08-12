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
    bool onAcid;
    PlayerStats playerStats;

    // Start is called before the first frame update
    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
   
    public void SetDirection(float _direction)
    {
        HitWall = false;
        hitFloor = false;
        boxCollider.isTrigger = true;
        animator.SetBool("Grow", true);
        InteractOject.SetActive(false);
        velocity = new Vector2(speedX * _direction, 0);   
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = velocity;
    }

    private void Update()
    {
        if (onAcid)
            return;

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
        if (HitWall || onAcid) return;
     
        if (!collision.CompareTag("Player"))
        {
            HitWall = true;
            boxCollider.isTrigger = false;
            //animator.SetBool("Grow", true);

            velocity = new Vector2(0, 0);
            Invoke("FallDown", 0.5f);
        }
    }
    public void HitAcid()
    {
        Debug.Log("Hit acid!");
        rigidbody.gravityScale = 0;
        HitWall = false;
        onAcid = true;
        velocity = new Vector2(0, 0);
        boxCollider.isTrigger = false;
        //InteractOject.SetActive(true);
    }

    void HitFloor()
    {
        hitFloor = true;
        DustManager.instance.SpawnDust(transform.position);
        if(CameraShake.instance)
            CameraShake.instance.TriggerShake(0.1f);
    }

    void FallDown()
    {
        InteractOject.SetActive(true);
        velocity = new Vector2(0, speedY);
    }

    public override void ResetEntity()
    {
        Debug.Log("Reset");
        animator.SetBool("Grow", false);
        rigidbody.gravityScale = 1;
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        if (!boxCollider)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + boxCastOffset), new Vector2(boxCollider.size.x * 0.8f, groundCastLength));
    }

    private void OnEnable()
    {
        playerStats.CurrentBullets--;
    }

    private void OnDisable()
    {
        playerStats.CurrentBullets++;
    }
}
