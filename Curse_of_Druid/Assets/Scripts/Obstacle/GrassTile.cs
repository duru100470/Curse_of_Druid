using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile, IFlammable
{
    [SerializeField]
    private float timeInterval;
    [SerializeField]
    private int maxGrowNumber;
    [SerializeField]
    private Sprite[] grassSprites;
    private int growNumber = 0;
    
    private bool isBurning = false;
    public bool IsBurning => isBurning;
    [SerializeField]
    private float burnTime;
    [SerializeField]
    private float spreadTime;

    private void Start()
    {
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeInterval);

            if (growNumber < maxGrowNumber)
            {
                growNumber++;
                GetComponent<SpriteRenderer>().sprite = grassSprites[growNumber];
            }
        }
    }

    public void Burn(int burnTime)
    {
        isBurning = true;
        StartCoroutine(DelayBurn());
    }

    public void SpreadFlame()
    {
        // Obstacle.ChildredTileList를 통해 장애물 내에서 퍼지기
        // TileManager를 통해 전체 범위에서 퍼지기
        // 두 가지 케이스로 구현 가능하고 Coordinate.Distance(c1, c2)로 탈 범위 거리 계산 가능
    }

    private IEnumerator DelayBurn()
    {
        yield return new WaitForSeconds(burnTime);

        SpreadFlame();
        Destroy();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Entity entity;
        if (isBurning && collision.gameObject.TryGetComponent<Entity>(out entity))
        {
            if (entity is IDamageable)
                (entity as IDamageable).GetDamage(1, DAMAGE_TYPE.Flame);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();

        if (pc == null) return;

        pc.SlowList[this] = (-1) * growNumber;
        pc.GetSlowDebuff();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();

        if (pc == null) return;

        pc.SlowList.Remove(this);
        pc.GetSlowDebuff();
    }
}
