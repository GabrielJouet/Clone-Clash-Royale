using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to handle a single card behavior.
/// </summary>
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
    /// Does this card is actively used by player?
    /// </summary>
    private bool _active = false;

    /// <summary>
    /// Can the card be interacted at all?
    /// </summary>
    private bool _canBeInteracted = false;


    /// <summary>
    /// Method called to initialize a card based on unit and parameters.
    /// </summary>
    /// <param name="unit">Actual unit that will be passed to this card</param>
    /// <param name="active">Does the card can be activated?</param>
    public void Initialize(Unit unit, bool active)
    {
        _background = GetComponent<Image>();
        Unit = unit;
        _name.text = unit.name;
        _cost.text = unit.ManaCost.ToString();

        _canBeInteracted = active;
    }


    /// <summary>
    /// Method called by controllers to update the mana value displayed.
    /// </summary>
    /// <param name="manaValue">The new mana value</param>
    public void UpdateManaValue(int manaValue)
    {
        if (!_active)
        {
            if (manaValue > Unit.ManaCost)
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
    }


    /// <summary>
    /// Method called to set a the card as unavailable.
    /// </summary>
    public void SetUnAvailable()
    {
        _background.color = Color.gray;
    }


    /// <summary>
    /// Method called to set a the card as selected.
    /// </summary>
    public void SetSelected()
    {
        _active = true;
        _background.color = Color.green;
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