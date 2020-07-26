using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform player;

    public float smoothness = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 position = player.position + offset;
        Vector3 smooth = Vector3.Lerp(transform.position, position, smoothness);
        transform.position = smooth;
    }
}
