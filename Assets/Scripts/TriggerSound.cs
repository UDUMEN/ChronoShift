using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (AudioManager.Instance != null)
            AudioManager.Play(AudioManager.Instance.questionMarkClip);
    }
}
