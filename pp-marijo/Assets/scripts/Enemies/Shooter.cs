using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;
    private LayerMask GroundLayer;
    private Vector2 ShooterGroundCheckPosition;

    [SerializeField] float TurnRight, TurnLeft, Jumpforce, JumpRight, JumpLeft;

    [SerializeField] float ProjectilePeriod;
    [SerializeField] float ProjectileSpeed;
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform ProjectileParent;

    bool Grounded;
    bool jumping;
    private int Horiz = 1;
    float timer;

    void ShootProjectile()
    {
        var ProjectileCopy = Instantiate(Projectile,
                new Vector2(GetComponent<Transform>().position.x + 0.1f * Horiz, GetComponent<Transform>().position.y + 0.02f),
                Quaternion.identity);
        ProjectileCopy.transform.parent = ProjectileParent;
        ProjectileCopy.GetComponent<Rigidbody2D>().AddForce(transform.right * Horiz * ProjectileSpeed);
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GroundLayer = LayerMask.GetMask("Ground");
        ShooterGroundCheckPosition = GameObject.Find("Shooter Ground Check").GetComponent<Transform>().position;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        ShootProjectile();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Projectile copy
        timer += Time.fixedDeltaTime;
        if(timer >= ProjectilePeriod)
        {
            timer = 0;
            ShootProjectile();
        }

        //Move path
            //position checks
        Grounded = Physics2D.OverlapCircle(ShooterGroundCheckPosition, 0.1f, GroundLayer);

        if (transform.position.x <= TurnRight)
        {
            Horiz = 1;
            sr.flipX = false;
        }
        if (transform.position.x >= TurnLeft)
        {
            Horiz = -1;
            sr.flipX = true;
        }

        //move
        transform.Translate(Vector2.right * Horiz * _runSpeed * Time.fixedDeltaTime);

        //jump
        if (transform.position.x >= JumpRight && transform.position.x <= JumpRight + 0.01 && Horiz == 1
            || transform.position.x <= JumpLeft && transform.position.x >= JumpLeft - 0.01 && Horiz == -1)
        {
            if (!jumping)
            {
                jumping = true;
                rb.AddForce(transform.up * Jumpforce);
                anim.SetBool("jump", true);
            }
            
        }else if(Grounded){ 
            anim.SetBool("jump", false);
            jumping = false; 
        }

        
        if (!Grounded && !jumping) anim.SetBool("fall", true);
        else anim.SetBool("fall", false);
    }

       

}
