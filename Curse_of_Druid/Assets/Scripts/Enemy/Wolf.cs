using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy, IDamageable, IStep
{
    public StateMachine stateMachine;
    public Rigidbody2D rigid2d { get; set; }
    public bool IsHeadingRight { get; set; }

    protected override void Awake()
    {
        IsHeadingRight = true;
        rigid2d = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine(new WolfIdle(this));
    }

    public void GetDamage(int amount, DAMAGE_TYPE damageType)
    {
        health = Mathf.Max(0, health - amount);
        Debug.Log(health);
        if (health == 0)
        {
            Dead();
            return;
        }
    }
    public void OnStep(Entity entity, bool _bool)
    {
        GetDamage(1, DAMAGE_TYPE.Step);
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
        RaycastHit2D raycastHit2DLeft = Physics2D.Raycast(transform.position, Vector3.left, 0.35f, LayerMask.GetMask("Ground"));
        RaycastHit2D raycastHit2DRight = Physics2D.Raycast(transform.position, Vector3.right, 0.35f, LayerMask.GetMask("Ground"));

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
