using Assets.Scripts.Classes;
using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] AttackType attackType;
    
    [SerializeField] AudioClip deathSfx;
    [SerializeField] [Range(0, 1)] float deathSfxVolume = 1f;

    // Cached references
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

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

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var target = otherCollider.gameObject.GetComponent<Enemy>();
        if (target == null)
        {
            return;
        }

        var targetAttackType = target.GetAttackType();
        if (AttacksBalancer.GetDominatorOf(targetAttackType) == attackType)
        {
            target.TakeHit();
            Destroy(gameObject);
        }
        else if (AttacksBalancer.GetDominatorOf(attackType) == targetAttackType)
        {
            Destroy(otherCollider.gameObject);
            TakeHit();
        }

    }

    private void TakeHit()
    {
        gameSession.ReduceScore(1);
        AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position, deathSfxVolume);
        Destroy(gameObject);
    }
}
