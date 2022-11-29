// Adds GameObject to GameObjectDependencyManager dictionary. Should be executed just after GameObjectDependencyManager
// All GameObjects which use GameObjectDependencyManager need to be tagged!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDependencyRegisterComponent : MonoBehaviour
{
    [SerializeField] bool Singleton = false;
    [SerializeField] bool UnregisterOnDeath = false;
    IDamageable iDamageable;
    void Awake()
    {
        GameObjectDependencyManager.instance.RegisterGameObject(this.gameObject);
    }

    private void Start()
    {
        iDamageable = GetComponent<IDamageable>();
        if(iDamageable != null)
        {
            iDamageable.EventDead += IsDead;
        }
    }
    // This sucks, I need an event system

    void IsDead()
    {
        if (iDamageable != null)
        {
            iDamageable.EventDead -= IsDead;
        }
        Debug.Log("död");
        if(UnregisterOnDeath) GameObjectDependencyManager.instance.UnRegisterGameObject(this.gameObject);
    }
}
