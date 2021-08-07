using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    float direction;
    [SerializeField]
    float speedX;
    [SerializeField]
    float speedY;
    bool HitWall = false;
    Rigidbody2D rigidbody;
    Collider2D collider2D;
    [SerializeField]
    GameObject InteractOject;
    [SerializeField]
    float ShootInterval;
    [SerializeField]
    float ShootTimer;
    Vector2 velocity;
    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    public void SetDirection(float _direction)
    {
        InteractOject.SetActive(false);
        collider2D.isTrigger = true;
        velocity = new Vector2(speedX * _direction, 0);   
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = velocity;
    }
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HitWall) return;
        if (!collision.CompareTag("Player"))
        {
            InteractOject.SetActive(true);
            collider2D.isTrigger = false;
            velocity = new Vector2(0, speedY);
            HitWall = true;
        }
    }
}
