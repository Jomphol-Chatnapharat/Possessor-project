using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessOnClick : MonoBehaviour
{
    public GameObject PlayerMain;
    public float PossessRange;
    private SimpleEnemy enemy;

    void Update()
    {
        if (PlayerMain.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.transform != null)
                {
                    if (hit.transform.gameObject.tag == "Enemy")
                    {
                        float dist = Vector2.Distance(hit.transform.position, PlayerMain.transform.position);
                        if (dist <= PossessRange)
                        {
                            enemy = hit.transform.gameObject.GetComponent<SimpleEnemy>();
                            PlayerMain.transform.SetParent(hit.transform);
                            enemy.isBeingControl = true;
                            PlayerMain.SetActive(false);
                        }
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerMain.SetActive(true);
            PlayerMain.transform.SetParent(null);
            enemy.isBeingControl = false;
            enemy.playerRb.velocity = Vector2.zero;
        }
    }
}
