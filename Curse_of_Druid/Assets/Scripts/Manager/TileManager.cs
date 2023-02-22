using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : SingletonBehavior<TileManager>
{
    public Dictionary<Coordinate, Tile> TileDict { get; set; }

    [SerializeField]
    private List<GameObject> tilePrefabList;
    [SerializeField]
    private GameObject crackEffect;

    [Header("Debug")]
    [SerializeField]
    private GameObject tile;
    [SerializeField]
    private bool isDebugMod;

    private void Awake()
    {
        TileDict = new Dictionary<Coordinate, Tile>();
    }

    public void AddTile(Coordinate coor, Tile tile)
    {
        TileDict[coor] = tile;
        (tile as RuleTile)?.UpdateRuleTile();
        UpdateAdjacentRuleTile(coor);
    }

    public void RemoveTile(Coordinate coor)
    {
        try
        {
            TileDict.Remove(coor);
            UpdateAdjacentRuleTile(coor);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void UpdateAdjacentRuleTile(Coordinate coor)
    {
        Tile tile;
        if (TileDict.TryGetValue(coor + new Coordinate(-1, -1), out tile))
            (tile as RuleTile)?.UpdateRuleTile();
        if (TileDict.TryGetValue(coor + new Coordinate(0, -1), out tile))
            (tile as RuleTile)?.UpdateRuleTile();
        if (TileDict.TryGetValue(coor + new Coordinate(1, -1), out tile))
            (tile as RuleTile)?.UpdateRuleTile();
        if (TileDict.TryGetValue(coor + new Coordinate(-1, 0), out tile))
            (tile as RuleTile)?.UpdateRuleTile();
        if (TileDict.TryGetValue(coor + new Coordinate(1, 0), out tile))
            (tile as RuleTile)?.UpdateRuleTile();
        if (TileDict.TryGetValue(coor + new Coordinate(-1, 1), out tile))
            (tile as RuleTile)?.UpdateRuleTile();
        if (TileDict.TryGetValue(coor + new Coordinate(0, 1), out tile))
            (tile as RuleTile)?.UpdateRuleTile();
        if (TileDict.TryGetValue(coor + new Coordinate(1, 1), out tile))
            (tile as RuleTile)?.UpdateRuleTile();
    }
}