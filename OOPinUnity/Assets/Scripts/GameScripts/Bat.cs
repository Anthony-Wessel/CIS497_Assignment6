/*
 * Anthony Wessel
 * Assignment 6
 * A bat which can damage the player
 */
using System.Collections;
using UnityEngine;

public class Bat : MonoBehaviour, IDamageable
{
    Animator anim;
    SpriteRenderer sr;
    Rigidbody2D rb2d;
    public float flySpeed = 2;
    public float spawnTime;

    public virtual void TakeDamage(int amount)
    {
        StartCoroutine(Die());
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    IEnumerator Die()
    {
        anim.SetTrigger("die");
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }

    // Default bat doesn't move around
    void Update()
    {
        move(Vector2.zero);
    }

    // move a specific direction
    protected void move(Vector2 dir)
    {
        if (Time.time - spawnTime > 0.5f)
        {
            rb2d.velocity = Vector2.zero;
            transform.Translate(dir * Time.deltaTime * flySpeed);
            sr.flipX = dir.x > 0;
        }
    }

    // Damage the player if they touch this bat
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage(1);
        }
    }
}
