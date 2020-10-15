using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] AttackType attackType;
    [SerializeField] int scoreValue = 1;

    // Cached References
    private GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var launcher = collision.gameObject.GetComponent<Launcher>();
        if (launcher)
        {
            launcher.TakeHit();
        }
    }

    public void TakeHit()
    {
        gameSession.AddScore(scoreValue);
        Destroy(gameObject);
    }

    public void Launch(Vector2 velocity)
    {
        var lineFlyer = GetComponent<LineFlyer>();
        if (!lineFlyer)
        {
            Debug.LogError("The enemy should have a LineFlyer component");
            return;
        }
        lineFlyer.SetVelocity(velocity);
    }

    public AttackType GetAttackType()
    {
        return attackType;
    }
}
