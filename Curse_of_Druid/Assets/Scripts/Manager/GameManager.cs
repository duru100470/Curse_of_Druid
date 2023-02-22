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
    [Header("Item Debug")]
    [SerializeField]
    private ItemInfo itemInfo;

    private void Awake()
    {
    }
    
    private void Start()
    {
        LoadMapAsync();
        // TestItem();
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
            TileManager.Inst.AddTile(tile.Pos, tile);
            Debug.Log($"X: {tile.Pos.X}, Y: {tile.Pos.Y}, TileID: {tile.TileId}");
        }

        Debug.Log("TileArray generating has been finished!");
    }

    private void InitializePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void TestItem()
    {
        Item item = new Machete();
        item.ItemInfo = itemInfo;
        item.ItemSprite = itemInfo.itemSprite[0];
        item.Durability = itemInfo.maxDurability;

        Debug.Log(UIManager.Inst.Inventory.AcquireItem(ref item));
    }
}
