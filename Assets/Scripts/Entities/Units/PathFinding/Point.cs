using UnityEngine;

/// <summary>
/// Class used to handle a waypoint in the unit movement.
/// </summary>
public class Point : MonoBehaviour
{
    /// <summary>
    /// The following point on the path.
    /// </summary>
    [SerializeField]
    private Point _followingPoint;

    /// <summary>
    /// The following point on the path.
    /// </summary>
    public Point NextPoint { get => _followingPoint; }


    /// <summary>
    /// The previous point on the path.
    /// </summary>
    [SerializeField]
    private Point _previousPoint;

    /// <summary>
    /// The previous point on the path.
    /// </summary>
    public Point PreviousPoint { get => _previousPoint; }
}