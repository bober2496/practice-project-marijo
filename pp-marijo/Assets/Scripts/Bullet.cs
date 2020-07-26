using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _blowUpTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy" || collision.name == "MapEdge" || collision.gameObject.layer == 8)
            BlowUp();
    }

    void BlowUp()
    {
        //GetComponent<Animator>().SetBool("isded", true);      //kell animator elobb
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(this.gameObject, _blowUpTime);
    }
}
