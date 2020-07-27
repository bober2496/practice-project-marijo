using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHead : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player" || collision.collider.name == "Bullet")
            GetComponentInParent<enemyDeath>().enabled = true;
    }


    /*      Eredeti code

    //enemy meghalas
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player") 
        {
            GetComponentInParent<Animator>().SetBool("isded", true);
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            Destroy(transform.parent.gameObject, 0.2f);
        }
    }

    */
}
