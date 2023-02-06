using UnityEngine;

/// <summary>
/// Class used to handle a single unit behavior.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
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
    /// Mana cost of this unit.
    /// </summary>
    public int ManaCost { get => _manaCost; }


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


    /// <summary>
    /// Next point loaded.
    /// </summary>
    private Point _nextPoint;

    /// <summary>
    /// Goal position of this unit.
    /// </summary>
    private Vector3 _goalPosition;


    /// <summary>
    /// Does this unit is an enemy one?
    /// </summary>
    private bool _enemy;

    /// <summary>
    /// Does this unit can move?
    /// </summary>
    private bool _canMove = false;


    /// <summary>
    /// RigidBody component.
    /// </summary>
    private Rigidbody _rigidBody;



    /// <summary>
    /// Awake method, called at initialization before Start.
    /// </summary>
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _healthMax = _health;
    }


    /// <summary>
    /// Method called to initialized an unit.
    /// </summary>
    /// <param name="position">New position of this unit</param>
    /// <param name="enemy">Does this unit is an enemy?</param>
    public void Initialize(Vector3 position, bool enemy)
    {
        _enemy = enemy;
        _nextPoint = Controller.Instance.PointController.GetBetterPoint(position, _enemy);
        _goalPosition = new Vector3(_nextPoint.transform.position.x, transform.position.y, _nextPoint.transform.position.z);

        _canMove = true;
        transform.position = position;
        _health = _healthMax;
    }


    /// <summary>
    /// Update method, called at each frame.
    /// </summary>
    private void Update()
    {
        if (_canMove)
        {
            _rigidBody.MovePosition(Vector3.MoveTowards(transform.position, _goalPosition, Time.deltaTime * _speed));

            if ((transform.position - _goalPosition).magnitude <= 0.075f)
            {
                _nextPoint = _enemy ? _nextPoint.PreviousPoint : _nextPoint.NextPoint;
                _goalPosition = new Vector3(_nextPoint.transform.position.x, transform.position.y, _nextPoint.transform.position.z);
            }
        }
    }
}