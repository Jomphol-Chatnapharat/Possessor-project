using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public float moveSpeed = 1f;
    public float maxSpeed = 5;

    private bool isControlling = true;
    // Start is called before the first frame update
    public void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isControlling = false;
        }

        if (isControlling == true)
        {
            SimpleMovement();
        }
    }

    public void ApplyInput()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
         
        float xForce = xInput * moveSpeed * Time.deltaTime;
        float yForce = yInput * moveSpeed * Time.deltaTime;

        Vector2 force = new Vector2(xForce, yForce);

        playerRb.AddForce(force);
    }

    void SimpleMovement()
    {
        if (playerRb != null)
        {
            ApplyInput();
        }
        else
        {
            Debug.Log("no playerRB");
        }
        //Debug.Log(playerRb.velocity.magnitude);
        playerRb.velocity = Vector2.ClampMagnitude(playerRb.velocity, maxSpeed);

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            playerRb.velocity = Vector2.zero;
        }
    }
}

