using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField]
    private List<Card> _cards;


    public void Initialize(List<Unit> units, bool enemy)
    {
        for (int i = 0; i < units.Count; i++)
            _cards[i].Initialize(units[i], enemy);

        UpdateManaValue(7);
    }


    public void UpdateManaValue(int mana)
    {
        for (int i = 0; i < _cards.Count; i++)
            _cards[i].UpdateManaValue(mana);
    }


    public void SetWantedCard(Unit unit)
    {
        for (int i = 0; i < _cards.Count; i++)
            _cards[i].SetUnWanted();

        _cards.Find((x) => x.Unit == unit).SetWanted();
    }
}