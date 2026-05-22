using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static Vector3 StartPosition;

    void Awake()
    {
        StartPosition = transform.position;
    }
}
