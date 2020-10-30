/*
 * Anthony Wessel
 * Assignment 6
 * Damages any bats that it hits (when a player jumps on them)
 */

using UnityEngine;

public class Boots : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) player.grounded = true;
        else if (collision.CompareTag("Bat"))
        {
            collision.GetComponent<Bat>().TakeDamage(1);
            player.jump(8);
        }
    }
}
