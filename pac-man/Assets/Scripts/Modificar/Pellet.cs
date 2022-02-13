using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour
{
    public int points = 10;
    private GameController gameController;
    
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    protected virtual void Eat()
    {
        FindObjectOfType<GameController>().PelletEaten(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && gameController.started)
        {
            Eat();
        }
    }

}
