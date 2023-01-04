using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Tile")]
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private TILE_ID tileId;

    public Coordinate Pos { get; set; }
    public int Health => health;
    public TILE_ID TileId => tileId;

    protected virtual void Awake()
    {
        maxHealth = health;
    }
}