using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] AttackType attackType;
    [SerializeField] int scoreValue = 1;
    [SerializeField] AudioClip deathSfx;
    [SerializeField] [Range(0, 1)] float deathSfxVolume = 1f;

    private bool firstShredderTouch = true;

    // Cached References
    private GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
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
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var launcher = otherCollider.gameObject.GetComponent<Launcher>();
        if (launcher)
        {
            launcher.TakeHit();
            Destroy(gameObject);
        }
    }

    public void TakeHit()
    {
        gameSession.AddScore(scoreValue);
        AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position, deathSfxVolume);
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
