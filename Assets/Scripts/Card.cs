using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to handle a single card behavior.
/// </summary>
[RequireComponent(typeof(Image), typeof(Button))]
public class Card : MonoBehaviour
{
    /// <summary>
    /// Text component used to display the name of this card.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _name;

    /// <summary>
    /// Text component used to display the cost of this card.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _cost;

    /// <summary>
    /// Game object used to display if this card is wanted or not.
    /// </summary>
    [SerializeField]
    private GameObject _wantedBorders;


    /// <summary>
    /// Actual unit displayed.
    /// </summary>
    public Unit Unit { get; private set; }


    /// <summary>
    /// Image component shortcut.
    /// </summary>
    private Image _background;

    /// <summary>
    /// Button component shortcut.
    /// </summary>
    private Button _button;

    private bool _canBeInteracted = false;

    private bool _activated = false;



    /// <summary>
    /// Method called to initialize a card based on unit and parameters.
    /// </summary>
    /// <param name="unit">Actual unit that will be passed to this card</param>
    /// <param name="enemy">Does the card can be activated?</param>
    public void Initialize(Unit unit, bool enemy)
    {
        _background = GetComponent<Image>();
        _button = GetComponent<Button>();

        Unit = unit;
        _name.text = unit.name;
        _cost.text = unit.ManaCost.ToString();

        _canBeInteracted = !enemy;
        _button.enabled = !enemy;
    }


    /// <summary>
    /// Method called to set an unit to a card.
    /// </summary>
    /// <param name="unit">The new unit</param>
    /// <param name="mana">The mana value</param>
    public void SetUnit(Unit unit, int mana)
    {
        Unit = unit;
        _name.text = unit.name;
        _cost.text = unit.ManaCost.ToString();

        UpdateManaValue(mana);
    }


    /// <summary>
    /// Method called by controllers to update the mana value displayed.
    /// </summary>
    /// <param name="manaValue">The new mana value</param>
    public void UpdateManaValue(int manaValue)
    {
        if (!_activated)
        {
            if (manaValue >= Unit.ManaCost)
                SetAvailable();
            else
                SetUnAvailable();
        }
    }


    /// <summary>
    /// Method called to set a the card as available.
    /// </summary>
    public void SetAvailable()
    {
        _background.color = Color.white;
        _button.enabled = _canBeInteracted;
    }


    /// <summary>
    /// Method called to set a the card as unavailable.
    /// </summary>
    public void SetUnAvailable()
    {
        _background.color = Color.gray;
        _button.enabled = false;
    }


    /// <summary>
    /// Method called to set a the card as selected.
    /// </summary>
    public void SetSelected()
    {
        if (!_activated)
        {
            _activated = true;
            Controller.Instance.PlayerController.LoadUnit(Unit);
            _background.color = Color.green;
        }
        else
        {
            Controller.Instance.PlayerController.UnLoadUnit();
            SetUnSelected();
        }
    }


    /// <summary>
    /// Method called to set a the card as selected.
    /// </summary>
    /// <param name="mana">Mana value to reset card</param>
    public void SetUnSelected()
    {
        _activated = false;
        _background.color = _button.enabled ? Color.white : Color.gray;
    }


    /// <summary>
    /// Method called to set a the card as wanted.
    /// </summary>
    public void SetWanted()
    {
        _wantedBorders.SetActive(true);
    }


    /// <summary>
    /// Method called to set a the card as unwanted.
    /// </summary>
    public void SetUnWanted()
    {
        _wantedBorders.SetActive(false);
    }
}