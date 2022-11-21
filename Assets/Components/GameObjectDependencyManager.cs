
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameObjectDependencyManager : MonoBehaviour
{
    public static GameObjectDependencyManager instance;
    public Dictionary<string, Dictionary<int, GameObject>> gameObjectDictionary = new Dictionary<string, Dictionary<int, GameObject>>();
    public List<GameObject> debugList = new List<GameObject>();
    public Dictionary<string, GameObject> debugDictionary = new Dictionary<string, GameObject>();
    private int identifier = 0;
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
      
        identifier = 0;
        //gameObjectDictionary = new Dictionary<string, Dictionary<int, GameObject>>();
        gameObjectDictionary.Clear();
        debugList = new List<GameObject>();

    }
    
    public void RegisterGameObject(GameObject gameObj)
    {
        debugList.Add(gameObj);
        Dictionary<int, GameObject> tempDict = new Dictionary<int, GameObject>();
        tempDict.Add(identifier, gameObj);
        gameObjectDictionary.Add(gameObj.name, tempDict);
       // identifier++;
        Debug.Log("Registered: "+gameObj.name);
    }

    public GameObject GetGameObject (string gameObjectName)
    {
        return gameObjectDictionary[gameObjectName][0];

    }


}
