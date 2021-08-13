using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    public float timeUntilPush = 1f;
    public float moveSpeed = 5f;
    public LayerMask blockMask;
    float timer = 0;
    float pushDirection;
    bool playerIsPushing = false;
    bool moveToTarget = false;
    Rigidbody2D rb;
    Vector2 targetPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        moveToTarget = false;
        playerIsPushing = false;
        timer = 0;
    }

    private void Update()
    {
        if (playerIsPushing && !DirectionIsBlocked(pushDirection))
        {
            timer += Time.deltaTime;
            if(timer >= timeUntilPush)
            {
                timer = 0;
                moveToTarget = true;
                targetPos = transform.position + Vector3.right * pushDirection;
            }
        }

        if(moveToTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPos) < 0.01f)
            {
                moveToTarget = false;
            }
        }
    }

    public void Push()
    {
        Debug.Log("push!");
        timer = 0;
        rb.AddForce(new Vector2(pushDirection,0) * moveSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            playerIsPushing = true;

            if (collision.transform.position.x > transform.position.x)
                pushDirection = -1;
            else
                pushDirection = 1;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerIsPushing = false;
            timer = 0;
        }
    }

    bool DirectionIsBlocked(float direction)
    {
        return Physics.Linecast(transform.position, transform.position + (Vector3.right * direction), blockMask);
    } 
}
