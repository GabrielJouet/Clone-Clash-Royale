using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle paths and check best point position.
/// </summary>
public class PointController : MonoBehaviour
{
    /// <summary>
    /// Left path of the world.
    /// </summary>
    [SerializeField]
    private List<Point> _leftPath;

    /// <summary>
    /// Right path of the world.
    /// </summary>
    [SerializeField]
    private List<Point> _rightPath;



    /// <summary>
    /// Method called to get the nearest point relative to the unit position.
    /// </summary>
    /// <param name="position">The unit position</param>
    /// <returns>The point found</returns>
    public Point GetNearestPoint(Vector3 position)
    {
        List<Point> pointsBuffered = position.x <= 0 ? _rightPath : _leftPath;

        Point nearestPoint = null;
        float minDistance = Mathf.Infinity;
        foreach(Point point in pointsBuffered)
        {
            if ((point.transform.position - position).magnitude < minDistance)
            {
                minDistance = (point.transform.position - position).magnitude;
                nearestPoint = point;
            }
        }

        return nearestPoint;
    }


    /// <summary>
    /// Method called to get the better point (not the nearest but the point that will waste the less time) relative to the unit position.
    /// </summary>
    /// <param name="position">The unit position</param>
    /// <param name="enemy">Does the unit seeking the point is an enemy one?</param>
    /// <returns>The point found</returns>
    public Point GetBetterPoint(Vector3 position, bool enemy)
    {
        List<Point> pointsBuffered = position.x <= 0 ? _rightPath : _leftPath;

        Point nearestPoint = null;
        float minDistance = Mathf.Infinity;
        foreach (Point point in pointsBuffered)
        {
            if ((!enemy ? point.transform.position.z >= position.z : point.transform.position.z <= position.z) && (point.transform.position - position).magnitude < minDistance)
            {
                minDistance = (point.transform.position - position).magnitude;
                nearestPoint = point;
            }
        }

        return nearestPoint;
    }


    /// <summary>
    /// Method called to get the nearest tower of the relative position.
    /// </summary>
    /// <param name="position">The position of the unit</param>
    /// <param name="enemy">Does this unit is an enemy?</param>
    /// <returns>Returns the nearest tower in a way point format</returns>
    public Point GetNearestTower(Vector3 position, bool enemy)
    {
        PlayableController controller = enemy ? (PlayableController)Controller.Instance.PlayerController : Controller.Instance.EnemyController;

        return position.x <= 0 ? controller.RightTower.Waypoint : controller.LeftTower.Waypoint;
    }
}