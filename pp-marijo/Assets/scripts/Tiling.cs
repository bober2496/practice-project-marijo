using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{
    [SerializeField] private float offsetX = 2;
    [SerializeField] private bool needFlip;

    private Camera mainCam;
    private Transform myTransform;
    private SpriteRenderer mySR;

    float myWidth;
    private bool hasRightExt, hasLeftExt;

    private void Awake()
    {
        mainCam = Camera.main;
        myTransform = transform;
        mySR = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        myWidth = mySR.sprite.bounds.size.x * myTransform.localScale.x;
    }

    void MakeExtension(int side)    // Left == -1   Right == 1
    {
        Vector3 extensionPosition = new Vector3 (myTransform.position.x + myWidth * side, 
            myTransform.position.y, myTransform.position.z);

        Transform newExtension = Instantiate(myTransform, extensionPosition, myTransform.rotation) as Transform;
        if (needFlip) 
            newExtension.GetComponent<SpriteRenderer>().flipX = !newExtension.GetComponent<SpriteRenderer>().flipX;

        if (side == 1) newExtension.GetComponent<Tiling>().hasLeftExt = true;
        else if (side == -1)newExtension.GetComponent<Tiling>().hasRightExt = true;

        newExtension.transform.parent = myTransform.parent;
    }
    
    void Update()
    {
        if (!hasLeftExt || !hasRightExt)
        {
            float camHorizExtend = mainCam.orthographicSize * Screen.width / Screen.height;
            float myRightEdgeOnCam = myTransform.position.x + myWidth / 2 - camHorizExtend;
            float myLeftEdgeOnCam = myTransform.position.x - myWidth / 2 + camHorizExtend;

            if(mainCam.transform.position.x >= myRightEdgeOnCam - offsetX && !hasRightExt)
            {
                MakeExtension(1);
                hasRightExt = true;
            }
            else if(mainCam.transform.position.x <= myLeftEdgeOnCam + offsetX && !hasLeftExt)
            {
                MakeExtension(-1);
                hasLeftExt = true;
            }
        }
        else this.GetComponent<Tiling>().enabled = false;
    }
}
