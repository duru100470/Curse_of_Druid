using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Health Properties")]
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int maxHealth;
    public int Health { get { return health; } set { health = value; } }

    [SerializeField]
    protected Coordinate coor;
    public Coordinate Coor => coor;

    public virtual void AttackEntity(int damageAmount, DAMAGE_TYPE dmgType) { }
    public virtual void BreakTile() { }

    protected virtual void Awake()
    {
        health = maxHealth;
    }
}