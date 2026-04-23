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
        offset = new Quaternion ( 0, 1f, 0, direction) * new Vector3 (
            distance, 
            offset.y, 
            distance
        );

        transform.forward = player.transform.position - transform.position;


        transform.position = player.transform.position + offset;
    }
}
