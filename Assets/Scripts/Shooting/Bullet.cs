using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float AirTime = 1f; 
    float speed = 20f;
    Rigidbody2D rb;

    public static event Action<Transform> BulletSpawned;
    public static event Action BulletDestroyed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        BulletSpawned?.Invoke(transform);

    }

    private void OnDestroy()
    {
        // Trigger the bullet destroyed event when the bullet is destroyed
        BulletDestroyed?.Invoke();
    }

    private void Update()
    {
        AirTime -= Time.deltaTime;

        if (AirTime <= 0f) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitinfo)
    {
        Debug.Log(hitinfo.name);
        Destroy(gameObject); 
    }
}
