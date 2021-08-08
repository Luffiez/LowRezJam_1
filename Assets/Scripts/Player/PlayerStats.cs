using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public int maxBullets;

    private int currentHealth;
    private int currentBullets;

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
        private set 
        { 
            currentBullets = value;
            if (statsUI)
                statsUI.SetPlayerBulletUI(currentBullets, maxBullets);
        }
    }

    PlayerStatsUI statsUI;

    private void Awake()
    {
        statsUI = PlayerStatsUI.instance;

        CurrentBullets = maxBullets;
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int _damage)
    {
        CurrentHealth -= _damage;

        if(CurrentHealth < 0)
        {
            Debug.Log("Player Died.");

            // TODO: reload scene from somewhere else? Play death animation/sound?
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void IncreaseMaxBullets()
    {
        maxBullets++;
        CurrentBullets++;
    }

    public void IncreaseMaxHealth()
    {
        maxHealth++;
        CurrentHealth++;
    }
}
