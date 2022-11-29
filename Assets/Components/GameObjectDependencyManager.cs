// GameObjectDependencyManager handles game object dependencies
// 
// - RegisterGameObject adds object to dictionary based on it's name
// (Maybe I should actually use tags.)
// - GetGameObject returns first entry with matching key from dictionary
// - GetGameObjects returns array of objects, which match the key
// - ResetGameObjectDictionary resets the dictionary and should be called on scene changes
//
// Todo:
// - GameObjects are given unique identifier when registered, so it's possible to also
//   remove them from the list. Functionality for this should be finished.

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Linq;

public class GameObjectDependencyManager : MonoBehaviour
{
    public static GameObjectDependencyManager instance;
    public Dictionary<string, List<GameObject>> gameObjectDictionary = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, GameObject> debugDictionary = new Dictionary<string, GameObject>();
    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void ResetGameObjectDictionary()
    {
        gameObjectDictionary.Clear();
    }
    
    public void RegisterGameObject(GameObject gameObj)
    {
        if(gameObjectDictionary.ContainsKey(gameObj.tag))
        {
            gameObjectDictionary[gameObj.tag].Add(gameObj);     
        }
        else
        {
            List<GameObject> tempList = new List<GameObject>();
            tempList.Add(gameObj);
            gameObjectDictionary.Add(gameObj.tag, tempList);
        }
    }

    public void UnRegisterGameObject(GameObject gameObj)
    {
        foreach( GameObject tmpGameObj in gameObjectDictionary[gameObj.tag])
        {
            if(tmpGameObj == gameObj)
            {
                gameObjectDictionary[gameObj.tag].Remove(tmpGameObj);
                break;
            }
        }
    }

    public GameObject GetGameObject (string gameObjectName)
    {
        if (gameObjectDictionary.ContainsKey(gameObjectName))
        {
            return gameObjectDictionary[gameObjectName].First();
        }
        else
        {
            return null;
        }
    }

    // Returns list of GameObjects, which match gameObjectName
    public List<GameObject> GetGameObjects(string gameObjectName)
    {
        if (gameObjectDictionary.ContainsKey(gameObjectName))
        {
            return gameObjectDictionary[gameObjectName];
        }
        else
        {
            return null;
        }
    }
}
