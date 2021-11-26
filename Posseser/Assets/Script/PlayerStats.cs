using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    public Image hpBar;

    public float maxHp;
    public float currentHp;
    public GameObject Player;

    public float energyRecharge;




    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= 0)
        {
            Destroy(Player.gameObject);
        }

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        SetHealthImageAmount(currentHp / maxHp);
    }

    void RechargeEnergy()
    {
        currentHp += energyRecharge;

    }

    public void SetHealthImageAmount(float newAmount)
    {
        hpBar.fillAmount = newAmount;
    }
}
