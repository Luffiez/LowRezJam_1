using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;
    PlayerMovement playerMovement;
    [SerializeField]
    int startAmmo = 2;
    [SerializeField]
    Transform LeftSpawnPoint;
    [SerializeField]
    Transform RightSpawnPoint;
    [SerializeField]
    float boxCastYSize;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float shootTime = 0.5f;
    float ShootTimer = 0;
    List<GameObject> bulletList = new List<GameObject>();
    bool holdingFire = false; 


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        for (int i = 0; i < startAmmo; i++)
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
        //Debug.Log("shoot");
        if (bulletList.Count <=0  || ShootTimer > 0 || bulletIndex== -1) return;
        ShootTimer = shootTime;
        bool faceLeft = playerMovement.IsFacingLeft;
        Vector2 spawnPosition = faceLeft ? LeftSpawnPoint.position : RightSpawnPoint.position;
        bool canSpawn = !Physics2D.OverlapBox(spawnPosition, new Vector2(boxCastYSize, boxCastYSize), 0,layerMask);
        GameObject bullet = bulletList[bulletIndex];
        bullet.SetActive(true);
        bullet.transform.position = spawnPosition;
        bullet.GetComponent<BulletBehavior>().SetDirection(faceLeft ? -1 : 1);
        holdingFire = true;
        //boxcast to see if the player can shoot;
    }

    public void DisableBullets() 
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            bulletList[i].SetActive(false);
        }
    } 
    public void OnDrawGizmos()
    {
        if (playerMovement == null) return;
        bool faceLeft = playerMovement.FacingLeft;
        Vector3 spawnPoint = faceLeft? LeftSpawnPoint.position : RightSpawnPoint.position; 
        Gizmos.DrawCube(spawnPoint, new Vector2(boxCastYSize,boxCastYSize));
    }
}
