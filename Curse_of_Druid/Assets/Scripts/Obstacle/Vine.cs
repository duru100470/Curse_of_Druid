using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : Obstacle
{
    [SerializeField]
    private float timeInterval;

    public GameObject _Vine;
    private bool isTileThere;

    void Start()
    {
        StartCoroutine("Grow", timeInterval);
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(timeInterval);

        /*if(TileManager.Inst.TileArray[(int)this.transform.position.x, (int)this.transform.position.y - 1])
        {
            yield break;
        }
        else
        {
            GameObject instance = Instantiate(_Vine, this.transform.position + new Vector3(0, -1, 0), this.transform.rotation);
            instance.transform.parent = this.transform;
            int index = instance.name.IndexOf("(Clone)");
            if(index > 0)
            {
                instance.name = instance.name.Substring(0, index);
            }
            StartCoroutine("Grow", timeInterval);   //덩굴을 제거하는 상황이 없다고 가정하면 필요 없는 코드.
        }*/
        //TileArray 사용 없는 코드
        RaycastHit2D hitData = Physics2D.Raycast(transform.position + new Vector3(0, -1, 0), -transform.up, 0.1f);
        if (hitData)
        {
            if (hitData.collider.tag == "Tile")
            {
                isTileThere = true;
            }
        }
        Debug.Log(isTileThere);

        if (isTileThere)
        {
            yield break;
        }
        else
        {
            GameObject instance = Instantiate(_Vine, this.transform.position + new Vector3(0, -1, 0), this.transform.rotation);
            instance.transform.parent = this.transform;
            int index = instance.name.IndexOf("(Clone)");
            if (index > 0)
            {
                instance.name = instance.name.Substring(0, index);
            }
            StartCoroutine("Grow", timeInterval);   //덩굴을 제거하는 상황이 없다고 가정하면 필요 없는 코드.
        }
    }
}
