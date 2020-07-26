using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _blowUpTime;

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" || collision.name == "MapEdge" || collision.gameObject.layer == 8) 
            BlowUp();
    }

    void BlowUp()
    {
        GetComponent<Animator>().SetBool("isded", true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        Destroy(this.gameObject, _blowUpTime);
    }   
}
