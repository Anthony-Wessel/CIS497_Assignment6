using System.Collections;
using System.Collections.Generic;
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
