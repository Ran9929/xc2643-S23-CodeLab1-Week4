using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 position;

    Rigidbody2D rigidbody2D;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        position.y += Time.deltaTime * speed * direction;
        rigidbody2D.MovePosition(position);
    }
    
    public void Hit()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = 
            new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
    
}
