using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float damage;
    public bool isDamaged;
    public float resetTime;

    public GameObject possessedEnemy;

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "BeingControl")
        {
            possessedEnemy = other.gameObject;
            TakingDamage();
        }
    }

    void TakingDamage()
    {

        if (!isDamaged)
        {
            possessedEnemy.GetComponent<SimpleEnemy>().currentHP -= damage;
            isDamaged = true;
            Invoke(nameof(DamageReset), resetTime);
        }
    }

    void DamageReset()
    {
        isDamaged = false;
    }
}
