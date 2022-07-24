using UnityEngine;

public class followCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    void update()
    {
        transform.position = target.position; 
    }
}
