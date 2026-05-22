using UnityEngine;
using UnityEngine.InputSystem;

public class QuestionMarkTrigger : MonoBehaviour
{
    public GameObject storyUI;
    public GameObject sonradanWall;
    public GameObject soruDuvar;
    public GameObject mummy;

    private bool storyActive;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || storyActive) return;
        storyActive = true;
        if (storyUI != null)      storyUI.SetActive(true);
        if (sonradanWall != null) sonradanWall.SetActive(true);
    }

    void Update()
    {
        if (!storyActive) return;
        if (Keyboard.current == null || !Keyboard.current.eKey.wasPressedThisFrame) return;

        if (storyUI != null)   storyUI.SetActive(false);
        if (soruDuvar != null) soruDuvar.SetActive(false);
        if (mummy != null)     mummy.SetActive(true);

        TimeManager.qDisabled = true;

        storyActive = false;
        gameObject.SetActive(false);
    }
}
