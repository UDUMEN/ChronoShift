using UnityEngine;

public class SphereRespawn : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var cc = other.GetComponentInParent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            other.transform.root.position = SpawnManager.StartPosition;
            cc.enabled = true;
        }
        else
        {
            other.transform.root.position = SpawnManager.StartPosition;
        }
    }
}
