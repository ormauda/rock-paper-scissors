using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 450f;
    [SerializeField] Projectile[] projectilesPrefabs;
    [SerializeField] float launchSpeed = 10f;
    [SerializeField] float projectileFirePeriod = 1f;

    private Coroutine firingCoroutine;
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
        Projectile projectilePrefab = GetProjectileFromInput();
        //if (projectilePrefab == null)
        //{
        //    return;
        //}
        if (selectedProjectileButtonName != null && Input.GetButtonUp(selectedProjectileButtonName))
        {
            StopCoroutine(firingCoroutine);
            selectedProjectileButtonName = null;
        }
        else if (projectilePrefab != null)
        {
            firingCoroutine = StartCoroutine(FireContinuously(projectilePrefab));
        }

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

    private Projectile GetProjectileFromInput()
    {
        Projectile projectilePrefab = null;
        if (Input.GetButtonDown(scissorsButtonName))
        {
            projectilePrefab = projectilesPrefabs[0];
            selectedProjectileButtonName = scissorsButtonName;
        }
        else if (Input.GetButtonDown(paperButtonName))
        {
            projectilePrefab = projectilesPrefabs[1];
            selectedProjectileButtonName = paperButtonName;
        }
        else if (Input.GetButtonDown(rockButtonName))
        {
            projectilePrefab = projectilesPrefabs[2];
            selectedProjectileButtonName = rockButtonName;
        }

        return projectilePrefab;
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
