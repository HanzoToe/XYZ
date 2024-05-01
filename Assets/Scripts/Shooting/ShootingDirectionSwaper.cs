using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDirectionSwaper : MonoBehaviour
{
    public ShotingScript SSPT;
    public ShootingScriptUP SSPTUP;
    public ShootingUPR SSPTUPR;
    public ShootingDOWN SSPTD;
    public ShootingDR SSPTDR;
    public Animator animator; 


    // Enum to represent shooting directions
    public enum ShootingDirection
    {
        Right,
        UpRight,
        Up,
        Down,
        DownRight
    }

    // Current shooting direction
    private ShootingDirection currentDirection = ShootingDirection.Right;

    // Start is called before the first frame update
    void Start()
    {
        // Enable default shooting direction
        SetShootingDirection(currentDirection);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Fire2 button is pressed
        if (Input.GetButtonDown("Fire2"))
        {
            // Cycle to the next shooting direction
            currentDirection = (ShootingDirection)(((int)currentDirection + 1) % 5);
            SetShootingDirection(currentDirection);
        }
    }

    // Function to set shooting direction
    private void SetShootingDirection(ShootingDirection direction)
    {
        // Disable all shooting scripts
        SSPT.enabled = false;
        SSPTUP.enabled = false;
        SSPTUPR.enabled = false;
        SSPTD.enabled = false;
        SSPTDR.enabled = false;

        // Enable the appropriate shooting script based on direction
        switch (direction)
        {
            case ShootingDirection.Right:
                SSPT.enabled = true;
                animator.SetBool("Aiming_Down", false);
                animator.SetBool("Idle", true);
                animator.SetBool("Aiming_Up_Right", false);
                break;
            case ShootingDirection.UpRight:
                SSPTUPR.enabled = true;
                animator.SetBool("Aiming_Down", false);
                animator.SetBool("Idle", false);
                animator.SetBool("Aiming_Up_Right", true);
                break;
            case ShootingDirection.Up:
                SSPTUP.enabled = true;
                break;
            case ShootingDirection.Down:
                SSPTD.enabled = true;
                break;
            case ShootingDirection.DownRight:
                SSPTDR.enabled = true;
                animator.SetBool("Aiming_Down", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Aiming_Up_Right", false);
                break;
        }
    }
}
