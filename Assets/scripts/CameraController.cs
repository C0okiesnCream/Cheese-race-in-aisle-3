using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float targetDistance = 7;

    private Vector3 offset;
    private Vector3 pVelocity;
    private float direction;
    private Vector2 currentDistance;

    private const double PI = 3.1415926535897931f;

    public float Direction {
        get { return direction; }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // get player velocity
        pVelocity = player.GetComponent<PlayerController>().RB.linearVelocity;

        // get distance from player
        currentDistance = new Vector2 (
            player.transform.x - transform.x,
            player.transform.z - transform.z
        );

        // find direction in radians through (playerZ - cameraZ) / (playerX - cameraX)
        direction = Math.Atan(
            (currentDistance.y) /
            (currentDistance.x)
        );

        // use direction and targetDistance to find new camera location
        offset = new Vector3 (
            Convert.ToSingle( targetDistance * Math.Sin(direction) ),
            offset.y, 
            Convert.ToSingle( targetDistance * Math.Cos(direction) )
        );

        transform.forward = player.transform.position - transform.position;

        if ()
        {
            transform.position = player.transform.position + offset;
        }
    }
}
