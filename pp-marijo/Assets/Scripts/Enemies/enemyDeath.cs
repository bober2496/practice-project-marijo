using UnityEngine;

public class enemyDeath : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Animator>().SetBool("isded", true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        Destroy(gameObject, 0.2f);
    }


    /*      Nem mukodik tokeletesen.

    private Collider2D _headCollider, _bodyCollider;
    [SerializeField] private Collider2D[] deadlyOnHead, deadlyOnBody;

    void Death()
    {
        GetComponent<Animator>().SetBool("isded", true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        Destroy(gameObject, 0.2f);
    }

    void Awake()
    {
        _headCollider = transform.Find("enemyHead").GetComponent<BoxCollider2D>();
        _bodyCollider = transform.Find("enemyBody").GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (deadlyOnHead.Length > 0)
            foreach (Collider2D i in deadlyOnHead)
                if (_headCollider.IsTouching(i)) Death();

        if (deadlyOnBody.Length > 0)
            foreach (Collider2D i in deadlyOnBody)
                if (_bodyCollider.IsTouching(i)) Death();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (deadlyOnHead.Length > 0)
            foreach (Collider2D i in deadlyOnHead)
                if (collision == i) Death();

        if (deadlyOnBody.Length > 0)
            foreach (Collider2D i in deadlyOnBody)
                if (collision == i) Death();
    }

    */
}