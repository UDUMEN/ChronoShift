using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string targetScene = "Level2";

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        AudioManager.Play(AudioManager.Instance?.levelTransitionClip);
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(targetScene);
    }
}
