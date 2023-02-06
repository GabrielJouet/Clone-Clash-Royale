using UnityEngine;

/// <summary>
/// Class that will handle every other controllers.
/// </summary>
[RequireComponent(typeof(PoolController), typeof(EnemyController), typeof(PlayerController))]
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
    /// Player Controller component.
    /// </summary>
    public PlayerController PlayerController { get; private set; }

    /// <summary>
    /// Enemy Controller component.
    /// </summary>
    public EnemyController EnemyController { get; private set; }



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
        PlayerController = GetComponent<PlayerController>();
        EnemyController = GetComponent<EnemyController>();
    }
}