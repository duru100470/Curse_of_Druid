using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected DAMAGE_TYPE damageType;
    [SerializeField]
    protected string enemyName;
    [SerializeField]
    protected float speed;

    public int MaxHealth => maxHealth;
    public int Damage => damage;
    public DAMAGE_TYPE DamageType => damageType;
    public string EnemyName => enemyName;
    public float Speed => speed;
}
