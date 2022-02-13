using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8f;
    public float speedMultiplier = 1f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public Rigidbody2D rigidbody2D;
    public Vector2 direction;
    public Vector2 nextDirection;
    private Vector3 startingPosition;
    
    void Start()
    {
        startingPosition = gameObject.transform.position;
        ResetState();
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        enabled = true;
    }

    void Update()
    {
        if (nextDirection != Vector2.zero)
        {
            ChangeDirection(nextDirection);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        rigidbody2D.MovePosition(position + translation);
    }

    public void ChangeDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !HasObstacle(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool HasObstacle(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }
}
