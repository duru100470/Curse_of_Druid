using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Enemy, IDamageable
{
    public StateMachine stateMachine;
    public Rigidbody2D rigid2d { get; set; }
    public Animator anim;
    public bool IsHeadingRight { get; set; }

    public bool isRolling = false;

    protected override void Awake()
    {
        IsHeadingRight = true;
        rigid2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stateMachine = new StateMachine(new RockIdle(this));
    }

    private void Update()
    {
        stateMachine.DoOperateUpdate();
        
    }

    public void GetDamage(int amount, DAMAGE_TYPE damageType)
    {
        if (damageType != DAMAGE_TYPE.Pickaxe) return;
        health = Mathf.Max(0, health - amount);
        Debug.Log(health);
        if (health == 0)
        {
            Dead();
            return;
        }
    }

    public void Dead()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var entity = other.collider.GetComponent<Entity>();

        if (entity == null) return;

        if (entity is Player)
        {
            (entity as Player).GetDamage(1, DAMAGE_TYPE.Melee);

            // Knockback

            (entity as Player).PlayerController.rigid2d.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);

            if (other.collider.transform.position.x > this.transform.position.x)
                (entity as Player).PlayerController.rigid2d.AddForce(Vector2.right * 6f, ForceMode2D.Impulse);
            else
                (entity as Player).PlayerController.rigid2d.AddForce(Vector2.left * 6f, ForceMode2D.Impulse);
        }
    }
    public int WallCollide()
    {
        // ToDo : zero -> pos of enemy
        RaycastHit2D raycastHit2DLeft = Physics2D.Raycast(transform.position, Vector3.left, 0.5f, LayerMask.GetMask("Ground"));
        RaycastHit2D raycastHit2DRight = Physics2D.Raycast(transform.position, Vector3.right, 0.5f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, transform.position-Vector3.left, Color.red);
        if (raycastHit2DLeft.collider != null)
        {
            return -1;
        }
        else if (raycastHit2DRight.collider != null)
        {
            return 1;
        }

        return 0;
    }
}
