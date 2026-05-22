using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public Transform spawnPoint; // Checkpoint sistemi yoksa yedek olarak kullanılır

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Vector3 target;
        Quaternion rot = Quaternion.identity;

        if (CheckpointManager.Instance != null)
        {
            target = CheckpointManager.Instance.GetRespawnPosition();
        }
        else if (spawnPoint != null)
        {
            target = spawnPoint.position;
            rot    = spawnPoint.rotation;
        }
        else return;

        var cc = other.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
        other.transform.SetPositionAndRotation(target, rot);
        if (cc != null) cc.enabled = true;

        Debug.Log("Player respawned at " + target);
    }
}
