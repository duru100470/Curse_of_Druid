using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : SingletonBehavior<TileManager>
{
    public Tile[,] TileArray { get; set; }
    public int worldXSize { get; set; }
    public int worldYSize { get; set; }

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
    }

    /// <summary>
    /// 특정 Coordinate에 TILE_ID 타일을 설치 (성공여부 반환)
    /// </summary>
    public bool PlaceTile(Coordinate coor, TILE_ID tileType, bool isGenerating = false)
    {
        if (TileArray[coor.X, coor.Y] != null) return false;

        GameObject newTile = Instantiate(tilePrefabList[(int)tileType]);
        newTile.transform.parent = tileParent.transform;
        newTile.transform.position = Coordinate.CoordinatetoWorldPoint(coor);

        // Initialize Tile Class
        var tmp = newTile.GetComponent<Tile>();
        TileArray[coor.X, coor.Y] = tmp;
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
        if (TileArray[coor.X, coor.Y] == null) return false;

        Destroy(TileArray[coor.X, coor.Y].gameObject);
        TileArray[coor.X, coor.Y] = null;

        // Notify Tile has changed
        EventManager.Inst.PostNotification(EVENT_TYPE.TileDestroyed, null, coor);

        // Update adjacent rule tiles
        UpdateAdjacentRuleTile(coor);

        return true;
    }

    public void UpdateAdjacentRuleTile(Coordinate coor)
    {
        if (coor.X != 0 && coor.Y != 0)
            (TileArray[coor.X - 1, coor.Y - 1] as RuleTile)?.UpdateRuleTile();
        if (coor.Y != 0)
            (TileArray[coor.X, coor.Y - 1] as RuleTile)?.UpdateRuleTile();
        if (coor.X != worldXSize - 1 && coor.Y != 0)
            (TileArray[coor.X + 1, coor.Y - 1] as RuleTile)?.UpdateRuleTile();
        if (coor.X != 0)
            (TileArray[coor.X - 1, coor.Y] as RuleTile)?.UpdateRuleTile();
        if (coor.X != worldXSize - 1)
            (TileArray[coor.X + 1, coor.Y] as RuleTile)?.UpdateRuleTile();
        if (coor.X != 0 && coor.Y != worldYSize - 1)
            (TileArray[coor.X - 1, coor.Y + 1] as RuleTile)?.UpdateRuleTile();
        if (coor.Y != worldYSize - 1)
            (TileArray[coor.X, coor.Y + 1] as RuleTile)?.UpdateRuleTile();
        if (coor.X != worldXSize - 1 && coor.Y != worldYSize - 1)
            (TileArray[coor.X + 1, coor.Y + 1] as RuleTile)?.UpdateRuleTile();
    }
}