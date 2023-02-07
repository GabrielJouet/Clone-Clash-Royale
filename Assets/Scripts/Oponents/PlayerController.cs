using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle player behavior, inherit Playable controller.
/// </summary>
public class PlayerController : PlayableController
{
    /// <summary>
    /// Spawn zone, needed for raycasting.
    /// </summary>
    [SerializeField]
    private GameObject _spawnZone;


    /// <summary>
    /// All units in the game.
    /// </summary>
    private List<Unit> _allUnits;


    /// <summary>
    /// Units actually displayed in the deck.
    /// </summary>
    private readonly List<Unit> _deckUnits = new List<Unit>();


    private readonly List<Unit> _nonUsedUnits = new List<Unit>();


    /// <summary>
    /// Unit actually selected and loaded in this controller.
    /// </summary>
    private Unit _unitSelected;



    /// <summary>
    /// Start method, called at initialization after Awake.
    /// </summary>
    protected override void Start()
    {
        base.Start();

        _allUnits = new List<Unit>(Controller.Instance.UnitController.Units);
        _allUnits.Shuffle();

        for (int i = 0; i < 8; i++)
        {
            if (i < 4)
                _deckUnits.Add(_allUnits[i]);
            else
                _nonUsedUnits.Add(_allUnits[i]);
        }

        _deck.Initialize(_deckUnits, false);
    }


    /// <summary>
    /// Update method, called every frame.
    /// </summary>
    private void Update()
    {
        if (_unitSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray);

                foreach(RaycastHit hit in hits)
                {
                    if (hit.transform.gameObject.name.Equals("SpawnZone"))
                    {
                        SpawnUnit(_unitSelected, hit.point, false);

                        _deck.UnSelect();
                        _deck.SwapCards(_unitSelected, ReplaceUnits());
                        _unitSelected = null;
                        _spawnZone.SetActive(false);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Method called to load an unit inside this controller, meaning the next click will spawn an unit.
    /// </summary>
    /// <param name="unit">The unit loaded</param>
    public void LoadUnit(Unit unit)
    {
        _unitSelected = unit;
        _spawnZone.SetActive(true);
    }


    /// <summary>
    /// Method called to unload an unit.
    /// </summary>
    public void UnLoadUnit()
    {
        _unitSelected = null;
        _spawnZone.SetActive(false);
    }


    /// <summary>
    /// Method called to find one unit not present in the deck.
    /// </summary>
    /// <returns>Returns a non present unit in the deck</returns>
    private Unit ReplaceUnits()
    {
        _nonUsedUnits.Shuffle();

        Unit buffer = _nonUsedUnits[0];
        _deckUnits.Add(buffer);

        _deckUnits.Remove(_unitSelected);

        _nonUsedUnits.Add(_unitSelected);
        _nonUsedUnits.Remove(buffer);

        return buffer;
    }
}