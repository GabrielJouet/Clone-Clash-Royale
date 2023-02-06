using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class used to handle tower and units attack and health.
/// </summary>
public class Entity : MonoBehaviour
{
    /// <summary>
    /// Health of this unit, set to 0 it will die.
    /// </summary>
    [SerializeField, Range(2, 100)]
    protected int _health;

    /// <summary>
    /// Health max of this unit, used between pooling.
    /// </summary>
    protected int _healthMax;


    /// <summary>
    /// Attack speed of this unit.
    /// </summary>
    [SerializeField, Range(0.25f, 3.5f)]
    protected float _attackSpeed;

    /// <summary>
    /// Attack damage of this unit.
    /// </summary>
    [SerializeField, Range(1, 10)]
    protected int _attackDamage;

    /// <summary>
    /// Projectile used to attack other entities.
    /// </summary>
    [SerializeField]
    protected GameObject _projectile;


    /// <summary>
    /// All unit within the seeing zone.
    /// </summary>
    protected List<Unit> _potentialTargets = new List<Unit>();

    /// <summary>
    /// All unit within the attacking zone.
    /// </summary>
    protected List<Unit> _targets = new List<Unit>();


    /// <summary>
    /// Does this unit is an enemy one?
    /// </summary>
    public bool Enemy { get; protected set; }



    /// <summary>
    /// Awake method, called at initialization before Start.
    /// </summary>
    protected virtual void Awake()
    {
        _healthMax = _health;
    }


    /// <summary>
    /// Method called to add an unit when too near.
    /// </summary>
    /// <param name="unit">The new unit added</param>
    /// <param name="attack">Does the unit is near enough to be attacked?</param>
    public virtual void AddUnit(Unit unit, bool attack)
    {
        if (Enemy && !unit.Enemy || !Enemy && unit.Enemy)
        {
            if (attack && !_targets.Contains(unit))
                _targets.Add(unit);
            else if (!attack && !_potentialTargets.Contains(unit))
                _potentialTargets.Add(unit);
        }
    }


    /// <summary>
    /// Method called to remove an unit when too far.
    /// </summary>
    /// <param name="unit">The new unit added</param>
    /// <param name="attack">Does the unit is near enough to be attacked?</param>
    public void RemoveUnit(Unit unit, bool attack)
    {
        if (attack && _targets.Contains(unit))
            _targets.Remove(unit);
        else if (!attack && _potentialTargets.Contains(unit))
            _potentialTargets.Remove(unit);
    }


    /// <summary>
    /// Method called to find the nearest unit based on the list entered.
    /// </summary>
    /// <param name="targets">The targeted units</param>
    /// <returns>The nearest unit</returns>
    protected Unit FindNearestUnit(List<Unit> targets)
    {
        Unit nearestUnit = null;
        float minDistance = Mathf.Infinity;
        foreach (Unit unit in targets)
        {
            if ((unit.transform.position - transform.position).magnitude < minDistance)
            {
                minDistance = (unit.transform.position - transform.position).magnitude;
                nearestUnit = unit;
            }
        }

        return nearestUnit;
    }


    /// <summary>
    /// Method called when the entity is attacked.
    /// </summary>
    /// <param name="amount">The amount of damage dealt</param>
    public virtual void TakeDamage(int amount)
    {
        _health = Mathf.Clamp(_health - amount, 0, _healthMax);

        if (_health == 0)
            Die();
    }


    /// <summary>
    /// Method called when the entity die.
    /// </summary>
    protected virtual void Die()
    {
        Controller.Instance.PoolController.In(gameObject);
    }
}