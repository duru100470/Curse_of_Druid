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

    private void Start()
    {
        StartCoroutine(Grow());
        supportingTileCoorList.Add(new Coordinate(0, -1));
    }

    private IEnumerator Grow()
    {
        yield return new WaitForSeconds(timeInterval);

        if (growNumber < maxGrowNumber)
        {
            
        }
    }
}
