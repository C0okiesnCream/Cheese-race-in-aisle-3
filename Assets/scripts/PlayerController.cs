using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; // rigid body of player
    private float movementX; // movement along X axis
    private float movementY; // movement along Y axis

    public float speed = 0; // speed of player
    public float rotationSpeed = 1;
    public float rotationAccel = 1;
    public float airSpeedMult = 0.2f;

    private float direction = 0;
    private float rotation;
    private float rotationInertia;
    private float distToGround;

    private const float PI = 3.14159f;

    public Rigidbody RB {
        get { return rb; }
    }

    public float Direction {
        get { return direction; }
    }

    bool IsGrounded() 
    {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    void OnMove (InputValue movementVal)
    {
        Vector2 movementVector = movementVal.Get<Vector2>();

        movementY = movementVector.y;

        rotation = movementVector.x/rotationSpeed;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3 (
            Convert.ToSingle( movementY * Math.Sin(direction)), 
            0.0f, 
            Convert.ToSingle( movementY * Math.Cos(direction))
        );

        if (IsGrounded())
        {
            rb.AddForce(movement * speed);
        }
        else
        {
            rb.AddForce(movement * speed * airSpeedMult);
        }

        rotationInertia += rotation;
        direction += rotationInertia;

        rotationInertia = rotationInertia/rotationAccel;
    }
}
