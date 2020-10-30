/*
 * Anthony Wessel
 * Assignment 6
 * Spawns bats when the player walks over the trigger
 */

using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    public GameObject RandomBat;
    public GameObject AngryBat;
    public int spawnAmount = 20;
    public bool spawnUp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Bat bat;
            for (int i = 0; i < spawnAmount; i++)
            {
                // Spawn a bat
                float r = Random.Range(0f, 1f);
                if (r < 0.2) bat = Instantiate(AngryBat, transform.position, Quaternion.identity).GetComponent<Bat>();  
                else bat = Instantiate(RandomBat, transform.position, Quaternion.identity).GetComponent<Bat>();

                // Give the bat an initial velocity
                bat.spawnTime = Time.time;
                Vector2 spawnVelocity = new Vector2(Random.Range(0f, 1f)-0.5f, Random.Range(0f, 1f));
                if (!spawnUp) spawnVelocity.y *= -1;
                bat.GetComponent<Rigidbody2D>().velocity = spawnVelocity.normalized * 5;
            }

            // Destroy the spawner once it has been activated once
            Destroy(gameObject);
        }
    }
}
