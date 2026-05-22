using UnityEngine;

public class Soru4Trigger : MonoBehaviour
{
    private bool triggered;

    private readonly string[] messages = new string[]
    {
        "Gelecek ve geçmişi kullanarak doğru küplerle kapıları aç.\n\n<color=#FFCC88><i>Yanlışı bulursan doğruları unutma...</i></color>",
    };

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || triggered) return;
        triggered = true;

        if (NarrativePanel.Instance == null)
            new GameObject("NarrativePanel").AddComponent<NarrativePanel>();

        NarrativePanel.Instance.ShowSequence(messages);
        gameObject.SetActive(false);
    }
}
