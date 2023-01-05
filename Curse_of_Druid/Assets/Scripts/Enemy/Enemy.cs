using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int maxHealth;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected DAMAGE_TYPE damageType;
    [SerializeField]
    protected string enemyName;

    public int Health => health;
    public int MaxHealth => maxHealth;
    public int Damage => damage;
    public DAMAGE_TYPE DamageType => damageType;
    public string EnemyName => enemyName;
}
