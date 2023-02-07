using System.Collections.Generic;

/// <summary>
/// Class used to handle player behavior, inherit Playable controller.
/// </summary>
public class PlayerController : PlayableController
{
    /// <summary>
    /// All units in the game.
    /// </summary>
    private List<Unit> _allUnits;


    /// <summary>
    /// Units actually displayed in the deck.
    /// </summary>
    private readonly List<Unit> _deckUnits = new List<Unit>();



    /// <summary>
    /// Start method, called at initialization after Awake.
    /// </summary>
    protected override void Start()
    {
        base.Start();

        _allUnits = new List<Unit>(Controller.Instance.UnitController.Units);
        _allUnits.Shuffle();

        for (int i = 0; i < 4; i++)
            _deckUnits.Add(_allUnits[i]);

        _deck.Initialize(_deckUnits, false);
    }
}