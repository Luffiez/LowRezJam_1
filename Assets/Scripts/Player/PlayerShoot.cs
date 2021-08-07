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
    int ammo = 0;
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
        if (ammo == 0 ||! fire) return;
        bool faceLeft = playerMovement.IsFacingLeft;
        Vector2 spawnPosition = faceLeft ? LeftSpawnPoint.position : RightSpawnPoint.position;
        bool canSpawn = !Physics2D.OverlapBox(spawnPosition, new Vector2(boxCastYSize, boxCastYSize), 0,layerMask);
        Debug.Log(canSpawn);
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
