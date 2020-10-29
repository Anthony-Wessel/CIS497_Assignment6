using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
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
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && grounded) jump(jumpForce);

        move(x);
    }

    void move(float x)
    {
        float y = rb2d.velocity.y;
        y -= 0.1f;
        rb2d.velocity = new Vector2(x*runSpeed, y);

        if (x != 0) anim.SetBool("running", true);
        else anim.SetBool("running", false);

        sr.flipX = x < 0;
    }

    public void jump(float power)
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.AddForce(Vector2.up * power, ForceMode2D.Impulse);
        grounded = false;
    }

    IEnumerator hurtColor()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        sr.color = Color.white;
    }
}
