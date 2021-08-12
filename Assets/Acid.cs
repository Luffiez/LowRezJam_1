using UnityEngine;

public class Acid : MonoBehaviour
{
    public float damageTickTimer = 1f;
    PlayerStats playerStats;

    float timer = 0;

    void Update()
    {
        if(playerStats)
        {
            timer += Time.deltaTime;
            if(timer >= damageTickTimer)
            {
                playerStats.TakeDamage(1);
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player in acid!");
            playerStats = collision.GetComponent<PlayerStats>();
        }
        else if(collision.GetComponent<BulletBehavior>())
        {
            collision.GetComponent<BulletBehavior>().HitAcid();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player out of acid!");
            playerStats = null;
            timer = 0;
        }
    }
}
