using UnityEngine;

/// <summary>
/// Abstract class used to handle tower and units attack and health.
/// </summary>
public class Entity : MonoBehaviour
{
    /// <summary>
    /// Health of this unit, set to 0 it will die.
    /// </summary>
    [SerializeField, Range(2, 50)]
    protected int _health;

    /// <summary>
    /// Health max of this unit, used between pooling.
    /// </summary>
    protected int _healthMax;


    /// <summary>
    /// Attack speed of this unit.
    /// </summary>
    [SerializeField, Range(0.25f, 1)]
    protected float _attackSpeed;

    /// <summary>
    /// Attack damage of this unit.
    /// </summary>
    [SerializeField, Range(1, 10)]
    protected int _attackDamage;

    /// <summary>
    /// Minimum range before an unit can attack another.
    /// </summary>
    [SerializeField, Range(0.1f, 1)]
    protected float _attackRange;

    /// <summary>
    /// Minimum range before an unit saw another unit and try to kill it.
    /// </summary>
    [SerializeField, Range(0.5f, 2)]
    protected float _seeRange;
}