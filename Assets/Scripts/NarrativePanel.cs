using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativePanel : MonoBehaviour
{
    public static NarrativePanel Instance { get; private set; }

    CanvasGroup panelGroup;
    TMP_Text narrativeText;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        BuildUI();
    }

    void BuildUI()
    {
        var canvasGO = new GameObject("NarrativeCanvas");
        canvasGO.transform.SetParent(transform);
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;

        var scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;

        canvasGO.AddComponent<GraphicRaycaster>();

        // Ana panel — ekranın alt %28'i
        var panelGO = new GameObject("NarrativeBG");
        panelGO.transform.SetParent(canvasGO.transform, false);

        var panelRect = panelGO.AddComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0f, 0f);
        panelRect.anchorMax = new Vector2(1f, 0f);
        panelRect.pivot     = new Vector2(0.5f, 0f);
        panelRect.sizeDelta = new Vector2(0f, 300f);
        panelRect.anchoredPosition = Vector2.zero;

        var bg = panelGO.AddComponent<Image>();
        bg.color = new Color(0.03f, 0.02f, 0.08f, 0.92f);

        panelGroup = panelGO.AddComponent<CanvasGroup>();
        panelGroup.alpha = 0f;
        panelGroup.interactable = false;
        panelGroup.blocksRaycasts = false;

        // Üst süsleme çizgisi
        var lineGO = new GameObject("TopLine");
        lineGO.transform.SetParent(panelGO.transform, false);
        var lineRect = lineGO.AddComponent<RectTransform>();
        lineRect.anchorMin = new Vector2(0.04f, 1f);
        lineRect.anchorMax = new Vector2(0.96f, 1f);
        lineRect.pivot     = new Vector2(0.5f, 1f);
        lineRect.sizeDelta = new Vector2(0f, 3f);
        lineRect.anchoredPosition = new Vector2(0f, -10f);
        lineGO.AddComponent<Image>().color = new Color(0.80f, 0.45f, 1.0f, 0.65f);

        // Sol ince çizgi aksanı
        var accentGO = new GameObject("LeftAccent");
        accentGO.transform.SetParent(panelGO.transform, false);
        var accentRect = accentGO.AddComponent<RectTransform>();
        accentRect.anchorMin = new Vector2(0.04f, 0.15f);
        accentRect.anchorMax = new Vector2(0.04f, 0.85f);
        accentRect.pivot     = new Vector2(0f, 0.5f);
        accentRect.sizeDelta = new Vector2(3f, 0f);
        accentRect.anchoredPosition = Vector2.zero;
        accentGO.AddComponent<Image>().color = new Color(0.80f, 0.45f, 1.0f, 0.45f);

        // Metin alanı
        var textGO = new GameObject("NarrativeText");
        textGO.transform.SetParent(panelGO.transform, false);

        var textRect = textGO.AddComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = new Vector2(110f, 28f);
        textRect.offsetMax = new Vector2(-110f, -24f);

        narrativeText = textGO.AddComponent<TextMeshProUGUI>();
        narrativeText.fontSize = 30f;
        narrativeText.lineSpacing = 12f;
        narrativeText.alignment  = TextAlignmentOptions.MidlineLeft;
        narrativeText.color      = new Color(0.94f, 0.88f, 1.0f, 1f);
        narrativeText.fontStyle  = FontStyles.Italic;
        narrativeText.richText   = true;
        narrativeText.maxVisibleCharacters = 0;
    }

    public void Show(string text)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(text));
    }

    public void ShowSequence(string[] messages)
    {
        StopAllCoroutines();
        StartCoroutine(SequenceRoutine(messages));
    }

    IEnumerator SequenceRoutine(string[] messages)
    {
        foreach (var msg in messages)
        {
            yield return StartCoroutine(ShowRoutine(msg));
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator ShowRoutine(string text)
    {
        // Önceki metni sıfırla
        narrativeText.maxVisibleCharacters = 0;
        narrativeText.text = text;
        narrativeText.ForceMeshUpdate();
        int totalChars = narrativeText.textInfo.characterCount;

        // Panel fade-in
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 0.5f;
            panelGroup.alpha = Mathf.SmoothStep(0f, 1f, t);
            yield return null;
        }
        panelGroup.alpha = 1f;

        // Typewriter efekti
        for (int i = 0; i <= totalChars; i++)
        {
            narrativeText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.018f);
        }

        // Okuma süresi
        yield return new WaitForSeconds(3f);

        // Panel fade-out
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 0.7f;
            panelGroup.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }
        panelGroup.alpha = 0f;
        narrativeText.maxVisibleCharacters = 0;
    }
}
