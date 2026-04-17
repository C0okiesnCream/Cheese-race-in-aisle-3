using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; // rigid body of player
    private float movementX; // movement along X axis
    private float movementY; // movement along Y axis

    public float speed = 0; // speed of player
    public int winScore = 1; // number of collectibles required to win

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
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }
}
