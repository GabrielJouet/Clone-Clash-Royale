using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;

    [SerializeField]
    private TextMeshProUGUI _cost;


    private bool _canBeInteracted = false;


    public void Initialize(Unit unit, bool active)
    {
        _name.text = unit.name;
        _cost.text = unit.ManaCost.ToString();

        _canBeInteracted = active;
    }
}