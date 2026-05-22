using UnityEngine;

public class TopTrigger : MonoBehaviour
{
    public PuzzleManager puzzleManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puzzleManager.TopTouched(gameObject.name);
        }
    }
}