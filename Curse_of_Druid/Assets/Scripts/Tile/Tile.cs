using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Tile")]
    [SerializeField]
    private TILE_ID tileId;

    public Coordinate Pos { get; set; }
    public TILE_ID TileId => tileId;
    public Action<Coordinate> DestroyCallback {get;set;}

    public void SetPos()
    {
        Pos = Coordinate.WorldPointToCoordinate(transform.position);
    }

    public void Destroy()
    {
        // todo
        // TileManager TileArray에서 Tile info 삭제해야함
        DestroyCallback(Pos);

        Destroy(this.gameObject);
    }
}