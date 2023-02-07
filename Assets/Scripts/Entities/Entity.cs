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
    /// All entities within the seeing zone.
    /// </summary>
    protected List<Entity> _potentialTargets = new List<Entity>();

    /// <summary>
    /// All entities within the attacking zone.
    /// </summary>
    protected List<Entity> _targets = new List<Entity>();


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
    /// Method called to add an entity when too near.
    /// </summary>
    /// <param name="entity">The new entity added</param>
    public virtual void AddUnitSeen(Entity entity)
    {
        if ((Enemy && !entity.Enemy || !Enemy && entity.Enemy) && !_potentialTargets.Contains(entity))
            _potentialTargets.Add(entity);
    }


    /// <summary>
    /// Method called to add an entity when too near to attack.
    /// </summary>
    /// <param name="entity">The new entity added</param>
    public virtual void AddUnitAttacked(Entity entity)
    {
        if ((Enemy && !entity.Enemy || !Enemy && entity.Enemy) && !_targets.Contains(entity))
            _targets.Add(entity);
    }


    /// <summary>
    /// Method called to remove an entity when too far.
    /// </summary>
    /// <param name="entity">The new entity added</param>
    public virtual void RemoveUnitSeen(Entity entity)
    {
        if (_potentialTargets.Contains(entity))
            _potentialTargets.Remove(entity);
    }


    /// <summary>
    /// Method called to remove an entity when too far to attack.
    /// </summary>
    /// <param name="entity">The new entity added</param>
    public virtual void RemoveUnitAttacked(Entity entity)
    {
        if (_targets.Contains(entity))
            _targets.Remove(entity);
    }


    /// <summary>
    /// Method called to find the nearest entity based on the list entered.
    /// </summary>
    /// <param name="targets">The targeted entities</param>
    /// <returns>The nearest entity</returns>
    protected Entity FindNearestUnit(List<Entity> targets)
    {
        Entity nearestEntity = null;
        float minDistance = Mathf.Infinity;
        foreach (Entity entity in targets)
        {
            if ((entity.transform.position - transform.position).magnitude < minDistance)
            {
                minDistance = (entity.transform.position - transform.position).magnitude;
                nearestEntity = entity;
            }
        }

        return nearestEntity;
    }


    /// <summary>
    /// Method called to remote already dead units inside the stack.
    /// </summary>
    protected void RemoveDisactivatedUnits()
    {
        List<Entity> unitsToRemove = new List<Entity>();
        foreach (Entity unit in _targets)
            if (unit || !unit.gameObject.activeSelf)
                unitsToRemove.Add(unit);

        foreach (Entity unitToRemove in unitsToRemove)
            _targets.Remove(unitToRemove);

        unitsToRemove.Clear();

        foreach (Entity unit in _potentialTargets)
            if (unit || !unit.gameObject.activeSelf)
                unitsToRemove.Add(unit);

        foreach (Entity unitToRemove in unitsToRemove)
            _potentialTargets.Remove(unitToRemove);
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