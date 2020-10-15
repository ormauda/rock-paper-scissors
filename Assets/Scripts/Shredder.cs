using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var enemy = otherCollider.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TouchShredder();
        }
        else
        {
            Destroy(otherCollider.gameObject);
        }
    }
}
