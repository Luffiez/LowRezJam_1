using UnityEngine;

public class Enemy : Entity
{
    public int maxHealth;
    public int damage;
    public AudioClip enemyTakeDamageClip;
    int currentHealth;

    SoundManager soundManager;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        soundManager = SoundManager.instance;
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;

        if (enemyTakeDamageClip && soundManager)
            soundManager.PlaySfx(enemyTakeDamageClip);

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
        else if(collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }

    public override void ResetEntity()
    {
        base.ResetEntity();
        currentHealth = maxHealth;
        gameObject.SetActive(true);
    }
}
