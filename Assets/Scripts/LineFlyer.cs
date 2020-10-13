using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFlyer : MonoBehaviour
{
    private Vector2 velocityVector;

    // Cached references
    private new Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var newPosition = new Vector2(
            transform.position.x + velocityVector.x * Time.deltaTime,
            transform.position.y + velocityVector.y * Time.deltaTime);
        transform.position = newPosition;
        //rigidbody2D.AddForce(transform.forward * 2f);
    }

    public void SetVelocity(Vector2 velocity)
    {
        velocityVector = velocity;
    }
}
