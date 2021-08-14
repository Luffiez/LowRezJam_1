using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;
    PlayerMovement playerMovement;
    PlayerStats playerStats;
    [SerializeField]
    Transform LeftSpawnPoint;
    [SerializeField]
    Transform RightSpawnPoint;
    [SerializeField]
    float boxCastSize;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float shootTime = 0.5f;
    public AudioClip playerShootClip;
    float ShootTimer = 0;
    List<GameObject> bulletList = new List<GameObject>();
    bool holdingFire = false;

    SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.instance;
        playerStats = GetComponent<PlayerStats>();
        playerMovement = GetComponent<PlayerMovement>();

        SpawnMissingBullets();
        playerStats.BulletCountChanged.AddListener(SpawnMissingBullets);
    }

    public void SpawnMissingBullets()
    {
        Debug.Log("Check missing bullets");
        for (int i = bulletList.Count; i < playerStats.maxBullets; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            bulletList.Add(bullet);
            bullet.SetActive(false);
        }
    }

    public void Shoot(bool fire)
    {
        if (!fire)
        {
            holdingFire = false;
        }

        ShootTimer -= Time.fixedDeltaTime;
        if (!fire || holdingFire ) return;
        int bulletIndex = -1;

        for (int i = 0; i < bulletList.Count; i++)
        {
            //Debug.Log(i);
            if (!bulletList[i].activeSelf)
            {
                bulletIndex = i;
                break;
            }
        }
        bool faceLeft = playerMovement.IsFacingLeft;
        Vector2 spawnPosition = faceLeft ? LeftSpawnPoint.position : RightSpawnPoint.position;

        bool canSpawn = !Physics2D.Raycast(transform.position, faceLeft ? Vector2.left : Vector2.right, transform.position.x - LeftSpawnPoint.position.x, layerMask);
        //Debug.Log("shoot");
        if (bulletList.Count <=0  || ShootTimer > 0 || bulletIndex== -1 || !canSpawn) return;
        ShootTimer = shootTime;
        GameObject bullet = bulletList[bulletIndex];
        bullet.SetActive(true);
        bullet.transform.position = spawnPosition;
        bullet.GetComponent<BulletBehavior>().SetDirection(faceLeft ? -1 : 1);
        holdingFire = true;

        if (playerShootClip && soundManager)
            soundManager.PlaySfx(playerShootClip);
    }

    public void DisableBullets() 
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            bulletList[i].SetActive(false);
            bulletList[i].GetComponent<BulletBehavior>().CanSpawn = true;
        }
    } 
}
