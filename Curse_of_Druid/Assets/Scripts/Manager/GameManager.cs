using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    [SerializeField]
    private int worldXSize;
    [SerializeField]
    private int worldYSize;
    private Player player;

    private void Awake()
    {
        LoadMapAsync();
    }

    private void LoadMapAsync()
    {
        TileManager.Inst.TileArray = GenerateTileArrayAsync();

        InitializePlayer();
    }

    private Tile[,] GenerateTileArrayAsync()
    {
        Tile[,] tileArray = new Tile[worldXSize, worldYSize];
        GameObject[] tileObjs = GameObject.FindGameObjectsWithTag("Tile");

        foreach(var tileObj in tileObjs)
        {
            Tile tile = tileObj.GetComponent<Tile>();
            tile.SetPos();
            (tile as RuleTile)?.UpdateRuleTile();
            tileArray[tile.Pos.X, tile.Pos.Y] = tile;
            Debug.Log($"X: {tile.Pos.X}, Y: {tile.Pos.Y}, TileID: {tile.TileId}");
        }

        Debug.Log("TileArray generating has been finished!");
        return tileArray;
    }

    private void InitializePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
