// Adds GameObject to GameObjectDependencyManager dictionary. Should be executed just after GameObjectDependencyManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDependencyRegisterComponent : MonoBehaviour
{
    [SerializeField] bool Singleton = false;
    void Awake()
    {
        GameObjectDependencyManager.instance.RegisterGameObject(this.gameObject);
    }

}
