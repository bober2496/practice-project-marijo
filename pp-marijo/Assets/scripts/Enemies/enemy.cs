using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    int Movehoriz=1;
    public float speed;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * Movehoriz * speed * Time.fixedDeltaTime);
        if (transform.position.x <= -3.1)
        {
            Movehoriz = 1;
            sr.flipX = false;
        }
        if (transform.position.x >= -2.5)
        {
            Movehoriz = -1;
            sr.flipX = true;
        } 

    }  
}


