using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grass : Obstacle
{
    [SerializeField]
    private float timeInterval;
    [SerializeField]
    private int maxGrowNumber;
    private int growNumber = 0;
    [SerializeField]
    private GameObject grassPrefab;
    private Coordinate growingPoint;

    private void Start()
    {
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        yield return new WaitForSeconds(timeInterval);

        if (!TileManager.Inst.TileDict.ContainsKey(growingPoint) ||
            growNumber < maxGrowNumber)
        {
            GameObject obj = Instantiate(grassPrefab,
                Coordinate.CoordinatetoWorldPoint(growingPoint),
                Quaternion.identity);
            obj.transform.parent = this.transform;

            var grassTile = obj.GetComponent<GrassTile>();
            grassTile.Obstacle = this;
            grassTile.DestroyCallback = DestroyTile;

            // fixme
            // TileManager에 타일 생성 알려야함

            growingPoint += new Coordinate(0, 1);
        }
    }

    // childrenTile의 destroy에 실행됨
    public override void DestroyTile(Coordinate coor)
    {
        // 부서진 풀 타일보다 위에 있는 타일들 찾기
        var aboveGrasses = childrenTileList.Where(e => e.Pos.Y > coor.Y).ToList();

        // 생장점 다시 내리기
        growingPoint = coor;
        growNumber -= aboveGrasses.Count + 1;

        // 위 타일 제거
        for (int i = aboveGrasses.Count - 1; i >= 0; i--)
        {
            // Callback 제거해서 이 함수가 불리지 않도록
            aboveGrasses[i].DestroyCallback = null;
            aboveGrasses[i].Destroy();
        }

        base.DestroyTile(coor);
    }

    /*
    [SerializeField]
    private float timeInterval;
    [SerializeField]
    private int maxGrowNumber;

    private float timer;
    private int growNumber;
    private bool isPlayerThere;
    private float speed;

    void Start()
    {
        timer = 0f;
        growNumber = 0;
        speed = GameObject.Find("Player").GetComponent<PlayerController>().MaxSpeed;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeInterval && growNumber < maxGrowNumber)
        {
            timer = 0f;
            growNumber++;
            Grow(growNumber);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && growNumber <= maxGrowNumber)
        {
            other.GetComponent<PlayerController>().MaxSpeed = speed - growNumber;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && growNumber <= maxGrowNumber)
        {
            other.GetComponent<PlayerController>().MaxSpeed = speed;
        }
    }

    void Grow(int growNumber)
    {
        //sprite 변화
        //Debug.Log(growNumber);
    }
    */
}
