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
    }
}