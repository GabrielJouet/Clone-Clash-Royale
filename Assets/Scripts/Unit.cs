using UnityEngine;

/// <summary>
/// Class used to handle a single unit behavior.
/// </summary>
public class Unit : MonoBehaviour
{
    /// <summary>
    /// Health of this unit, set to 0 it will die.
    /// </summary>
    [SerializeField]
    private int _health;

    /// <summary>
    /// Health max of this unit, used between pooling.
    /// </summary>
    private int _healthMax;


    /// <summary>
    /// Speed of this unit.
    /// </summary>
    [SerializeField]
    private float _speed;

    /// <summary>
    /// Attack speed of this unit.
    /// </summary>
    [SerializeField]
    private float _attackSpeed;

    /// <summary>
    /// Attack damage of this unit.
    /// </summary>
    [SerializeField]
    private int _attackDamage;

    /// <summary>
    /// Minimum range before an unit saw another unit and try to kill it.
    /// </summary>
    [SerializeField]
    private float _seeRange;

    /// <summary>
    /// Minimum range before an unit can attack another.
    /// </summary>
    [SerializeField]
    private float _attackRange;

    /// <summary>
    /// Mana cost of this unit.
    /// </summary>
    [SerializeField]
    private int _manaCost;
}