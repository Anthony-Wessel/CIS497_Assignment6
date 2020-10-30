/*
 * Anthony Wessel
 * Assignment 6
 * Win trigger that ends the game when the player reaches it
 */

using UnityEngine;

public class Exit : MonoBehaviour
{
    public void Awake()
    {
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance != null)
            GameManager.Instance.Pause(GameManager.GameState.WON);
    }
}
