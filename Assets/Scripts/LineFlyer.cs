using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFlyer : MonoBehaviour
{
    private Vector2 velocityVector;

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
    }

    public void SetVelocity(Vector2 velocity)
    {
        velocityVector = velocity;
    }
}
