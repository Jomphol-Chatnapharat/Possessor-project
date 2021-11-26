using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeInEnemy : MonoBehaviour
{
    public float damage;
    public float attackDelay;
    public bool attacked;

    public GameObject swingEffect;
    public bool isSwing;
    public float swingDelay;

    private void Start()
    {
        this.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (other.gameObject.tag == "Enemy")
                {
               
                    other.gameObject.GetComponent<SimpleEnemy>().currentHP -= damage;
                    attacked = true;

                    swingEffect.SetActive(true);
                    //Invoke(nameof(ResetAttack), attackDelay);
                }
            }
    }

    void ResetAttack()
    {
        swingEffect.SetActive(false);
        attacked = false;
    }
}
