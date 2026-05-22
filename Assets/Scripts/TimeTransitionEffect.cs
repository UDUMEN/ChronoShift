using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeTransitionEffect : MonoBehaviour
{
    public static TimeTransitionEffect Instance { get; private set; }

    Image fadeOverlay;

    bool transitioning;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        BuildUI();
    }

    void BuildUI()
    {
        var canvasGO = new GameObject("TransitionCanvas");
        canvasGO.transform.SetParent(transform);
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode   = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 200;

        var scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode        = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight  = 0.5f;

        canvasGO.AddComponent<GraphicRaycaster>();

        var go   = new GameObject("FadeOverlay");
        go.transform.SetParent(canvasGO.transform, false);
        var rect = go.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        fadeOverlay       = go.AddComponent<Image>();
        fadeOverlay.color = Color.clear;
    }

    public void SetEraTintImmediate(bool isPast) { }

    public void PlayTransition(bool isPast, Action onMidpoint)
    {
        if (transitioning) return;
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(onMidpoint));
    }

    IEnumerator FadeRoutine(Action onMidpoint)
    {
        transitioning = true;

        // Kararma (0.35s)
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 0.35f;
            fadeOverlay.color = new Color(0f, 0f, 0f, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        fadeOverlay.color = Color.black;

        // Tam karanlıkta geçiş yap
        onMidpoint?.Invoke();

        yield return new WaitForSeconds(0.1f);

        // Açılma (0.45s)
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 0.45f;
            fadeOverlay.color = new Color(0f, 0f, 0f, Mathf.SmoothStep(1f, 0f, t));
            yield return null;
        }
        fadeOverlay.color = Color.clear;

        transitioning = false;
    }
}
