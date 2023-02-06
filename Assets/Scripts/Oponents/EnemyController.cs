using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle enemy behavior, inherit Playable controller.
/// </summary>
public class EnemyController : PlayableController
{
    /// <summary>
    /// Next unit that will be spawned.
    /// </summary>
    private Unit _nextUnit;

    /// <summary>
    /// All units buffered.
    /// </summary>
    private List<Unit> _units;



    /// <summary>
    /// Start method, called at initialization after Awake.
    /// </summary>
    protected override void Start()
    {
        base.Start();

        _units = Controller.Instance.UnitController.Units;
        _nextUnit = _units[Random.Range(0, _units.Count)];

        StartCoroutine(SpawnUnit());
    }


    /// <summary>
    /// Coroutine used to delay the spawn of new unit.
    /// </summary>
    protected IEnumerator SpawnUnit()
    {
        while (true)
        {
            if (Mana >= _nextUnit.ManaCost)
            {
                Controller.Instance.PoolController.Out(_nextUnit.gameObject).GetComponent<Unit>().Initialize(new Vector3(Random.Range(-3f, 3f) , 0, Random.Range(3.5f, 8)), true);
                RemoveMana(_nextUnit.ManaCost);
                _nextUnit = _units[Random.Range(0, _units.Count)];
            }

            yield return new WaitForSeconds(1);
        }
    }
}