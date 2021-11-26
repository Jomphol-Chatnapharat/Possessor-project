using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossTrap : MonoBehaviour
{
    public float damage;
    public bool isDamaged;
    public float resetTime;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            TakingDamage();
        }
    }

    void TakingDamage()
    {

        if (!isDamaged)
        { 
            GameObject player = GameObject.Find("Player");

            player.GetComponent<PlayerStats>().currentHp -= damage;
            isDamaged = true;
            Invoke(nameof(DamageReset), resetTime);
        }
    }

    void DamageReset()
    {
        isDamaged = false;
    }
}
