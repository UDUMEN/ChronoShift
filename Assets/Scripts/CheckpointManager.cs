using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    public int ActiveIndex { get; private set; } = -1;

    Vector3 respawnPosition;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    // Checkpoint'ler Start()'ta kendini kaydeder; en düşük index otomatik aktif olur
    public void RegisterCheckpoint(int index, Vector3 position)
    {
        if (index > ActiveIndex) return; // sadece daha küçük index ilk defa çağırır
        // bu metod kullanılmıyor — SetCheckpoint kullan
    }

    public void SetCheckpoint(int index, Vector3 position, bool force = false)
    {
        if (!force && index <= ActiveIndex) return;
        ActiveIndex = index;
        respawnPosition = position;
        Debug.Log($"[Checkpoint] {index} aktif — respawn: {position}");
    }

    public Vector3 GetRespawnPosition() => respawnPosition;
}
