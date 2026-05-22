using UnityEngine;

public class CollectibleCube : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (GameManager3.Instance != null)
            GameManager3.Instance.CollectCube();
        gameObject.SetActive(false);
    }
}
