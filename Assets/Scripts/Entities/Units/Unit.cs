using System.Collections;
using UnityEngine;

/// <summary>
/// Class used to handle a single unit behavior.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Unit : Entity
{
    /// <summary>
    /// Speed of this unit.
    /// </summary>
    [SerializeField, Range(0.5f, 2)]
    private float _speed;

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
    /// How many units will be spawned at once?
    /// </summary>
    public int SpawnedCount { get => _spawnedCount; }


    /// <summary>
    /// Next point loaded.
    /// </summary>
    private Point _nextPoint;

    /// <summary>
    /// Goal position of this unit.
    /// </summary>
    private Vector3 _goalPosition;
    

    /// <summary>
    /// Goal unit for this unit (as a movement base)
    /// </summary>
    private Entity _goalUnit;

    /// <summary>
    /// Goal unit for this unit (as a combat base)
    /// </summary>
    private Entity _attackedUnit;


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
    protected override void Awake()
    {
        base.Awake();

        _rigidBody = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// Method called to initialized an unit.
    /// </summary>
    /// <param name="position">New position of this unit</param>
    /// <param name="enemy">Does this unit is an enemy?</param>
    public void Initialize(Vector3 position, bool enemy)
    {
        StopAllCoroutines();

        _targets.Clear();
        _potentialTargets.Clear();
        Enemy = enemy;
        _attackedUnit = null;
        _goalUnit = null;

        _nextPoint = _flying ? Controller.Instance.PointController.GetNearestTower(position, Enemy) : Controller.Instance.PointController.GetBetterPoint(position, Enemy);
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
        if (_canMove && !_attackedUnit)
        {
            _rigidBody.MovePosition(Vector3.MoveTowards(transform.position, _goalUnit ? _goalUnit.transform.position : _goalPosition, Time.deltaTime * _speed));

            if (!_goalUnit)
            {
                if ((transform.position - _goalPosition).magnitude <= 0.5f && (Enemy ? _nextPoint.PreviousPoint : _nextPoint.NextPoint))
                {
                    _nextPoint = Enemy ? _nextPoint.PreviousPoint : _nextPoint.NextPoint;
                    _goalPosition = new Vector3(_nextPoint.transform.position.x, transform.position.y, _nextPoint.transform.position.z);
                }
            }

            if (_goalUnit && !_goalUnit.gameObject.activeSelf)
                RemoveUnitSeen(_goalUnit);
        }
        else if (_attackedUnit && !_attackedUnit.gameObject.activeSelf)
            RemoveUnitAttacked(_attackedUnit);

        transform.position = _rigidBody.position;
    }


    /// <summary>
    /// Coroutine used to delay each attack.
    /// </summary>
    private IEnumerator Attack()
    {
        do
        {
            yield return new WaitForSeconds(_attackSpeed);
            RemoveDisactivatedUnits();

            if (_attackedUnit)
                Controller.Instance.PoolController.Out(_projectile).GetComponent<Projectile>().Initialize(Enemy, _attackedUnit, _attackDamage, transform.position);
        }
        while (_attackedUnit && _attackedUnit.gameObject.activeSelf);
    }


    /// <summary>
    /// Method called to add an entity when too near.
    /// </summary>
    /// <param name="entity">The new entity added</param>
    public override void AddUnitSeen(Entity entity)
    {
        if (!(_ignoreOponents && entity.TryGetComponent(out Unit unit)))
        {
            if (((Enemy && !entity.Enemy) || (!Enemy && entity.Enemy)) && !_potentialTargets.Contains(entity))
                _potentialTargets.Add(entity);

            if (_potentialTargets.Count > 0 && !_goalUnit)
                _goalUnit = FindNearestUnit(_potentialTargets);
        }
    }


    /// <summary>
    /// Method called to add an entity when too near.
    /// </summary>
    /// <param name="entity">The new entity added</param>
    public override void AddUnitAttacked(Entity entity)
    {
        if (!(_ignoreOponents && entity.TryGetComponent(out Unit unit)))
        {
            if (((Enemy && !entity.Enemy) || (!Enemy && entity.Enemy)) && !_targets.Contains(entity))
                _targets.Add(entity);

            if (_targets.Count > 0 && !_attackedUnit)
            {
                StopAllCoroutines();
                _attackedUnit = FindNearestUnit(_targets);
                StartCoroutine(Attack());
            }
        }
    }


    /// <summary>
    /// Method called to remove an entity when too far.
    /// </summary>
    /// <param name="entity">The new entity added</param>
    public override void RemoveUnitSeen(Entity entity)
    {
        base.RemoveUnitSeen(entity);

        if (_goalUnit == entity)
        {
            if (_potentialTargets.Count > 0)
                _goalUnit = FindNearestUnit(_potentialTargets);
            else
                _goalUnit = null;
        }
    }


    /// <summary>
    /// Method called to remove an entity when too far to attack.
    /// </summary>
    /// <param name="entity">The new entity added</param>
    public override void RemoveUnitAttacked(Entity entity)
    {
        base.RemoveUnitAttacked(entity);

        if (_attackedUnit == entity)
        {
            if (_targets.Count > 0)
                _attackedUnit = FindNearestUnit(_targets);
            else
                _attackedUnit = null;
        }
    }
}