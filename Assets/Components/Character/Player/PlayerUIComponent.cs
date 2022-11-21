using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIComponent : MonoBehaviour
{
    HealthComponent healthComponent;
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObjectDependencyManager.instance.GetGameObject("GameManager").GetComponent<GameManager>();
        healthComponent = GetComponent<HealthComponent>();
        gameManager.health = healthComponent.health;
    }
    void OnDamage()
    {
        gameManager.health = healthComponent.health;
        if(healthComponent.health <= 0)
        {
            gameManager.gameOver = true;
        }
        gameManager.UpdateUI();
    }
}
