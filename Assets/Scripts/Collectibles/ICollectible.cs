using System.Collections.Generic;
using UnityEngine;

public interface ICollectible
{
    public List<GameObject> CollectibleList { set; }
    void ReturnToList();
}