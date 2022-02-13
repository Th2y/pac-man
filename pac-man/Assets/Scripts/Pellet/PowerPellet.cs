using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8f;

    protected override void Eat()
    {
        this.points = 50;
        FindObjectOfType<GameController>().PowerPelletEaten(this);
    }
}
