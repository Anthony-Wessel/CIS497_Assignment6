/*
 * Anthony Wessel
 * Assignment 6
 * A subclass of Enemy which has higher health
 */

using UnityEngine;

public class Golem : Enemy
{

    protected int damage;

    protected override void Awake()
    {
        base.Awake();
        health = 120;

        GameManager.Instance.score = 5;
    }

    protected override void Attack(int amount)
    {
        Debug.Log("Golem attacks");
    }

    public override void TakeDamage(int amount)
    {
        Debug.Log("You took " + amount + " damage");
    }
}
