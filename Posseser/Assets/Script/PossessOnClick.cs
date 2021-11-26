using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossessOnClick : MonoBehaviour
{
    public GameObject PlayerMain;
    public float PossessRange;
    private SimpleEnemy enemy;
    private Rigidbody2D newRb;
    public GameObject pentagram;

    public Image enemyHp;
    public float maxHp;
    public float currentHp;

    public bool onCoolDown;
    public bool isPossessing = false;
    public bool drained;
    public bool recharged;

    public float coolDownDuration = 3f;
    public float energyUse;
    public float hpDrain;

    void Update()
    {
        if (PlayerMain.activeSelf == true)
        {
            if (PlayerMain.GetComponent<PlayerStats>().currentHp >= energyUse)
            {
                if (!onCoolDown)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                        if (hit.transform != null && hit.transform.gameObject.tag == "Enemy" && !hit.transform.gameObject.GetComponent<Rigidbody2D>())
                        {
                            float dist = Vector2.Distance(hit.transform.position, PlayerMain.transform.position);
                            if (dist <= PossessRange)
                            {


                                isPossessing = true;
                                newRb = hit.transform.gameObject.AddComponent<Rigidbody2D>();
                                newRb.gravityScale = 0;
                                newRb.freezeRotation = true;

                                enemy = hit.transform.gameObject.GetComponent<SimpleEnemy>();

                                pentagram.SetActive(true);
                                pentagram.transform.SetParent(hit.transform);
                                pentagram.transform.position = enemy.transform.position;


                                PlayerMain.transform.position = enemy.transform.position;
                                PlayerMain.transform.SetParent(hit.transform);
                                enemy.isBeingControl = true;
                                PlayerMain.SetActive(false);




                                enemy.tag = "BeingControl";
                                enemy.gameObject.layer = 7;
                                //enemy.GetComponentInChildren<MeleeInEnemy>().enabled = true;
                                enemy.GetComponentInChildren<RangeInEnemy>().enabled = true;
                            }
                        }
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            pentagram.SetActive(false);
            isPossessing = false;
            PlayerMain.SetActive(true);
            PlayerMain.transform.SetParent(null);

            enemy.isBeingControl = false;
            enemy.PlayerOut();
            enemy.playerRb.velocity = Vector2.zero;
            enemy.tag = "Enemy";
            enemy.gameObject.layer = 0;
            //enemy.GetComponentInChildren<MeleeInEnemy>().enabled = false;

            enemy.GetComponentInChildren<RangeInEnemy>().enabled = false;


            Destroy(newRb);

            onCoolDown = true;
            Invoke(nameof(CoolDown), coolDownDuration);
        }

        if (enemy.currentHP <= 10)
        {
            pentagram.SetActive(false);
            isPossessing = false;
            PlayerMain.SetActive(true);
            PlayerMain.transform.SetParent(null);

            enemy.isBeingControl = false;
            enemy.PlayerOut();
            enemy.playerRb.velocity = Vector2.zero;
            enemy.tag = "Enemy";
            enemy.gameObject.layer = 0;

            onCoolDown = true;
            Invoke(nameof(CoolDown), coolDownDuration);
        }

        if (enemy.GetComponent<SimpleEnemy>() != null)
        {
            currentHp = enemy.currentHP;
            maxHp = enemy.maxHP;
        }
        SetHealthImageAmount(currentHp / maxHp);

        if (isPossessing)
        {
            if (!drained)
            {
                enemy.currentHP -= hpDrain;

                drained = true;
                Invoke(nameof(DrainReset), 0.5f);
            }
        }
    }

    void CoolDown()
    {
        onCoolDown = false;
    }

    void DrainReset()
    {
        drained = false;
    }

    public void SetHealthImageAmount(float newAmount)
    {
        enemyHp.fillAmount = newAmount;
    }
}
