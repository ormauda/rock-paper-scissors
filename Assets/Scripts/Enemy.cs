using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] AttackType attackType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
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
