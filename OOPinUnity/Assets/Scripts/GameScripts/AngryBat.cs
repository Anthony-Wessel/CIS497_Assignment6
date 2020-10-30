/*
 * Anthony Wessel
 * Assignment 6
 * A bat which chases the player
 */

using UnityEngine;

public class AngryBat : Bat
{
    Transform player;

    void Awake()
    {
        player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        move(dir);
    }
}
