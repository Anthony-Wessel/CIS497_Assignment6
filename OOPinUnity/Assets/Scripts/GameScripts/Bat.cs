using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour, IDamageable
{
    Animator anim;
    SpriteRenderer sr;
    public float flySpeed = 2;

    public virtual void TakeDamage(int amount)
    {
        StartCoroutine(Die());
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    IEnumerator Die()
    {
        anim.SetTrigger("die");
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }

    protected void move(Vector2 dir)
    {
        transform.Translate(dir * Time.deltaTime * flySpeed);
        sr.flipX = dir.x > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage(1);
        }
    }
}
