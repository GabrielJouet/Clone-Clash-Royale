using UnityEngine;

/// <summary>
/// Class used to handle the range around the entities for both seeing and attacking.
/// </summary>
public class RangeCollider : MonoBehaviour
{
    /// <summary>
    /// Parent tower to notify.
    /// </summary>
    [SerializeField]
    private Entity _parentEntity;

    /// <summary>
    /// Does this range is used for attack or for vision only?
    /// </summary>
    [SerializeField]
    private bool _attack;


    /// <summary>
    /// Method called when a trigger is made.
    /// </summary>
    /// <param name="other">The item triggered</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
            _parentEntity.AddUnit(unit, _attack);
    }


    /// <summary>
    /// Method called when a trigger is cut off.
    /// </summary>
    /// <param name="other">The item triggered</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
            _parentEntity.RemoveUnit(unit, _attack);
    }
}