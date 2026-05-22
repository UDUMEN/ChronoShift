using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager3 : MonoBehaviour
{
    public static GameManager3 Instance { get; private set; }

    const int TOTAL_CUBES = 4;
    int collected = 0;
    bool gameWon = false;

    // UI
    Canvas canvas;
    CanvasGroup introGroup;
    CanvasGroup winGroup;
    CanvasGroup hudGroup;
    TMP_Text hudText;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        BuildUI();
    }

    void Start()
    {
        StartCoroutine(ShowIntro());
        UpdateHUD();
    }

    // ── UI BUILD ─────────────────────────────────────────────────────────────

    void BuildUI()
    {
        var canvasGO = new GameObject("GameUI3");
        canvasGO.transform.SetParent(transform);
        canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 50;

        var scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        canvasGO.AddComponent<GraphicRaycaster>();

        introGroup = BuildPanel(canvasGO.transform, "IntroPanel",
            new Color(0.04f, 0.04f, 0.12f, 0.93f),
            "4 tane <color=#FFD700>sarı küp</color>ü topla\nve <color=#44FF88>yeşil alana</color> geri gel!",
            56f, "<size=32><color=#AAAAAA>[E] tuşuna bas veya bekle</color></size>");

        winGroup = BuildPanel(canvasGO.transform, "WinPanel",
            new Color(0.02f, 0.12f, 0.04f, 0.95f),
            "<color=#44FF88>Başardın!</color>",
            80f, "<size=36><color=#CCFFCC>Tüm küpleri topladın ve hedefe ulaştın!</color></size>");
        winGroup.alpha = 0f;
        winGroup.interactable = false;
        winGroup.blocksRaycasts = false;

        // HUD — top-right küp sayacı
        var hudGO = new GameObject("HUD");
        hudGO.transform.SetParent(canvasGO.transform, false);
        var hudRect = hudGO.AddComponent<RectTransform>();
        hudRect.anchorMin = new Vector2(1f, 1f);
        hudRect.anchorMax = new Vector2(1f, 1f);
        hudRect.pivot     = new Vector2(1f, 1f);
        hudRect.anchoredPosition = new Vector2(-40f, -30f);
        hudRect.sizeDelta = new Vector2(320f, 60f);

        var bg = hudGO.AddComponent<Image>();
        bg.color = new Color(0f, 0f, 0f, 0.55f);

        hudGroup = hudGO.AddComponent<CanvasGroup>();
        hudGroup.alpha = 0f;

        var txtGO = new GameObject("HUDText");
        txtGO.transform.SetParent(hudGO.transform, false);
        var tr = txtGO.AddComponent<RectTransform>();
        tr.anchorMin = Vector2.zero; tr.anchorMax = Vector2.one;
        tr.offsetMin = new Vector2(16f, 8f); tr.offsetMax = new Vector2(-16f, -8f);
        hudText = txtGO.AddComponent<TextMeshProUGUI>();
        hudText.fontSize = 28f;
        hudText.alignment = TextAlignmentOptions.MidlineLeft;
        hudText.color = Color.white;
        hudText.richText = true;
    }

    CanvasGroup BuildPanel(Transform parent, string id, Color bgColor,
                           string mainMsg, float mainSize, string subMsg)
    {
        var panelGO = new GameObject(id);
        panelGO.transform.SetParent(parent, false);

        var rect = panelGO.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero; rect.offsetMax = Vector2.zero;

        panelGO.AddComponent<Image>().color = bgColor;
        var group = panelGO.AddComponent<CanvasGroup>();

        // Main text
        var mainGO = new GameObject("Main");
        mainGO.transform.SetParent(panelGO.transform, false);
        var mr = mainGO.AddComponent<RectTransform>();
        mr.anchorMin = new Vector2(0.1f, 0.45f); mr.anchorMax = new Vector2(0.9f, 0.75f);
        mr.offsetMin = Vector2.zero; mr.offsetMax = Vector2.zero;
        var mt = mainGO.AddComponent<TextMeshProUGUI>();
        mt.text = mainMsg; mt.fontSize = mainSize;
        mt.alignment = TextAlignmentOptions.Center;
        mt.color = Color.white; mt.richText = true;

        // Sub text
        var subGO = new GameObject("Sub");
        subGO.transform.SetParent(panelGO.transform, false);
        var sr = subGO.AddComponent<RectTransform>();
        sr.anchorMin = new Vector2(0.1f, 0.28f); sr.anchorMax = new Vector2(0.9f, 0.44f);
        sr.offsetMin = Vector2.zero; sr.offsetMax = Vector2.zero;
        var st = subGO.AddComponent<TextMeshProUGUI>();
        st.text = subMsg; st.fontSize = 36f;
        st.alignment = TextAlignmentOptions.Center;
        st.color = Color.white; st.richText = true;

        return group;
    }

    // ── COROUTINES ────────────────────────────────────────────────────────────

    IEnumerator ShowIntro()
    {
        // Fade in
        float t = 0f;
        while (t < 1f) { t += Time.deltaTime / 0.4f; introGroup.alpha = Mathf.Clamp01(t); yield return null; }
        introGroup.alpha = 1f;

        // Wait for E key or 5 seconds
        float elapsed = 0f;
        while (elapsed < 5f)
        {
            elapsed += Time.deltaTime;
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame) break;
            yield return null;
        }

        // Fade out intro
        t = 0f;
        while (t < 1f) { t += Time.deltaTime / 0.4f; introGroup.alpha = Mathf.Clamp01(1f - t); yield return null; }
        introGroup.alpha = 0f;
        introGroup.interactable = false;
        introGroup.blocksRaycasts = false;

        // Show HUD
        t = 0f;
        while (t < 1f) { t += Time.deltaTime / 0.3f; hudGroup.alpha = Mathf.Clamp01(t); yield return null; }
        hudGroup.alpha = 1f;
    }

    IEnumerator ShowWin()
    {
        // Fade in win panel
        winGroup.interactable = true;
        winGroup.blocksRaycasts = true;
        float t = 0f;
        while (t < 1f) { t += Time.deltaTime / 0.5f; winGroup.alpha = Mathf.Clamp01(t); yield return null; }
        winGroup.alpha = 1f;
    }

    // ── PUBLIC API ────────────────────────────────────────────────────────────

    public void CollectCube()
    {
        if (gameWon) return;
        collected = Mathf.Min(collected + 1, TOTAL_CUBES);
        UpdateHUD();
    }

    public bool AllCollected() => collected >= TOTAL_CUBES;

    public void TriggerWin()
    {
        if (gameWon) return;
        gameWon = true;
        StartCoroutine(ShowWin());
    }

    void UpdateHUD()
    {
        if (hudText != null)
            hudText.text = $"Sarı Küp: <color=#FFD700>{collected}</color> / {TOTAL_CUBES}";
    }
}
