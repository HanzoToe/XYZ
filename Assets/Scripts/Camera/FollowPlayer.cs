using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]

    GameObject player;

    public float followspeed = 2f;
    public float playerdelay = -10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetposition = new Vector3(player.transform.position.x, player.transform.position.y, playerdelay);
        transform.position = Vector3.Slerp(transform.position, targetposition, followspeed * Time.deltaTime);
    }
}
