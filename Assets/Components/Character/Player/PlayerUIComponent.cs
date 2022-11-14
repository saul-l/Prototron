using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIComponent : MonoBehaviour
{
    HealthComponent healthComponent;

    void Start()
    {    
        healthComponent = GetComponent<HealthComponent>();
        GameManager.instance.health = healthComponent.health;       
    }
    void OnDamage()
    {
        GameManager.instance.health = healthComponent.health;
        if(healthComponent.health <= 0)
        {
            GameManager.instance.gameOver = true;
        }
        GameManager.instance.UpdateUI();
    }
}
