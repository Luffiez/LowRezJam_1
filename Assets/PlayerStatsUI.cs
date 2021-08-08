using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    public static PlayerStatsUI instance = null;

    public TMP_Text healthText;
    public TMP_Text bulletsText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SetPlayerHealthUI(int current, int max)
    {
        healthText.text = current + "/" + max;
    }

    public void SetPlayerBulletUI(int current, int max)
    {
        bulletsText.text = current + "/" + max;
    }
}
