using UnityEngine;

public class Enemy : Entity
{
    public int maxHealth;
    public int damage;
    int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        if (currentHealth <= 0)
            gameObject.SetActive(false);
    }

    protected virtual void DealDamage(PlayerStats playerStats)
    {
        playerStats.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            DealDamage(collision.GetComponent<PlayerStats>());
        }
    }

    public override void ResetEntity()
    {
        base.ResetEntity();
        currentHealth = maxHealth;
        gameObject.SetActive(true);
    }
}
