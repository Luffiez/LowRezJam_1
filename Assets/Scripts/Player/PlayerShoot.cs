using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;
    PlayerMovement playerMovement;
    [SerializeField]
    int maxAmmo = 1;
    [SerializeField]
    Transform LeftSpawnPoint;
    [SerializeField]
    Transform RightSpawnPoint;
    [SerializeField]
    float boxCastYSize;
    [SerializeField]
    GameObject bulletPrefab;
    int ammo = 0;
    [SerializeField]
    float shootTime = 0.5f;
    float ShootTimer = 0;
    public void SetMaxAmmo(int _maxAmmo) 
    {
        maxAmmo = _maxAmmo;
        ammo = maxAmmo;
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        ammo = maxAmmo;
    }
    public void Shoot(bool fire)
    {
        ShootTimer -= Time.fixedDeltaTime;
        if (ammo == 0 ||! fire || ShootTimer > 0) return;
        ShootTimer = shootTime;
        bool faceLeft = playerMovement.IsFacingLeft;
        Vector2 spawnPosition = faceLeft ? LeftSpawnPoint.position : RightSpawnPoint.position;
        bool canSpawn = !Physics2D.OverlapBox(spawnPosition, new Vector2(boxCastYSize, boxCastYSize), 0,layerMask);
        GameObject bullet = GameObject.Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        bullet.GetComponent<BulletBehavior>().SetDirection(faceLeft ? -1 : 1);
        //boxcast to see if the player can shoot;
    }

    public void OnDrawGizmos()
    {
        if (playerMovement == null) return;
        bool faceLeft = playerMovement.FacingLeft;
        Vector3 spawnPoint = faceLeft? LeftSpawnPoint.position : RightSpawnPoint.position; 
        Gizmos.DrawCube(spawnPoint, new Vector2(boxCastYSize,boxCastYSize));
    }
}
