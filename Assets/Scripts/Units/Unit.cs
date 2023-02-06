using UnityEngine;

/// <summary>
/// Class used to handle a single unit behavior.
/// </summary>
public class Unit : MonoBehaviour
{
    /// <summary>
    /// Health of this unit, set to 0 it will die.
    /// </summary>
    [SerializeField, Range(2, 50)]
    private int _health;

    /// <summary>
    /// Health max of this unit, used between pooling.
    /// </summary>
    private int _healthMax;


    /// <summary>
    /// Speed of this unit.
    /// </summary>
    [SerializeField, Range(0.5f, 2)]
    private float _speed;

    /// <summary>
    /// Attack speed of this unit.
    /// </summary>
    [SerializeField, Range(0.25f, 1)]
    private float _attackSpeed;

    /// <summary>
    /// Attack damage of this unit.
    /// </summary>
    [SerializeField, Range(1, 10)]
    private int _attackDamage;

    /// <summary>
    /// Minimum range before an unit saw another unit and try to kill it.
    /// </summary>
    [SerializeField, Range(0.5f, 2)]
    private float _seeRange;

    /// <summary>
    /// Minimum range before an unit can attack another.
    /// </summary>
    [SerializeField, Range(0.1f, 1)]
    private float _attackRange;

    /// <summary>
    /// Mana cost of this unit.
    /// </summary>
    [SerializeField, Range(1, 10)]
    private int _manaCost;


    /// <summary>
    /// Does this unit is flying?
    /// </summary>
    [SerializeField]
    private bool _flying;

    /// <summary>
    /// Does this unit ignore other units?
    /// </summary>
    [SerializeField]
    private bool _ignoreOponents;

    /// <summary>
    /// How many units will be spawned at once?
    /// </summary>
    [SerializeField]
    private int _spawnedCount;
}