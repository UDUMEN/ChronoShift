using UnityEngine;

public class GoalZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (GameManager3.Instance != null && GameManager3.Instance.AllCollected())
            GameManager3.Instance.TriggerWin();
    }
}
