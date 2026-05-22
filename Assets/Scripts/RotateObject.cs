using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed = 90f;
    public Vector3 axis = Vector3.up;

    void Update()
    {
        transform.Rotate(axis.normalized * speed * Time.deltaTime, Space.World);
    }
}
