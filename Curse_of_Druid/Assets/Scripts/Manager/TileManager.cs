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
    private GameObject tileParent;
    [SerializeField]
    private bool isDebugMod;

    private void Awake()
    {
        tileParent = new GameObject("TileParant");
        TileDict = new Dictionary<Coordinate, Tile>();
    }

    /// <summary>
    /// 특정 Coordinate에 TILE_ID 타일을 설치 (성공여부 반환)
    /// </summary>
    public bool PlaceTile(Coordinate coor, TILE_ID tileType, bool isGenerating = false)
    {
        if (TileDict.ContainsKey(coor)) return false;

        GameObject newTile = Instantiate(tilePrefabList[(int)tileType]);
        newTile.transform.parent = tileParent.transform;
        newTile.transform.position = Coordinate.CoordinatetoWorldPoint(coor);

        // Initialize Tile Class
        var tmp = newTile.GetComponent<Tile>();
        TileDict[coor] = tmp;
        tmp.Pos = coor;

        // Notify Tile has changed        
        EventManager.Inst.PostNotification(EVENT_TYPE.TileDestroyed, null, coor);

        // Update rule tiles
        if (isGenerating) return true;
        (tmp as RuleTile)?.UpdateRuleTile();
        UpdateAdjacentRuleTile(coor);
        return true;
    }

    /// <summary>
    /// 특정 Coordinate에 타일을 제거 (성공여부 반환)
    /// </summary>
    public bool DestroyTile(Coordinate coor)
    {
        if (!TileDict.ContainsKey(coor)) return false;

        Destroy(TileDict[coor].gameObject);
        TileDict.Remove(coor);

        // Notify Tile has changed
        EventManager.Inst.PostNotification(EVENT_TYPE.TileDestroyed, null, coor);

        // Update adjacent rule tiles
        UpdateAdjacentRuleTile(coor);

        return true;
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