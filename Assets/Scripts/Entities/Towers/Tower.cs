using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle tower related behavior.
/// </summary>
public class Tower : Entity
{
    /// <summary>
    /// Does this tower is an enemy one?
    /// </summary>
    [SerializeField]
    private bool _enemy;


    /// <summary>
    /// Does this tower is already attacking?
    /// </summary>
    protected bool _isAttacking = false;



    /// <summary>
    /// Start method, called after Awake.
    /// </summary>
    private void Start()
    {
        Enemy = _enemy;
    }


    /// <summary>
    /// Method called to add an unit when too near.
    /// </summary>
    /// <param name="unit">The new unit added</param>
    /// <param name="attack">Does the unit is near enough to be attacked?</param>
    public override void AddUnit(Unit unit, bool attack)
    {
        base.AddUnit(unit, attack);

        if (!_isAttacking)
            StartCoroutine(AttackNearestEnemy());
    }


    /// <summary>
    /// Coroutine used to attack the nearest enemy in sight.
    /// </summary>
    private IEnumerator AttackNearestEnemy()
    {
        _isAttacking = true;

        while (true)
        {
            RemoveDisactivatedUnits();

            if (_targets.Count > 0)
                Controller.Instance.PoolController.Out(_projectile).GetComponent<Projectile>().Initialize(Enemy, FindNearestUnit(_targets), _attackDamage, transform.position);
            else
                break;

            yield return new WaitForSeconds(_attackSpeed);
        }

        _isAttacking = false;
    }


    private void RemoveDisactivatedUnits()
    {
        List<Unit> unitsToRemove = new List<Unit>();
        foreach (Unit unit in _targets)
            if (!unit.gameObject.activeSelf)
                unitsToRemove.Add(unit);

        foreach(Unit unitToRemove in unitsToRemove)
            _targets.Remove(unitToRemove);

        unitsToRemove.Clear();

        foreach (Unit unit in _potentialTargets)
            if (!unit.gameObject.activeSelf)
                unitsToRemove.Add(unit);

        foreach (Unit unitToRemove in unitsToRemove)
            _potentialTargets.Remove(unitToRemove);
    }
}