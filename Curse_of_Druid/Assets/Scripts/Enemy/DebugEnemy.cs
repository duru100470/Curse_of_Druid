using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemy : Enemy, IDamageable, IStep
{
    public StateMachine stateMachine;

    protected override void Awake()
    {
        stateMachine = new StateMachine(new DebugEnemyIdle(this));    
    }

    public void OnStep(Entity entity, bool _bool)
    {
        GetDamage(1, DAMAGE_TYPE.Step);
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

    public void Dead()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided");
        Debug.Log(other.collider);

        var entity = other.collider.GetComponent<Entity>();
        Debug.Log(entity);
        if (entity is not Entity) {
            Debug.Log("Not Entity");
            return;
        }

        var collider = GetComponent<Collider2D>();
        if (collider is null) {
            Debug.Log("collider is null");
            return;
        }

        var thisCenterPosition = collider.bounds.center;
        var thisTopPosition = collider.bounds.center + new Vector3(0.0f, collider.bounds.extents.y, 0.0f);
        var targetCenterPosition = other.collider.bounds.center;
        var targetBottomPosition = other.collider.bounds.center - new Vector3(0.0f, other.collider.bounds.extents.y, 0.0f);

        Debug.Log(thisTopPosition);
        Debug.Log(targetCenterPosition);

        if (entity is Player
            && thisTopPosition.y > targetCenterPosition.y
            && thisCenterPosition.y > targetBottomPosition.y)
        {
            Debug.Log("GetDamage");
            (entity as Player).GetDamage(1, DAMAGE_TYPE.Melee);

            // Knockback

            if (other.collider.transform.position.x > this.transform.position.x) {
                (entity as Player).PlayerController.rigid2d.AddForce(Vector2.right * 6f, ForceMode2D.Impulse);
                Debug.Log("if");
            }
            else {
                (entity as Player).PlayerController.rigid2d.AddForce(Vector2.left * 6f, ForceMode2D.Impulse);
                Debug.Log("else");
            }
        }
    }
}
