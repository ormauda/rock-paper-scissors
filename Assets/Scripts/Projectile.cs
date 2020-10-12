using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] AttackType attackType;

    public void Launch(Vector2 velocity)
    {
        var lineFlyer = GetComponent<LineFlyer>();
        if (!lineFlyer)
        {
            Debug.LogError("The projectile should have a LineFlyer component");
            return;
        }
        lineFlyer.SetVelocity(velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.gameObject.GetComponent<Enemy>();
        if (target == null)
        {
            return;
        }

        var targetAttackType = target.GetAttackType();
        if (GetDominatorOf(targetAttackType) == attackType)
        {
            target.Hit();
        }

    }

    private AttackType GetDominatorOf(AttackType attack)
    {
        switch (attack)
        {
            case AttackType.Rock: return AttackType.Paper;
            case AttackType.Paper: return AttackType.Scissors;
            case AttackType.Scissors: return AttackType.Rock;
            default: throw new ArgumentException($"There is no such type of attack: {attack}");
        };
    }
}
