using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    private Vector3 pVelocity;
    private double direction;
    private double distance;
    

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

        // ensure a reasonable amount of velocity is occuring before changing direction
        if (abs(pVelocity.x) >= 3 or abs(pVelocity.z) >= 3) 
        {
            // get direction of player velocity
            direction = Math.Atan(-pVelocity.z / -pVelocity.x);

            // use direction and distance to find new camera location
            offset = new Vector3 (
                Convert.ToSingle( Math.Cos(direction) * distance ), 
                offset.y, 
                Convert.ToSingle( Math.Sin(direction) * distance )
            );

        }

        transform.position = player.transform.position + offset;
    }
}
