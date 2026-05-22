using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{
    public GameObject pastObjects;
    public GameObject futureObjects;

    public bool isPast = true;

    void Start()
    {
        if (TimeTransitionEffect.Instance == null)
            new GameObject("TimeTransitionEffect").AddComponent<TimeTransitionEffect>();

        UpdateObjects();
        // Başlangıç era tint'ini hemen uygula (efektsiz)
        TimeTransitionEffect.Instance.SetEraTintImmediate(isPast);
    }

    public static bool qDisabled = false;

    void Update()
    {
        if (qDisabled) return;
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            isPast = !isPast;
            AudioManager.Play(AudioManager.Instance?.timeTransitionClip);
            if (TimeTransitionEffect.Instance != null)
                TimeTransitionEffect.Instance.PlayTransition(isPast, UpdateObjects);
            else
                UpdateObjects();
        }
    }

    void UpdateObjects()
    {
        if (pastObjects != null) pastObjects.SetActive(isPast);
        if (futureObjects != null) futureObjects.SetActive(!isPast);

        Debug.Log("Time changed to: " + (isPast ? "Past" : "Future"));
    }
}
