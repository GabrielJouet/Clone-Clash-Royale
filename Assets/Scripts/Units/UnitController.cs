using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that will handle every unit in the game.
/// </summary>
public class UnitController : MonoBehaviour
{
    /// <summary>
    /// All units in the game.
    /// </summary>
    [SerializeField]
    private List<Unit> _units;

    /// <summary>
    /// All units in the game.
    /// </summary>
    public List<Unit> Units { get => _units; }
}