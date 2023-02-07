using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to handle tower related behavior.
/// </summary>
[RequireComponent(typeof(Point))]
public class Tower : Entity
{
    /// <summary>
    /// Health slider, used to display the health remaining.
    /// </summary>
    [SerializeField]
    private Slider _healthSlider;

    /// <summary>
    /// Does this tower is an enemy one?
    /// </summary>
    [SerializeField]
    private bool _enemy;


    /// <summary>
    /// Waypoint component in this tower.
    /// </summary>
    public Point Waypoint { get => GetComponent<Point>(); }


    /// <summary>
    /// Does this tower is already attacking?
    /// </summary>
    protected bool _isAttacking = false;



    /// <summary>
    /// Start method, called after Awake.
    /// </summary>
    private void Start()
    {
        _healthSlider.maxValue = _healthMax;
        _healthSlider.value = _healthMax;
        Enemy = _enemy;
    }


    /// <summary>
    /// Method called when the entity is attacked.
    /// </summary>
    /// <param name="amount">The amount of damage dealt</param>
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        _healthSlider.value = _health;
    }


    /// <summary>
    /// Method called when the entity die.
    /// </summary>
    protected override void Die()
    {
        base.Die();

        Destroy(_healthSlider);
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


    /// <summary>
    /// Method called to remote already dead units inside the stack.
    /// </summary>
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