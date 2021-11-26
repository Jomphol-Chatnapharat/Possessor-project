using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float bulletLife = 5f;
    public float bulletDamage = 5f;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<SimpleEnemy>().currentHP -= bulletDamage;
            Destroy(this.gameObject);
        }
    }
}
