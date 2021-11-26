using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public EnemyAI AI;
    public float moveSpeed = 1f;
    public float maxSpeed = 5;

    public bool isBeingControl = false;
    public bool isStun;
    public float stunTime;

    public float maxHP;
    public float currentHP;

    //private GameObject PlayerMain;


    // Start is called before the first frame update
    //public void Awake()
    //{
    //    playerRb = GetComponent<Rigidbody2D>();
    //}

    private void Start()
    {
            AI = GetComponent<EnemyAI>();

        currentHP = maxHP;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isBeingControl = true;
        }

        if (isBeingControl == true)
        {
            playerRb = GetComponent<Rigidbody2D>();
            AI.enabled = false;
            SimpleMovement();
        }

        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayerOut()
    {
        if (!isStun)
        {
            isStun = true;
            transform.position = transform.position;
            Invoke(nameof(ResetStun), stunTime);
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

    void ResetStun()
    {
        Debug.Log("reset stun");

        isStun = false;
        AI.enabled = true;
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

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
