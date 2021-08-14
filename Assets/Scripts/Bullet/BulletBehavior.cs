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
    public AudioClip groundHitClip;
    Vector2 velocity;
    Animator animator;
    public float boxCastOffset;
    public float groundCastLength;
    public LayerMask floorMask;
    BoxCollider2D boxCollider;
    bool onGround;
    bool onAcid;
    PlayerStats playerStats;
    SoundManager soundManager;

    // Start is called before the first frame update
    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        soundManager = SoundManager.instance;
    }

    public void SetDirection(float _direction)
    {
        rigidbody.gravityScale = 0;
        ResetBullet();
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
            InteractOject.SetActive(true);
            velocity = new Vector2(0, speedY);
            Invoke("FallDown", 0.5f);
        }
    }
    public void HitAcid()
    {
        Debug.Log("Hit acid!");
        HitWall = false;
        onAcid = true;
        velocity = new Vector2(0, 0);
        boxCollider.isTrigger = false;
        //InteractOject.SetActive(true);
    }

    void HitFloor()
    {
        if(groundHitClip && soundManager)
            soundManager.PlaySfx(groundHitClip);
        hitFloor = true;
        DustManager.instance.SpawnDust(transform.position);
        if(CameraShake.instance)
            CameraShake.instance.TriggerShake(0.1f);
    }

    void FallDown()
    {
        //InteractOject.SetActive(true);
       // velocity = new Vector2(0, speedY);
    }

    public override void ResetEntity()
    {
        Debug.Log("Reset");
        animator.SetBool("Grow", false);
        //rigidbody.gravityScale = 1;
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

    private void ResetBullet()
    {
        onAcid = false;
        hitFloor = false;
        HitWall = false;
        animator.SetBool("Grow", false);
        //rigidbody.gravityScale = 1;
        boxCollider.isTrigger = true;
    }

    private void OnDisable()
    {
        ResetBullet();
        playerStats.CurrentBullets++;
        gameObject.SetActive(false);
    }
}
