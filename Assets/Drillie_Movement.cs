using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Drillie_Movement : MonoBehaviour
{
    public GameObject PointB;
    public GameObject PointA;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PointB.transform; 
    }

    private void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        
        if(currentPoint == PointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0); 
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == PointB.transform)
        {
            Flip();
            currentPoint = PointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == PointA.transform)
        {
            Flip();
            currentPoint = PointB.transform;
        }


        
    }
    
    private void Flip()
    {
        Vector3 localescale = transform.localScale;
        localescale.x *= -1;
        transform.localScale = localescale;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);  
    }

}
