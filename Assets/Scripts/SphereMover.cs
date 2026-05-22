using UnityEngine;

public class SphereMover : MonoBehaviour
{
    public float amplitude = 1f;
    public float speed = 1.5f;
    public float phase;          // çift=0, tek=PI → ters senkron
    public Vector3 axis = Vector3.forward;

    private Vector3 origin;

    void Start()
    {
        origin = transform.position;
    }

    void Update()
    {
        transform.position = origin + axis * Mathf.Sin(Time.time * speed + phase) * amplitude;
    }
}
