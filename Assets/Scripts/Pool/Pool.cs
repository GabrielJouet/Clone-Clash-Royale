using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to instantiate poolable object and store them.
/// </summary>
public class Pool : MonoBehaviour
{
    /// <summary>
    /// The unique object handled by this pool.
    /// </summary>
    public GameObject Class { get; set; }


    /// <summary>
    /// All items inside this pool, desactivated and waiting.
    /// </summary>
    private readonly Stack<GameObject> _itemPool = new Stack<GameObject>();



    /// <summary>
    /// Method called to recover one object from the pool.
    /// </summary>
    /// <returns>The game object retrieved</returns>
    public GameObject Out()
    {
        GameObject buffer;

        if (_itemPool.Count > 0)
        {
            buffer = _itemPool.Pop();
            buffer.gameObject.SetActive(true);
        }
        else
        {
            buffer = Instantiate(Class.gameObject, transform);
            buffer.name = Class.name;
        }

        return buffer;
    }


    /// <summary>
    /// Method called to store an object inside a pool.
    /// </summary>
    /// <param name="newObject">The stored object</param>
    public void In(GameObject newObject)
    {
        newObject.gameObject.SetActive(false);
        newObject.transform.parent = transform;
        _itemPool.Push(newObject);
    }
}