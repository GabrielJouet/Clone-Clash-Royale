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
}