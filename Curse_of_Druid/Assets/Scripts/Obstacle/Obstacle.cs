using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : RuleTile, IFlammable
{
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

        if(isBurning)
        {
            Invoke("SpreadFlame", spreadTime);
        }
    }

    public void Burn(int burnTime)
    {
        if(isBurning)
        {
            currentBurnTime -= Mathf.RoundToInt(Time.deltaTime);

            if(currentBurnTime <= 0)
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
            if(item.tag == "Obstacle")  //Obstacle tag 생성 필요. flammable tag를 생성하면 flammable.
            {
                item.GetComponent<Obstacle>().Burn(burnTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && isBurning)
        {
            GameObject.Find("Player").GetComponent<Player>().GetDamage(1, damageType);
        }
    }
}
