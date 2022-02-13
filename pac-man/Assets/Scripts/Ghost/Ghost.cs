using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    public Movement movement;
    public GhostHome home;
    public GhostScatter scatter;
    public GhostChase chase;
    public GhostFrightened frightened;
    [SerializeField]
    private GhostBehavior initialBehavior;
    public Transform target;
    public int points = 200;
    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();

        frightened.Disable();
        chase.Disable();
        scatter.Enable();

        if (home != initialBehavior) {
            home.Disable();
        }

        if (initialBehavior != null) {
            initialBehavior.Enable();
        }
    }

    public void ChangePosition(Vector3 position)
    {
        position.z = transform.position.z;
        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (frightened.enabled) 
            {
                gameController.GhostEaten(this);
            } 
            else 
            {
                gameController.LoseLife();
            }
        }
    }
}