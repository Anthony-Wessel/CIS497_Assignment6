using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBat : Bat
{
    float lastChange = 0;
    Vector2 dir = Vector2.left;

    private void Update()
    {
        if (Time.time - lastChange > 1f)
        {
            dir = new Vector2(Random.Range(0f, 1f) - 0.5f, Random.Range(0f, 1f) - 0.5f);
            dir.Normalize();

            lastChange = Time.time;
        }

        move(dir);
    }
}
