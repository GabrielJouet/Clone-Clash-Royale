using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;

    [SerializeField]
    private TextMeshProUGUI _cost;

    [SerializeField]
    private GameObject _wantedBorders;


    public Unit Unit { get; private set; }


    private Image _background;

    private bool _active = false;

    private bool _canBeInteracted = false;


    public void Initialize(Unit unit, bool active)
    {
        _background = GetComponent<Image>();
        Unit = unit;
        _name.text = unit.name;
        _cost.text = unit.ManaCost.ToString();

        _canBeInteracted = active;
    }


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


    public void SetAvailable()
    {
        _background.color = Color.white;
    }


    public void SetUnAvailable()
    {
        _background.color = Color.gray;
    }


    public void SetSelected()
    {
        _active = true;
        _background.color = Color.green;
    }


    public void SetWanted()
    {
        _wantedBorders.SetActive(true);
    }


    public void SetUnWanted()
    {
        _wantedBorders.SetActive(false);
    }
}