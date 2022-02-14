using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehavior
{
    void OnDisable()
    {
        ghost.scatter.Enable();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            Debug.Log("Cha " + gameObject.name + " " + node.availableDirections.Count);
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, gameObject.transform.position.z);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            ghost.movement.ChangeDirection(direction);
        }
    }
}
