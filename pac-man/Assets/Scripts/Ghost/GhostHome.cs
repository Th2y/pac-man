using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    void OnEnable()
    {
        StopAllCoroutines();
    }

    void OnDisable()
    {
        if (gameObject.activeSelf) {
            StartCoroutine(ExitTransition());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled && collision.gameObject.tag == "Obstacle") {
            ghost.movement.ChangeDirection(-ghost.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        ghost.movement.ChangeDirection(Vector2.up, true);
        ghost.movement.enabled = false;

        Vector3 position = transform.position;

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            ghost.ChangePosition(Vector3.Lerp(position, inside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < duration)
        {
            ghost.ChangePosition(Vector3.Lerp(inside.position, outside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        ghost.movement.ChangeDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
        ghost.movement.enabled = true;
    }
}
