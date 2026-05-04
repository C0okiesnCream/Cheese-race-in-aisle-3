using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float distance;

    private Vector3 offset;
    private Vector3 pVelocity;
    private float direction;

    private const double PI = 3.1415926535897931f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position;
        distance = offset.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // get player velocity
        pVelocity = player.GetComponent<PlayerController>().RB.linearVelocity;

        // get direction of player
        direction = player.GetComponent<PlayerController>().Direction;

        // use direction and distance to find new camera location
        offset = new Vector3 (
            Convert.ToSingle( distance * Math.Sin(direction) ),
            offset.y, 
            Convert.ToSingle( distance * Math.Cos(direction) )
        );

        transform.forward = player.transform.position - transform.position;

        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, 0.05f);
    }
}
