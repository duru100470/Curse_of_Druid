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
        GenerateTileArrayAsync();
        InitializePlayer();
    }

    private void GenerateTileArrayAsync()
    {
        GameObject[] tileObjs = GameObject.FindGameObjectsWithTag("Tile");

        foreach(var tileObj in tileObjs)
        {
            Tile tile = tileObj.GetComponent<Tile>();
            tile.SetPos();
            (tile as RuleTile)?.UpdateRuleTile();
            TileManager.Inst.TileDict[tile.Pos] = tile;
            Debug.Log($"X: {tile.Pos.X}, Y: {tile.Pos.Y}, TileID: {tile.TileId}");
        }

        Debug.Log("TileArray generating has been finished!");
    }

    private void InitializePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
