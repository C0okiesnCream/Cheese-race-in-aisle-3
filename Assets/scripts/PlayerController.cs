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

    private float direction = 0;
    private float rotation;
    private float rotationInertia;

    private const float PI = 3.14159f;

    public Rigidbody RB {
        get { return rb; }
    }

    public float Direction {
        get { return direction; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
    }

    void OnMove (InputValue movementVal)
    {
        Vector2 movementVector = movementVal.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

        rotation = movementVector.x/rotationSpeed;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3 (
            Convert.ToSingle( movementY * Math.Sin(direction) + movementX * Math.Cos(direction)), 
            0.0f, 
            Convert.ToSingle( movementY * Math.Cos(direction) + movementX * Math.Sin(direction))
        );
        rb.AddForce(movement * speed);

        rotationInertia += rotation;
        direction += rotationInertia;

        rotationInertia = rotationInertia/rotationAccel;
    }
}
