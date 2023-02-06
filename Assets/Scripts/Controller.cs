using UnityEngine;

/// <summary>
/// Class that will handle every other controllers.
/// </summary>
[RequireComponent(typeof(PoolController))]
public class Controller : MonoBehaviour
{
    /// <summary>
    /// Instance of itself.
    /// </summary>
    public static Controller Instance { get; private set; } = null;

    /// <summary>
    /// Pool Controller component.
    /// </summary>
    public PoolController PoolController { get; private set; }



    /// <summary>
    /// Awake method, called at initialization before start.
    /// </summary>
    private void Awake()
    {
        if (FindObjectsOfType<Controller>().Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        Instance = this;

        PoolController = GetComponent<PoolController>();
    }
}