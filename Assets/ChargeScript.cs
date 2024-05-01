using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeScript : MonoBehaviour
{
    public float ViewDistance = 3f;

    public LayerMask BulletLayer;

    public Transform player;
    public Transform bullet;

    public Drillie_Movement DM;

    private Rigidbody2D rb;

    Vector2 Drilling;
    bool chasing = false;

    public float speed = 4f;

    ChargeScript ChargeScri;



    // Start is called before the first frame update
    void Start()
    {
        DM = gameObject.GetComponent<Drillie_Movement>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        ChargeScri = gameObject.GetComponent<ChargeScript>();
        Bullet.BulletSpawned += OnBulletSpawned;
        Bullet.BulletDestroyed += OnBulletDestroyed;

    }

    private void OnDestroy()
    {
        // Unsubscribe from the events to prevent memory leaks
        Bullet.BulletSpawned -= OnBulletSpawned;
        Bullet.BulletDestroyed -= OnBulletDestroyed;
    }

    private void OnBulletSpawned(Transform bulletTransform)
    {
        bullet = bulletTransform;
    }

    private void OnBulletDestroyed()
    {
        bullet = null;
      
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (bullet != null)
        {
            float distanceToBullet = Vector2.Distance(transform.position, bullet.position);

            if (distanceToBullet <= ViewDistance)
            {
                SpotBullet();
            }
        }
    }


    private void FixedUpdate()
    {
        if (chasing) // If chasing is true, constantly update the direction towards the player
        {
            Drilling = new Vector2(player.transform.position.x, rb.velocity.y) - new Vector2(transform.position.x, rb.velocity.y);
            rb.velocity = Drilling.normalized * speed; // Adjust speed based on your Drillie_Movement script

            if (transform.position.x > player.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (transform.position.x < player.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }


    private void SpotBullet()
    {
        Vector2 raydirectionBullet = bullet.position - transform.position;

        RaycastHit2D hitBullet = Physics2D.Raycast(transform.position, raydirectionBullet.normalized, ViewDistance, BulletLayer);



        if (hitBullet.collider != null && hitBullet.collider.CompareTag("Bullet"))
        {
            DM.enabled = false;
            chasing = true;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ViewDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Ground") || !collision.collider.CompareTag("Enemy") && chasing)
        {
            ChargeScri.enabled = false;
            DM.enabled = true;

            if (transform.localScale == new Vector3(-1, 1, 1))
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (transform.localScale == new Vector3(1, 1, 1))
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
