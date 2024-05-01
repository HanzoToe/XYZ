using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingUPR : MonoBehaviour
{
    // Bools
    private bool allowedToShootWhileWalking = true;
    private bool isSliding;
    private bool isWallSliding;
    private bool isWallJumping;
    private bool allowedToShoot = true;

    // Floats
    public float shootCooldown = 0.2f;

    // References
    public PlayerMovement pm;
    public GameObject bulletPrefab;

    private void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        // Update player movement state
        isSliding = pm.isSliding;
        isWallSliding = pm.isWallSliding;
        isWallJumping = pm.isWalljumping;


        // Check if shooting is allowed
        UpdateShootingState();

        // Check for shooting input
        if (Input.GetButton("Fire1") && allowedToShoot)
        {
            Shoot();
        }
    }

    private void UpdateShootingState()
    {
        // Shooting is not allowed while sliding, wall sliding, or wall jumping
        allowedToShootWhileWalking = !(isSliding || isWallJumping || isWallSliding);
    }

    private void Shoot()
    {
        if (allowedToShootWhileWalking)
        {
            StartCoroutine(ShootingCoroutine());
        }
    }

    private IEnumerator ShootingCoroutine()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        allowedToShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        allowedToShoot = true;
    }
}
