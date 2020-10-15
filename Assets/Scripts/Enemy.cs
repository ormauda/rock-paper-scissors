using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] AttackType attackType;
    [SerializeField] int scoreValue = 1;

    private bool firstShredderTouch = true;

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


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        HandleLauncherTrigger(otherCollider);
        //HandleShredderTrigger(otherCollider);
    }

    public void TouchShredder()
    {
        if (firstShredderTouch)
        {
            firstShredderTouch = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private bool HandleLauncherTrigger(Collider2D otherCollider)
    {
        var launcher = otherCollider.gameObject.GetComponent<Launcher>();
        if (launcher)
        {
            launcher.TakeHit();
            return true;
        }
        return false;
    }

    //private void HandleShredderTrigger(Collider2D otherCollider)
    //{
    //    var shredder = otherCollider.gameObject.GetComponent<Shredder>();
    //    if (!shredder)
    //    {
    //        return;
    //    }
    //    if (isOnScreen)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        isOnScreen = true;
    //    }
    //}

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
