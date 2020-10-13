﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 450f;
    [SerializeField] Projectile[] projectilesPrefabs;
    [SerializeField] float launchSpeed = 10f;
    [SerializeField] float projectileFirePeriod = 1f;
    [SerializeField] float switchProjectileDelay = 0.2f;

    private Coroutine firingCoroutine;
    private Projectile selectedProjectilePrefab = null;
    private string selectedProjectileButtonName = null;
    private readonly string scissorsButtonName = "Fire1";
    private readonly string paperButtonName = "Fire2";
    private readonly string rockButtonName = "Fire3";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Fire();
    }

    private void Rotate()
    {
        var deltaZ = Input.GetAxis("Horizontal");
        if (Mathf.Abs(deltaZ) < Mathf.Epsilon)
        {
            return;
        }

        var newZRotation = transform.rotation.z - deltaZ * rotationSpeed;
        var rotationVector = new Vector3(0, 0, newZRotation);
        transform.Rotate(rotationVector * Time.deltaTime);
    }

    private void Fire()
    {
        var previousProjectilePrefab = selectedProjectilePrefab;

        SetProjectileFromInput();

        if (HasSwitchedProjectile(previousProjectilePrefab) || HasStoppedFiring(previousProjectilePrefab))
        {
            Debug.Log("stopping coroutine");
            StopCoroutine(firingCoroutine);
        }

        if (HasSwitchedProjectile(previousProjectilePrefab) || HasStartedFiring(previousProjectilePrefab))
        {
            Debug.Log("starting coroutine");
            firingCoroutine = StartCoroutine(FireContinuously(selectedProjectilePrefab));
        }
    }

    private bool HasStoppedFiring(Projectile previousProjectile)
    {
        if (selectedProjectilePrefab == null && previousProjectile != null)
            Debug.Log("stopped firing");
        return selectedProjectilePrefab == null && previousProjectile != null;
    }

    private bool HasSwitchedProjectile(Projectile previousProjectile)
    {
        if (selectedProjectilePrefab != previousProjectile && previousProjectile != null && selectedProjectilePrefab != null)
            Debug.Log("switching projectile");
        return selectedProjectilePrefab != previousProjectile && previousProjectile != null && selectedProjectilePrefab != null;
    }

    private bool HasStartedFiring(Projectile previousProjectile)
    {
        if (selectedProjectilePrefab != null && previousProjectile == null)
            Debug.Log("starting firing");
        return selectedProjectilePrefab != null && previousProjectile == null;
    }

    private IEnumerator FireContinuously(Projectile projectilePrefab)
    {
        while (true)
        {
            var newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            var launchDirection = CalculateLaunchDirection();
            newProjectile.Launch(launchDirection * launchSpeed);
            yield return new WaitForSeconds(projectileFirePeriod);
        }
    }

    private void SetProjectileFromInput()
    {
        if (Input.GetButtonDown(scissorsButtonName))
        {
            selectedProjectilePrefab = projectilesPrefabs[0];
            selectedProjectileButtonName = scissorsButtonName;
        }
        else if (Input.GetButtonDown(paperButtonName))
        {
            selectedProjectilePrefab = projectilesPrefabs[1];
            selectedProjectileButtonName = paperButtonName;
        }
        else if (Input.GetButtonDown(rockButtonName))
        {
            selectedProjectilePrefab = projectilesPrefabs[2];
            selectedProjectileButtonName = rockButtonName;
        }
        else if (selectedProjectileButtonName != null && Input.GetButtonUp(selectedProjectileButtonName))
        {
            selectedProjectilePrefab = null;
            selectedProjectileButtonName = null;
        }
    }

    /*
     * caluclate the launch direction vector based on the rotation of the launcher
     * Vector.X = SINUS of angle in RADIANS
     * Vector.Y = COSINUS of angle in RADIANS
     * 1 DEG = (1 * Mathf.Deg2Rad) RADIDANS
     * somehow the x coordinates are flipped, so multiply by -1
     */
    private Vector2 CalculateLaunchDirection()
    {
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 launchDirection = new Vector2(Mathf.Sin(angle) * -1, Mathf.Cos(angle));
        return launchDirection;
    }
}
