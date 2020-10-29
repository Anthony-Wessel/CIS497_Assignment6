using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBat : Bat
{
    Transform player;

    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        move(dir);
    }
}
