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

    public void SetPos()
    {
        Pos = Coordinate.WorldPointToCoordinate(transform.position);
    }
}