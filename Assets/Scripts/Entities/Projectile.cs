using UnityEngine;

/// <summary>
/// Class used to handle projectile behavior.
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Projectile speed.
    /// </summary>
    [SerializeField]
    private float _projectileSpeed;


    /// <summary>
    /// Does this projectile is an enemy one?
    /// </summary>
    private bool _enemy;

    /// <summary>
    /// Targeted entity, moving towards this entity.
    /// </summary>
    private Entity _targetedEntity;

    /// <summary>
    /// How much damage this projectile will do?
    /// </summary>
    private int _damage;



    /// <summary>
    /// Method called to initialize the projectile.
    /// </summary>
    /// <param name="enemy">Does this projectile is enemy?</param>
    /// <param name="target">New target of this projectile</param>
    /// <param name="damage">How much damage this projectile do</param>
    /// <param name="position">New position of this projectile</param>
    public void Initialize(bool enemy, Entity target, int damage, Vector3 position)
    {
        _damage = damage;
        _targetedEntity = target;
        _enemy = enemy;

        transform.position = position;
    }


    /// <summary>
    /// Update method, called every frame.
    /// </summary>
    private void Update()
    {
        if (_targetedEntity && _targetedEntity.gameObject.activeSelf)
            transform.position = Vector3.MoveTowards(transform.position, _targetedEntity.transform.position, _projectileSpeed * Time.deltaTime);
        else
            Controller.Instance.PoolController.In(gameObject);
    }


    /// <summary>
    /// Method called when a trigger occurs.
    /// </summary>
    /// <param name="other">The other object collded</param>
    private void OnTriggerEnter(Collider other)
    {
        if (_enemy && other.TryGetComponent(out Entity entity) && !entity.Enemy && entity == _targetedEntity)
            AttackEntity(entity);
        else if (!_enemy && other.TryGetComponent(out Entity enemyEntity) && enemyEntity.Enemy && enemyEntity == _targetedEntity)
            AttackEntity(enemyEntity);
    }


    /// <summary>
    /// Method called when the projectile attacks an entity.
    /// </summary>
    /// <param name="entity">The entity attacked</param>
    private void AttackEntity(Entity entity)
    {
        entity.TakeDamage(_damage);
        Controller.Instance.PoolController.In(gameObject);
    }
}