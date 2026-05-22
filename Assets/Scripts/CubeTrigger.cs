using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    CubePuzzleManager manager;

    void Start()
    {
        manager = FindFirstObjectByType<CubePuzzleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.CubeTouched(
                gameObject.name,
                gameObject
            );
        }
    }
}