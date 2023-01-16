using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IListener
{
    protected List<Tile> childrenTileList;
    public List<Tile> ChildrenTileList => childrenTileList;
    protected List<Coordinate> supportingTileCoorList;

    protected virtual void Awake()
    {
        EventManager.Inst.AddListener(EVENT_TYPE.TileDestroyed, this);
    }

    public void OnEvent(EVENT_TYPE eventType, Component sender, object param)
    {
        switch (eventType)
        {
            case EVENT_TYPE.TileDestroyed: // param : 바뀐 타일의 Coordinate
                // 파괴된 타일 중 지지대가 있으면 파괴됨
                if (supportingTileCoorList.Exists(e => e == (Coordinate)param))
                    Destroy();
                break;
            default:
                break;
        }
    }

    public virtual void Destroy()
    {
        // 파괴될 때 해야할 일들
        for (int i = childrenTileList.Count - 1; i >= 0; i--)
        {
            childrenTileList[i].DestroyCallback = null;
            childrenTileList[i].Destroy();
        }
    }

    public virtual void DestroyTile(Coordinate coor)
    {
        // childrenTileList에 타일이 하나도 없으면 파괴됨
        if (childrenTileList.Count == 0) Destroy();
    }

    /*
    [SerializeField]
    private int burnTime;
    [SerializeField]
    private float spreadTime;
    [SerializeField]
    private Vector2 spreadSize;
    [SerializeField]
    protected DAMAGE_TYPE damageType;

    private bool isBurning;
    private int currentBurnTime;

    void Update()
    {
        Burn(burnTime);

        if (isBurning)
        {
            Invoke("SpreadFlame", spreadTime);
        }
    }

    public void Burn(int burnTime)
    {
        if (isBurning)
        {
            currentBurnTime -= Mathf.RoundToInt(Time.deltaTime);

            if (currentBurnTime <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void SpreadFlame()
    {
        Collider2D[] hitData = Physics2D.OverlapBoxAll(transform.position, spreadSize, 0);

        foreach (Collider2D item in hitData)
        {
            if (item.tag == "Obstacle")  //Obstacle tag 생성 필요. flammable tag를 생성하면 flammable.
            {
                item.GetComponent<Obstacle>().Burn(burnTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isBurning)
        {
            GameObject.Find("Player").GetComponent<Player>().GetDamage(1, damageType);
        }
    }
    */
}
