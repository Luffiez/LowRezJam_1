using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public int maxBullets;
    public AudioClip playerDamageClip;
    public AudioClip playerDieClip;


    private int currentHealth;
    private int currentBullets;

    public UnityEvent BulletCountChanged = new UnityEvent();
    public UnityEvent HealthCountChanged = new UnityEvent();

    Animator anim;
    SoundManager soundManager;
    PlayerShoot playerShoot;
    public int CurrentHealth 
    { 
        get => currentHealth;
        private set
        {
            currentHealth = value;
            if (statsUI)
                statsUI.SetPlayerHealthUI(currentHealth, maxHealth);
        }
    }

    public int CurrentBullets 
    { 
        get => currentBullets; 
        set 
        { 
            currentBullets = value;
            if (statsUI)
                statsUI.SetPlayerBulletUI(currentBullets, maxBullets);
        }
    }

    PlayerStatsUI statsUI;

    private void Start()
    {
        soundManager = SoundManager.instance;
        anim = GetComponent<Animator>();
        statsUI = PlayerStatsUI.instance;
        playerShoot = GetComponent<PlayerShoot>();
        ResetStats();
    }

    public void TakeDamage(int _damage)
    {
        CurrentHealth -= _damage;
        CameraShake.instance.TriggerShake();
        anim.SetTrigger("TakeDamage");
        if(CurrentHealth <= 0)
        {
            Debug.Log("Player Died.");
            RoomManager.instance.currentRoom.Reset();
            ResetStats();
            playerShoot.DisableBullets();
            if (playerDieClip && soundManager)
                soundManager.PlaySfx(playerDieClip);
        }
        else if (playerDamageClip && soundManager)
        {
            soundManager.PlaySfx(playerDamageClip);
        }
    }

    public void ResetStats()
    {
        CurrentHealth = maxHealth;
        CurrentBullets = maxBullets;
    }

    public void IncreaseMaxBullets()
    {
        maxBullets++;
        CurrentBullets++;
        if (BulletCountChanged != null)
            BulletCountChanged.Invoke();
    }

    public void IncreaseMaxHealth()
    {
        maxHealth++;
        CurrentHealth++;

        if (HealthCountChanged != null)
            HealthCountChanged.Invoke();
    }
}
