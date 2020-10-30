/*
 * Anthony Wessel
 * Assignment 6
 * Controls the player
 */

using System.Collections;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour, IDamageable
{
    public CinemachineVirtualCamera exitCam;
    public CinemachineVirtualCamera playerCam;

    public GameObject HeartHolder;
    
    public float runSpeed;
    public float jumpForce;

    [HideInInspector]
    public bool grounded;

    public int health;

    SpriteRenderer sr;
    Animator anim;
    Rigidbody2D rb2d;

    public void TakeDamage(int amount)
    {
        health -= amount;
        StartCoroutine(hurtColor());

        // Remove hearts from the UI
        for (int i = 0; i < amount; i++)
        {
            if(HeartHolder.transform.childCount > 0)
            {
                Destroy(HeartHolder.transform.GetChild(i).gameObject);
            }
        }
        
        if (health <= 0) die();
    }

    void die()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Pause(GameManager.GameState.LOST);
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        StartCoroutine(cameraTransition());
    }

    // Transition from exit camera to player camera
    IEnumerator cameraTransition()
    {
        yield return new WaitForSeconds(2);

        playerCam.gameObject.SetActive(true);
        exitCam.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check for fall death
        if (transform.position.y < -4) TakeDamage(health);

        // Check input
        float x = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && grounded) jump(jumpForce);

        move(x);
    }

    // Move left or right
    void move(float x)
    {
        float y = rb2d.velocity.y;
        y -= 0.1f;
        rb2d.velocity = new Vector2(x*runSpeed, y);

        if (x != 0) anim.SetBool("running", true);
        else anim.SetBool("running", false);

        sr.flipX = x < 0;
    }

    // Jump into the air
    public void jump(float power)
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.AddForce(Vector2.up * power, ForceMode2D.Impulse);
        grounded = false;
    }

    // Change the player's color to show that the player was hurt
    IEnumerator hurtColor()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        sr.color = Color.white;
    }
}
