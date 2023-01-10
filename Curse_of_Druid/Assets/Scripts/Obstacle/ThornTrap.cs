using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornTrap : RuleTile//, IFlammable
{
    bool isPlayerFromUp;

    void Update()
    {
        RaycastHit2D hitData = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), transform.up);
        if(hitData)
        {
            Debug.Log(hitData.collider.tag);
            if(hitData.collider.tag == "Player")
            {
                isPlayerFromUp = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(isPlayerFromUp == true)
        {
            GameObject.Find("Player").GetComponent<Player>().Dead();
            Debug.Log("dead");
        }
    }

    /*void Burn(int burnTime)
    {
        
    }

    void SpreadFlame()
    {
        
    }*/
}
