using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform transf;
    private Animator animator;    
    private RigidbodyConstraints2D originalConstraints;     //eredeti constraints elmentesere

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        transf = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {        
        originalConstraints = rb.constraints;
    }

    // Update is called once per frame
    void Update()
    {
        //Ha elhagyja a pályát meghal
        if (transf.position.y <= -3f) StartCoroutine(Halal());
    }

    //Enemyvel interakció
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.collider.tag == "enemy") StartCoroutine(Halal());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy" || collision.name == "MapEdge") StartCoroutine(Halal());
    }

    IEnumerator Halal()
    {
        animator.SetBool("isded", true);
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(1f);
        rb.constraints = originalConstraints;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        animator.SetBool("isded", false);
    }
}
