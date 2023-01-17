using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Vine : Obstacle
{
    [SerializeField]
    private float timeInterval;
    [SerializeField]
    private GameObject vinePrefab;
    private Coordinate growingPoint;

    private void Start()
    {
        StartCoroutine(Grow());
        growingPoint = new Coordinate(0, -1) + Coordinate.WorldPointToCoordinate(transform.position);
    }

    private IEnumerator Grow()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeInterval);

            Debug.Log($"{growingPoint.X}:{growingPoint.Y}");

            if (!TileManager.Inst.TileDict.ContainsKey(growingPoint))
            {
                GameObject obj = Instantiate(vinePrefab,
                    Coordinate.CoordinatetoWorldPoint(growingPoint),
                    Quaternion.identity);
                obj.transform.parent = this.transform;

                var vineTile = obj.GetComponent<VineTile>();
                vineTile.Obstacle = this;
                vineTile.DestroyCallback = DestroyTile;

                // fixme
                // TileManager에 타일 생성 알려야함
                TileManager.Inst.AddTile(growingPoint, vineTile);

                growingPoint += new Coordinate(0, -1);
            }
        }
    }

    // childrenTile의 destroy에 실행됨
    public override void DestroyTile(Coordinate coor)
    {
        // 부서진 풀 타일보다 위에 있는 타일들 찾기
        var aboveGrasses = childrenTileList.Where(e => e.Pos.Y > coor.Y).ToList();

        // 생장점 다시 내리기
        growingPoint = coor;

        // 위 타일 제거
        for (int i = aboveGrasses.Count - 1; i >= 0; i--)
        {
            // Callback 제거해서 이 함수가 불리지 않도록
            aboveGrasses[i].DestroyCallback = null;
            aboveGrasses[i].Destroy();
        }

        base.DestroyTile(coor);
    }
}
