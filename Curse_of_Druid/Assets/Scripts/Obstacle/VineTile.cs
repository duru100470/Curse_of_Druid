using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineTile : RuleTile, IFlammable
{
    public Obstacle Obstacle { get; set; }
    private bool isBurning = false;
    public bool IsBurning => isBurning;
    [SerializeField]
    private float burnTime;
    [SerializeField]
    private float spreadTime;

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
}
