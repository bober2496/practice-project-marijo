using JetBrains.Annotations;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Transform myTransform;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform bulletParent;
    
    private void Awake()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - myTransform.position;
        diff.Normalize();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {            
            float calcRotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            GameObject newbullet = Instantiate(bullet, myTransform.position, 
                Quaternion.Euler(myTransform.rotation.x, myTransform.rotation.y, -myTransform.rotation.z + calcRotation + 90)) as GameObject;

            newbullet.GetComponent<Rigidbody2D>().AddForce(diff * bulletSpeed);
            newbullet.transform.parent = bulletParent;
        }
    }
}
