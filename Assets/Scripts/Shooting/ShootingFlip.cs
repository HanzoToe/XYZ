using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingFlip : MonoBehaviour
{
    public PlayerMovement pm;
    private float horizontal;
    private bool isFacingRight = true;



    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = pm.horizontal;

        Flip();
    }


    private void Flip()
    {
        // Flip player sprite based on movement direction
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
