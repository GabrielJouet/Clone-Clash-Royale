using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle a deck like behavior with multiple cards.
/// </summary>
public class Deck : MonoBehaviour
{
    /// <summary>
    /// All cards in the deck.
    /// </summary>
    [SerializeField]
    private List<Card> _cards;


    /// <summary>
    /// Method called to initialize the deck based on new units.
    /// </summary>
    /// <param name="units">Units in the deck</param>
    /// <param name="enemy">Does this deck is for the enemy or player?</param>
    public void Initialize(List<Unit> units, bool enemy)
    {
        for (int i = 0; i < units.Count; i++)
            _cards[i].Initialize(units[i], enemy);

        UpdateManaValue(7);
    }


    /// <summary>
    /// Method called to update the mana value of the deck.
    /// </summary>
    /// <param name="mana">How much mana is available</param>
    public void UpdateManaValue(int mana)
    {
        for (int i = 0; i < _cards.Count; i++)
            _cards[i].UpdateManaValue(mana);
    }


    /// <summary>
    /// Method called by enemy controller to set the wanted card on display.
    /// </summary>
    /// <param name="unit">Which unit is wanted?</param>
    public void SetWantedCard(Unit unit)
    {
        for (int i = 0; i < _cards.Count; i++)
            _cards[i].SetUnWanted();

        _cards.Find((x) => x.Unit == unit).SetWanted();
    }


    /// <summary>
    /// Method called to unselect every card.
    /// </summary>
    public void UnSelect()
    {
        for (int i = 0; i < _cards.Count; i++)
            _cards[i].SetUnSelected();
    }
}