using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Collider2D colliderPacman;
    [SerializeField]
    private Movement movement;
    [SerializeField]
    private Animator animator;

    public bool started = false;
    private bool startedAnim = false;
    
    void Update()
    {
        if (started)
        {
            if (!startedAnim)
            {
                startedAnim = true;
                animator.SetBool("started", true);
            }
            ChangeDirection();
        }        
    }

    void ChangeDirection()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.ChangeDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.ChangeDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.ChangeDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.ChangeDirection(Vector2.right);
        }

        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        colliderPacman.enabled = true;
        animator.SetBool("isDie", false);
        animator.SetBool("started", true);
        movement.ResetState();
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        colliderPacman.enabled = false;
        movement.enabled = false;
        animator.SetBool("isDie", true);
        spriteRenderer.enabled = true;
    }
}
