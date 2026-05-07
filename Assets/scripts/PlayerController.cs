using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; // rigid body of player
    private float movementX; // movement along X axis
    private float movementY; // movement along Y axis

    public float speed = 0; // speed of player
    public float rotationSpeed = 40; // movement vector is divided by this number to get rotation
    public float rotationAccel = 2; // inertia is divided by this each frame to smooth out the end of rotation
    public float airSpeedMult = 0.2f; // speed multiplier while in air

    private float direction = 0; // direction the camera is facing
    private float rotation; // the value to be added to inertia to change it
    private float rotationInertia; // added directly to direction
    private float distToGround; // distance from middle of the model to the ground, used to ensure the vector that detects the player is grounded is just outside of the model

    public ParticleSystem impactParticles;

    public Rigidbody RB {
        get { return rb; }
    }

    public float Direction {
        get { return direction; }
    }

    // Checks if the player is on the ground and returns a boolean based on the result
    bool IsGrounded() 
    {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1));
    }

    void OnCollisionEnter()
    {
        impactParticles.Play();
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
