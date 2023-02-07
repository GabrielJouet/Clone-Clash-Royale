using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to handle either the player controller and enemy controller.
/// </summary>
public abstract class PlayableController : MonoBehaviour
{
    /// <summary>
    /// Mana slider, used to display how much mana is available.
    /// </summary>
    [SerializeField]
    protected Slider _manaSlider;

    /// <summary>
    /// Mana caption used to display how much mana is available rounded up.
    /// </summary>
    [SerializeField]
    protected TextMeshProUGUI _manaCaption;


    [Header("Towers")]

    /// <summary>
    /// Left tower of this player.
    /// </summary>
    [SerializeField]
    protected Tower _leftTower;

    /// <summary>
    /// Left tower of this player.
    /// </summary>
    public Tower LeftTower { get => _leftTower; }


    /// <summary>
    /// Right tower of this player.
    /// </summary>
    [SerializeField]
    protected Tower _rightTower;

    /// <summary>
    /// Right tower of this player.
    /// </summary>
    public Tower RightTower { get => _rightTower; }


    /// <summary>
    /// Dungeon of this player.
    /// </summary>
    [SerializeField]
    protected Tower _dungeon;

    /// <summary>
    /// Dungeon of this player.
    /// </summary>
    public Tower Dungeon { get => _dungeon; }


    [Header("Deck")]

    /// <summary>
    /// Deck used by this player.
    /// </summary>
    [SerializeField]
    protected Deck _deck;


    /// <summary>
    /// How much the player / enemy has mana right now?
    /// </summary>
    public float Mana { get; protected set; } = 7;

    /// <summary>
    /// How much mana is gained per second.
    /// </summary>
    protected float _manaPerSecond = 0.25f;



    /// <summary>
    /// Start method, called at initialization after Awake.
    /// </summary>
    protected virtual void Start()
    {
        StartCoroutine(ChargeUpMana());
    }


    /// <summary>
    /// Coroutine used to delay the mana loading.
    /// </summary>
    protected IEnumerator ChargeUpMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            AddMana(_manaPerSecond / 4);
        }
    }


    /// <summary>
    /// Method called to add mana to the player / enemy.
    /// </summary>
    /// <param name="amount">How much mana is added?</param>
    protected void AddMana(float amount)
    {
        Mana = Mathf.Clamp(Mana + amount, 0, 10);
        _manaSlider.value = Mana;
        _manaCaption.text = Mathf.FloorToInt(Mana).ToString();

        _deck.UpdateManaValue(Mathf.FloorToInt(Mana));
    }


    /// <summary>
    /// Method called to remove mana to the player / enemy.
    /// </summary>
    /// <param name="amount">How much mana is added?</param>
    protected void RemoveMana(float amount)
    {
        Mana = Mathf.Clamp(Mana - amount, 0, 10);
        _manaSlider.value = Mana;
        _manaCaption.text = Mathf.FloorToInt(Mana).ToString();
    }


    /// <summary>
    /// Method called to spawn unit based on unit choice and position.
    /// </summary>
    /// <param name="unit">Unit spawned</param>
    /// <param name="position">Position of the new unit</param>
    /// <param name="random">Does a bit of random is applied to position?</param>
    protected void SpawnUnit(Unit unit, Vector3 position, bool random)
    {
        for (int i = 0; i < unit.SpawnedCount; i++)
            Controller.Instance.PoolController.Out(unit.gameObject).GetComponent<Unit>().Initialize(position + (random ? new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) : Vector3.zero ), random);

        RemoveMana(unit.ManaCost);
    }
}