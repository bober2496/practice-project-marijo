﻿using UnityEngine;

public class enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    int Movehoriz = 1;
    [SerializeField] private float speed;
    [SerializeField] private float turnRightPosition, turnLeftPosition;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * Movehoriz * speed * Time.fixedDeltaTime);
        if (transform.position.x <= turnRightPosition)
        {
            Movehoriz = 1;
            sr.flipX = false;
        }
        if (transform.position.x >= turnLeftPosition)
        {
            Movehoriz = -1;
            sr.flipX = true;
        } 

    }  
}


