using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeInEnemy : MonoBehaviour
{
    public float damage;
    public float attackDelay;
    public bool attacked;

    public GameObject shooter;

    public GameObject projectile;
    public Transform firePoint;

    private void Start()
    {
        this.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RangeAttack();
        }
    }


    public void RangeAttack()
    {
        if (!attacked)
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);

            attacked = true;
            Invoke(nameof(ResetAttack), attackDelay);
        }
    }

    void ResetAttack()
    {
        attacked = false;
    }
}
