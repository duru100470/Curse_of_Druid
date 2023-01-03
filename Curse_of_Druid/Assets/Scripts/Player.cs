using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Health Properties")]
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;

    private PlayerController playerController;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void GetDamage(int amount, DAMAGE_TYPE dmgType)
    {
        health = Mathf.Max(0, health - amount);
        if (health == 0)
        {
            Dead();
            return;
        }
        StartCoroutine(Stun(1f));
    }

    public void Dead()
    {
        playerController.stateMachine.SetState(new PlayerDead(playerController));
    }

    private IEnumerator Stun(float duration)
    {
        playerController.stateMachine.SetState(new PlayerStun(playerController));
        yield return new WaitForSeconds(duration);
        playerController.stateMachine.SetState(new PlayerIdle(playerController));
    }
}
