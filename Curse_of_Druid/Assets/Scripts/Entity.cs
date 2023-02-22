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
    public int Health => health;

    [SerializeField]
    protected Coordinate coor;
    public Coordinate Coor => coor;

    public virtual void AttackEntity() { }
    public virtual void BreakTile() { }

    protected virtual void Awake()
    {
        health = maxHealth;
    }
}