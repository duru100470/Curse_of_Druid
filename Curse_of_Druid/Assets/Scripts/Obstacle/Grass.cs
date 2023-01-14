using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Obstacle
{
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

        if(timer > timeInterval && growNumber < maxGrowNumber)
        {
            timer = 0f;
            growNumber++;
            Grow(growNumber);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && growNumber <= maxGrowNumber)
        {
            other.GetComponent<PlayerController>().MaxSpeedSetter = speed - growNumber;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player" && growNumber <= maxGrowNumber)
        {
            other.GetComponent<PlayerController>().MaxSpeedSetter = speed;
        }
    }

    void Grow(int growNumber)
    {
        //sprite 변화
        //Debug.Log(growNumber);
    }
}
