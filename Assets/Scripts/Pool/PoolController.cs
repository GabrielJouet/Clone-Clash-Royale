using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle pools and pool related behavior.
/// </summary>
public class PoolController : MonoBehaviour
{
    /// <summary>
    /// All pools available.
    /// </summary>
    private readonly Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();



    /// <summary>
    /// Method called to put an object inside the pool.
    /// </summary>
    /// <param name="stored">The object stored in a game object object format.</param>
    public void In(GameObject stored)
    {
        _pools[stored.name].In(stored);
    }


    /// <summary>
    /// Method called to retrieve an object from the pool.
    /// </summary>
    /// <param name="wanted">The object we want to retrieve</param>
    /// <returns>Returns the object retrieved or instantiated</returns>
    public GameObject Out(GameObject wanted)
    {
        if (_pools.ContainsKey(wanted.name))
            return _pools[wanted.name].Out();

        Pool newPool = new GameObject(wanted.name + " Pool").AddComponent<Pool>();
        newPool.transform.parent = transform;
        newPool.Class = wanted;
        _pools.Add(wanted.name, newPool);

        return newPool.Out();
    }
}